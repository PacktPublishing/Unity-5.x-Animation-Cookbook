using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

    public float jumpForceUp = 10f;
    public float jumpForceForward = 20f;
    public float additionalGravity = 10f;
    public float maxGroundCheckDistance = 0.3f;
    public float groundCheckPauseTime = 0.5f;
    public Transform groundCheck;
    Animator anim;
    Rigidbody rb;

    public bool onGround = true;
    float lastJumpTime = 0f;
	// Use this for initialization
	void Start () {

        //We store the reference to the Animator and the Rigidbody components when the game starts
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        //We check if player pressed SPACE and if the character is on ground
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            //If the character is on ground, we save the lastJumpTime to pause the GroundCheck() function for a moment.
            lastJumpTime = Time.time;
            //We turn off the root motion in the Animator to make the character be moved only with physics.
            anim.applyRootMotion = false;
            //We set the onGround variable to false, because our character is in the air. 
            onGround = false;
            //We apply the force to the Rigid Body component of the character to make it jump
            rb.AddForce(Vector3.up* jumpForceUp + transform.forward * jumpForceForward, ForceMode.Impulse);
            //We trigger the Jump Trigger in the Animator Controller to make the character play jump animation
            anim.SetTrigger("Jump");
        }
        GroundCheck();
	}

    //This function checks if the character stands on ground
    void GroundCheck()
    {
        //First we check if the groundCheckPause time has passed from the lastJumpTime
        //We need to pause the GroundCheck() for a short amount of time after jump
        //to let the character be in air
        if(Time.time > lastJumpTime + groundCheckPauseTime  &&
            Physics.Raycast(groundCheck.position, Vector3.down, maxGroundCheckDistance))
        { 
            //If we're on ground we set the onGround variable and turn on the root motion in the Animator component
            onGround = true;
            anim.applyRootMotion = true;
        }
        else
        {
            onGround = false;
        }

        //We set the OnGround parameter in the Animator Controller accordingly to the onGround variable
        anim.SetBool("OnGround", onGround);
    }

    void FixedUpdate()
    {
        //We add some additional gravity force to make the character land faster
        if (additionalGravity > 0f && !onGround)
        {
            rb.AddForce(Vector3.down * additionalGravity, ForceMode.Force);
        }
    }
}
