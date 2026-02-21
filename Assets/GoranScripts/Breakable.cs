using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    // Private variable for checking Tag
    [SerializeField] private string requiredObjectTag = "Fist";
    


    void OnTriggerEnter(Collider other)
    {
        // Checking object for the right tag
        if (other.gameObject.CompareTag(requiredObjectTag))
        {
            Debug.Log("collision");
            BreakWall();
        }
    }

    void BreakWall()
    {
        Debug.LogError("breakit");
        // Disable the wall
        gameObject.SetActive(false);
    }
}
