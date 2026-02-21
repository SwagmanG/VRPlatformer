using UnityEngine;
using UnityEngine.XR;

public class LevelMover : MonoBehaviour
{
    public float moveSpeed = 2f;
    public Rigidbody playerRigidbody;

    // Static flag so other scripts like PlayerKill can check if the level is moving
    public static bool IsMoving { get; private set; }

    private CharacterController _playerCharacterController;

    // Store the player's position/rotation while the level is moving so we can hold them in place
    private Vector3 _frozenPosition;
    private Quaternion _frozenRotation;

    private void Start()
    {
        // Cache the CharacterController on the same GameObject as the player's Rigidbody
        if (playerRigidbody != null)
            _playerCharacterController = playerRigidbody.GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Read the right thumbstick axis from the XR right-hand controller
        var rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightStick);

        // Consider the level to be moving only when the stick is pushed beyond a small dead zone
        IsMoving = Mathf.Abs(rightStick.y) > 0.1f;

        if (IsMoving)
        {
            // Shift the entire level up or down based on the vertical stick input
            transform.position += Vector3.up * rightStick.y * moveSpeed * Time.deltaTime;

            // While the level moves, lock the player in place so they move with it seamlessly.
            // The CharacterController must be disabled before we can manually set the transform.
            if (_playerCharacterController != null)
            {
                _playerCharacterController.enabled = false;
                playerRigidbody.transform.position = _frozenPosition;
                playerRigidbody.transform.rotation = _frozenRotation;
            }
        }
        else
        {
            // Stick released — snapshot the player's current position/rotation so we have a
            // valid freeze point ready for the next time the level starts moving, then
            // re-enable the CharacterController to restore normal player movement.
            if (_playerCharacterController != null)
            {
                _frozenPosition = playerRigidbody.transform.position;
                _frozenRotation = playerRigidbody.transform.rotation;
                _playerCharacterController.enabled = true;
            }
        }
    }
}