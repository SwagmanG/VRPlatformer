using UnityEngine;
using UnityEngine.Splines;

public class SplineMover : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    [SerializeField] private float speed = 1f;
    [SerializeField] private bool pingPong = true;

    private float splineProgress = 0f;
    private int direction = 1; //1 for forward, -1 for backward (direction local to spline)
        
    //Update is called once per frame
    void Update()
    {
        //Checks for null value
        if (splineContainer == null) return;

        //Update position along spline (0 to 1)
        splineProgress += direction * speed * Time.deltaTime;
 
        //Handles ping-pong back and forth motion
        if (pingPong)
        {
            if (splineProgress >= 1f)
            {
                splineProgress = 1f;
                direction = -1;
            }
            else if (splineProgress <= 0f)
            {
                splineProgress = 0f;
                direction = 1;
            }
        }
        else
        {
            //Loop mode
            splineProgress = Mathf.Repeat(splineProgress, 1f);
        }

        //Get position and rotation from spline
        Vector3 position = splineContainer.EvaluatePosition(splineProgress);
        Vector3 tangent = splineContainer.EvaluateTangent(splineProgress);

        //Update Transform
        transform.position = position;

        //Orient object along spline direction
        if (tangent != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(tangent);
        }
    }
}
