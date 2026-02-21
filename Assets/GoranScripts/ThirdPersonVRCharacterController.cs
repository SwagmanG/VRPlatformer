using StarterAssets;
using UnityEngine;
using UnityEngine.XR;

public class ThirdPersonVRCharacterController : MonoBehaviour
{

	Vector2 moveVector;
	bool jumpInput;
	float gripValue;

	public StarterAssetsInputs inputs;

	private void Update()
	{
		var leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
		var rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

		leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out moveVector);

		rightController.TryGetFeatureValue(CommonUsages.grip, out gripValue);
		rightController.TryGetFeatureValue(CommonUsages.primaryButton, out jumpInput);

		if(inputs != null){
			inputs.MoveInput(moveVector);
			inputs.JumpInput(jumpInput);
			// if(triggerValue == 0){
			// 	inputs.SprintInput(false);
			// }else{
			// 	inputs.SprintInput(true);
			// }
			inputs.SprintInput(gripValue != 0);
		}
	}

}
