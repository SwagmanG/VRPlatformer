using UnityEngine;

public class HatchTrigger : MonoBehaviour
{
    // Tag the triggering collider must have for this trigger to fire (defaults to "Player")
    [SerializeField] private string requiredObjectTag = "Player";

    // The hatch GameObjects to hide when the trigger fires — typically geometry blocking a passage
    [SerializeField] private GameObject[] hatches;

    // An optional object to reveal when the trigger fires (e.g. a ladder, elevator, or UI prompt)
    [SerializeField] private GameObject objectToEnable;

    // Called by Unity when a collider enters this trigger volume
    void OnTriggerEnter(Collider other)
    {
        // Ignore anything that isn't the required tag (e.g. debris, enemies)
        if (other.gameObject.CompareTag(requiredObjectTag))
        {
            EnableObject();   // Reveal the associated object
            OpenHatches();    // Remove the hatches to open the passage
            gameObject.SetActive(false); // Deactivate the trigger so it can only fire once
        }
    }

    // Reveals objectToEnable, if one has been assigned in the Inspector
    void EnableObject()
    {
        if (objectToEnable != null)
            objectToEnable.SetActive(true);
        else
            Debug.LogWarning("No object to enable assigned!");
    }

    // Deactivates all hatch GameObjects, effectively opening them
    void OpenHatches()
    {
        if (hatches.Length == 0)
        {
            Debug.LogWarning("No hatches assigned!");
            return;
        }

        foreach (GameObject hatch in hatches)
        {
            if (hatch != null)
                hatch.SetActive(false); // Hide the hatch — SetActive(false) rather than Destroy
                                        // so it can be re-enabled later if needed
            else
                Debug.LogWarning("A hatch in the array is null!");
        }
    }
}