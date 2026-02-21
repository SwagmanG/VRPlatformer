using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    //variable tyo load scene by name
    [SerializeField] private string sceneToLoad;

    //function to handle code when an object enters a collider
    private void OnTriggerEnter(Collider other)
    {
        //checks if the object has the fist tag
        if (other.CompareTag("Fist"))
        {
            // loads scene if true
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
