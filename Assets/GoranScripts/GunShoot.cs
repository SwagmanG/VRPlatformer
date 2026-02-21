using UnityEngine;
using UnityEngine.XR;

public class GunShoot : MonoBehaviour
{
    // Which physical hand this gun component is attached to
    public enum Hand { Left, Right }
    public Hand hand;

    public GameObject projectilePrefab;  // The projectile to spawn on fire
    public Transform muzzlePoint;        // Spawn point and direction for the projectile

    public float projectileSpeed = 20f;  // Launch speed applied to the projectile's Rigidbody
    public float fireRate = 0.2f;        // Minimum seconds between shots
    public float DespawnTime = 1f;       // How long (seconds) before the projectile is destroyed

    private float nextFireTime;      // Earliest Time.time at which the gun can fire again
    private float lastTriggerValue;  // Trigger value from the previous frame, used for edge detection

    private void Update()
    {
        // Resolve the correct XR input device based on the assigned hand
        var controller = InputDevices.GetDeviceAtXRNode(hand == Hand.Left ? XRNode.LeftHand : XRNode.RightHand);
        controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);

        // Fire on the rising edge of the trigger (i.e. the moment it crosses the threshold,
        // not every frame it's held down) and only if the fire rate cooldown has elapsed
        if (triggerValue > 0.8f && lastTriggerValue <= 0.8f && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate; // Start the cooldown timer
        }

        // Store this frame's trigger value so next frame can detect the rising edge
        lastTriggerValue = triggerValue;
    }

    private void Fire()
    {
        // Safety guard — don't attempt to fire if essential references are missing
        if (projectilePrefab == null || muzzlePoint == null) return;

        // Spawn the projectile at the muzzle, inheriting its position and rotation
        GameObject projectile = Instantiate(projectilePrefab, muzzlePoint.position, muzzlePoint.rotation);

        // Tag the projectile so other scripts (e.g. hit detection) can identify it
        projectile.tag = "Fist";

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Ensure physics simulation is active on the projectile

            // VelocityChange ignores the Rigidbody's mass, so projectileSpeed is the exact
            // resulting velocity in m/s regardless of how heavy the projectile is
            rb.AddForce(muzzlePoint.forward * projectileSpeed, ForceMode.VelocityChange);
        }
        else
        {
            Debug.Log("No rigidbody found on projectile!");
        }

        // Auto-destroy the projectile after DespawnTime seconds to avoid scene clutter
        Destroy(projectile, DespawnTime);
    }
}