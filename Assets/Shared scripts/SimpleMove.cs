using UnityEngine;
using System.Collections;

public class SimpleMove : MonoBehaviour {

    /*Here are the public variables that we can use to customize our character's movement:

    jumpVelocity - is the velocity that will be added in the Y axis of the character, when jumping
    movementSpeed - this is the maximum movement speed of the character
    rotationSpeed - this is the amount of degrees our character can rotate per second
    goundCheckTransform - this is a Transform attached to the character, we are using it to check if the character stands on ground
    maxGroundCheckDistance - this is the distance in meters in which we check the ground, down from the groundCheckTransform*/
    public float jumpVelocity = 5f;
    public float movementSpeed = 2f;
    public float rotationSpeed = 90f;
    public Transform groundCheckTransform;
    public float maxGroundCheckDistance = 0.3f;

    /*These private variables are used to store certain values:

    hor - stores the "Horizontal" axis input value (used for movement)
    ver - stores the "Vertical" axis input value (used for movement)
    rot - stores the "Mouse X" axis input value (used for rotation)
    moveVector - stores the desired movement vector (created from hor and ver inputs)
    rb - stores the reference to the Rigidbody component attached to the game object
    anim - stores the Animator component attached to the game object
    jump - this bool flag is used to check if player pressed the "Jump" button*/

    private float hor = 0f;
    private float ver = 0f;
    private float rot = 0f;
    private Vector3 moveVector;
    private Rigidbody rb;
    private Animator anim;
    private bool jump = false;

    //Start function is called when the game starts
	void Start () {

        //We store the Rigidbody and Animator components in corresponding variables
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}

    //Update function is called every frame
    void Update()
    {
        /*We check if player pressed the "Jump" button. We need to do it in the Update function 
        instead of the FixedUpdate, because FixedUpdate has the tendency to lose GetButtonDown input*/
        if (Input.GetButtonDown("Jump"))
        {
            //If player presses the "Jump" button, we store it in the jump flag
            jump = true;
        }
    }

    //FixedUpdate is called every physics update - we use it to scrtipt Rigidbody based movement
	void FixedUpdate () {

        /*We check and store the input values. Those are axes, so we can safely check them in FixedUpdate
        because player has to hold them, to see the effect, and will not be able to notice any difference
        between processing them in Update or FixedUpdate. Make sure to define those inputs in the 
        Edit->Project Settings->Input section*/
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        rot = Input.GetAxis("Mouse X");

        /*We rotate the object's transform in the global Y axis (world's up vector) and use the 
        rot input and the rotationSpeed. We multiply it by the time between frames to rotate our object
        with constant speed per second*/

        transform.Rotate(Vector3.up * rot * rotationSpeed * Time.deltaTime);

        /*Here we calculate the desired moveVector. We use our inputs and object's forward and right axes.
        So the input is relative to the object's transform. The vector is normalized and multiplied 
        by movementSpeed to make the character move with the same speed in any direction (without it
        it would move faster on diagonals.*/
        moveVector = (transform.forward * ver + transform.right * hor).normalized * movementSpeed;

        /*We use the calculated moveVector to set the Rigidbody velocity. We are not changing
        the Y component, to make the gravitation work. Setting the Rigidbody velocity directly
        gives us more control than using forces and is a better option for characters*/
        rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);
      
        //We check if our character stands on ground
        if (GroundCheck())
        {
            //If so, we check if player pressed the "Jump" button (in the Update function)
            if (jump)
            {
                /*If it's true, we consume the jump flag, alter only the Rigidbody's velocity
                Y component, and trigger a jump animation (set in the AnimatorController)*/
                jump = false;
                rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
                anim.SetTrigger("Jump");
            }
        }

        /*We set the Speed parameter in the AnimatorController to play walk animation if our character is moving.
        We are using the Rigidbody's velocity vector lenght as our "Speed" value.*/

        if (anim != null)
        {
            anim.SetFloat("Speed", rb.velocity.magnitude);
        }
    }

    //This function checks if our character is grounded
    private bool GroundCheck()
    {
        /*We are using the Phusics.Raycast method to check if a ray shoot from the groundCheckTransform
        straight down, hits anything. If it does, we are grounded. The maxGroundDistance variable
        is used to determine the lenght of the ray*/
        if (Physics.Raycast(groundCheckTransform.position, Vector3.down, maxGroundCheckDistance))
        {
            /*If we stand on ground, we set the Ground parameter in the Animator Controller to true.
            It's used as the condition in the Jump->Idle and Jump->Run transitions*/
            if (anim != null)
            {
                anim.SetBool("Ground", true);
            }

            //We return a true value, if we are grounded.
            return true;
        }

        /*If we are not grounded, we set the Ground parameter in the Animator Controller to false,
        consume the jump flag to disallow a jump and return a false value*/
        if (anim != null)
        {
            anim.SetBool("Ground", false);
        }
        jump = false;
        return false;
    }
}
