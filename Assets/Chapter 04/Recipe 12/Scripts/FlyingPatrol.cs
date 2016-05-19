using UnityEngine;
using System.Collections;

public class FlyingPatrol : MonoBehaviour {

    public Transform[] patrolPoints;
    public float nextPatrolPointRange = 5f;
    Transform currentPatrolPoint;

    Animator anim;
    Vector3 desiredMoveDirection;
    Vector3 horizontalDirection;

    float horizontalAngle = 0f;
    float verticalAngle = 0f;
    float speed = 0f;

    int index = 0;

	void Start () {
        //Here we setthe currentPatrolPoint to be the first one defined in the patrolPoints[] array
        currentPatrolPoint = patrolPoints[0];

        //We also get the reference to the Animator component and store it in the anim variable
        anim = GetComponent<Animator>();
    }

    //This function is responsible for handling the partol route it checks if we are close to our current patrol point
    //and sets the next patrol point from our patrolPoints[] array
    void PatrolCheck()
    {
        if ((transform.position - currentPatrolPoint.position).magnitude <= nextPatrolPointRange)
        {
            index++;
            if (index >= patrolPoints.Length)
            {
                index = 0;
            }
            currentPatrolPoint = patrolPoints[index];
        }
    }

    //This function calculates and sets the Speed, DirectionHor and DirectionVer parameters in our 
    //Animator Controller
    void CalculateDirectionsAndSpeed()
    {
        //First we calculate the vector from our current position to the target (currentPatrolPoint) position
        desiredMoveDirection = currentPatrolPoint.position - transform.position;

        //Speed is simply the magnitude of this vector
        speed = desiredMoveDirection.magnitude;

        //We calculate the horizontal vector tu our target by setting the desiredMoveDirection's Y axis to 0
        horizontalDirection = new Vector3(desiredMoveDirection.x, 0f, desiredMoveDirection.z).normalized;

        //We use the horizontalDirection vector to calculate the angle between it and our forward axis. We use the Vector3.Angle() method
        //to get the angle and Vector3.Dot() method to get the sign of that angle (we check if the horizontalDirection vector points to the right,
        //or to the left of our character)
        horizontalAngle = Vector3.Angle(horizontalDirection, transform.forward) * Mathf.Sign(Vector3.Dot(horizontalDirection, transform.right));

        //We use the previously calculated horizontalDirection and desiredMoveDirection vectors to calculate vertical angle.
        //We use the Vector3.Dot() method again to get the sign of this angle (we check if it points up or down of our character)
        verticalAngle = Vector3.Angle(desiredMoveDirection, horizontalDirection) * Mathf.Sign(Vector3.Dot(desiredMoveDirection, transform.up));

        //We set all the required parameters in the Animator Controller to steer our character. 
        //We use dampTime parameters to make the blend our animations more smoothly. 
        anim.SetFloat("Speed", speed, 0.25f, Time.deltaTime);
        anim.SetFloat("DirectionHor", horizontalAngle, 0.5f, Time.deltaTime);
        anim.SetFloat("DirectionVer", verticalAngle, 0.5f, Time.deltaTime);

    }
	
	//We call PatrolCheck() and CalculateDirectionAndSpeed() functions every frame
	void Update () {

        PatrolCheck();
        CalculateDirectionsAndSpeed();


	}
}
