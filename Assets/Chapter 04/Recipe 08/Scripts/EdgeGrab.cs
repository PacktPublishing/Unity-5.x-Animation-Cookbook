using UnityEngine;
using System.Collections;

public class EdgeGrab : MonoBehaviour {

    Animator anim;
    Rigidbody rb;
    //We need references to those scripts to turn them off while we grab the edge. 
    //We don't want our character to move or jump when performing the edge grab action
    RootMotionSteering steeringScript;
    Jump jumpScript;
    //The trigger script is setting this variable for us in the Grab function. This is the target transform
    //that we want to match our position with
    Transform grabTarget;
    //This flag is used to determine wheter we should use Lerp function to adjust the character's position
    //when grabing an edge
    bool adjustPosition = false;
    //This is the speed multiplier in which we will interpolate the position and rotation of our character
    //to match the position of the edge
    public float lerpSpeed = 10f;

	void Start () {
        //Here we set all the references to the needed components
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        steeringScript = GetComponent<RootMotionSteering>();
        jumpScript = GetComponent<Jump>();
    }

    //This function is called from the trigger game object
    //It sets the grabTarget, makes our character play GrabEdge animations
    //turns on the GrabLerp method (with the adjustPosition flag)
    //and disables all the other scripts on our character. 
    //It also turns the Rigid Body to be kinematic, that turns off the collision.
    //We need our character's collision to be turned off, because it climbs 
    //the edge using animation. We also turn the root motion on, because we 
    //want to use animation for climbing the edge. 
    public void Grab (Transform target) {
        grabTarget = target;

        if (grabTarget != null)
        {
            anim.SetTrigger("GrabEdge");
            adjustPosition = true;
            steeringScript.enabled = false;
            jumpScript.enabled = false;
            rb.isKinematic = true;
            anim.applyRootMotion = true;
         
        }
	}
    //This function is called in FixedUpdate() and matches the position
    //and rotation of our character with the grabTarget transform. 
    void GrabLerp()
    {
        if (grabTarget == null || !adjustPosition)
        {
            return;
        }
        if ((transform.position - grabTarget.position).sqrMagnitude > 0.001f)
        {
            //We use the Lerp method with the Time.delta time instead of a finite timer, so we need
            //to check if we are close enough to our destination. If so, we stop the interpolation and
            //set the position and rotation to match our target. 
            transform.rotation = Quaternion.Lerp(transform.rotation, grabTarget.rotation, Time.deltaTime * lerpSpeed);
            transform.position = Vector3.Lerp(transform.position, grabTarget.position, Time.deltaTime * lerpSpeed);
         }
         else
         {
            transform.position = grabTarget.position;
            transform.rotation = grabTarget.rotation;
         }
        
    }
    //This function is used as an animation event in the GrabEdgeClimb animation
    //It's called after our character climbs the edge. It re-enables the scripts
    //and makes the rigid body non - kinematic again.
    public void StopGrab()
    {
        rb.isKinematic = false;
        steeringScript.enabled = true;
        jumpScript.enabled = true;
    }
    void FixedUpdate()
    {
        //We call GrabLerp() in FixedUpdate, because our character has a rigid body. 
        GrabLerp();
    }
    void Update()
    {
        //We set the PullUp trigger in the Animator, when player presses the Up Arrow key
        //Input should be implemented in the Update() function, as it would be less responsive in FixedUpdate()
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.GrabEdgeLoop"))
            {
                grabTarget = null;
                anim.SetTrigger("PullUp");
            }
        }
    }


}
