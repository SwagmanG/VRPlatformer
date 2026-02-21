using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    //Variable for naming a scene to load.
    [SerializeField] private string sceneToLoad;

    //Function to handle scene loading when a player enters.
    private void OnTriggerEnter(Collider other)
    {
        //checks for an object with the player tag
        if (other.CompareTag("Player"))
        {
            //if true it loads the named scene.
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
