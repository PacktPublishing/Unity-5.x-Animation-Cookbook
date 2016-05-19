using UnityEngine;
using System.Collections;

public class RootMotionSteering : MonoBehaviour {

    //This variable stores the reference to the camera placed in the scene
    //we will move the character relative to the camera
    public Transform cameraTransform;

    //This variable stores the reference to the Animator component of this game object
    Animator anim;

    //These variables store Horizontal and Vertical input values
    float hor = 0f;
    float ver = 0f;

    //This variable is used to calculate the speed value and set the Speed parameter in the Animator Controller
    //It is a public variable, because we want to use it in another script later
    [HideInInspector]
    public float speed = 0f;

    //This variable is used to calculate the direction we want to go to and set the Direction parameter in the Animator Controller
    //It is a public variable, because we want to use it in another script later
    [HideInInspector]
    public float direction = 0f;

    //This vector will point in the same direction as camera's forward, but will be completely horizontal (with the Y axis set to 0)
    //We are going to use it to move our character. 
    Vector3 cameraHorizontalForward;

    //This vector will be our desired move direction
    Vector3 desiredMoveDirection;

	void Start () {

        //We assign the Animator component to our anim variable when the game starts
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //We set the hor and ver input values
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        //We calculate the horizontal camera forward direction, we use only horizontal axes
        cameraHorizontalForward = new Vector3(cameraTransform.forward.x, 0f, cameraTransform.forward.z).normalized;

        //We use player input stored in ver and hor variables along with horizontal camera forward vector and camera right vector to create
        //the desired move direction vector - this is a vector in world space in which we want to move our character
        desiredMoveDirection = ver * cameraHorizontalForward + hor * cameraTransform.right;

        //We calculate the angle between the forward axis of our character and the desired move vector. We multiply the angle by the sign (1 or -1) of the 
        //dot product of our desired move direction vector and our character's right axis. This dot will be greater than 0 if our desired move vector points towards
        //the right axis of our character, and it will be less than 0, when i points in the left direction. 
        direction = Vector3.Angle(transform.forward, desiredMoveDirection) * Mathf.Sign(Vector3.Dot(desiredMoveDirection, transform.right));

        //Speed is the magnitude of our desired move vector
        speed = desiredMoveDirection.magnitude;

        //We set the Direction and Speed parameters in our Animator Controller to make the character move. 
        anim.SetFloat("Direction", direction, 0.2f, Time.deltaTime);
        anim.SetFloat("Speed", speed, 0.2f, Time.deltaTime);
    }
}
