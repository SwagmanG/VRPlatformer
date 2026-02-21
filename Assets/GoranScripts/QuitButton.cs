using UnityEngine;

public class QuitButton : MonoBehaviour
{
    //function that checks for an object to collide it
    private void OnTriggerEnter(Collider other)
    {
        //checks if the object is taggedf as fist
        if (other.CompareTag("Fist"))
        {
            // if true it quits the application
            Application.Quit();
        }
    }
}