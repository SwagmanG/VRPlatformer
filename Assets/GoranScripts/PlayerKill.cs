using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerKill : MonoBehaviour
{
    // Private variable for checking Tag
    [SerializeField] private string requiredObjectTag = "PlayerKill";

    void OnTriggerEnter(Collider other)
    {
        // Checking object for the right tag
        if (other.gameObject.CompareTag(requiredObjectTag))
        {
            // Player is immune while the level is being moved
            if (LevelMover.IsMoving)
            {
                Debug.Log("Level is moving - player is immune!");
                return;
            }

            Debug.Log("collision");
            ResetScene();
        }
    }

    void ResetScene()
    {
        Debug.LogError("breakit");
        // Reset Scene
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadScene(currentScene.name, LoadSceneMode.Single);
    }
}