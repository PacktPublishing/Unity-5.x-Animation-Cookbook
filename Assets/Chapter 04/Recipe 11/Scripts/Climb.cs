using UnityEngine;
using System.Collections;

public class Climb : MonoBehaviour {

    Transform climbNode;
    //We use this variable to set the speed of position adjustement when our character starts climbing
    public float lerpSpeed = 10f;
    //This is the time after which we will turn on the normal movement, 
    //when character ends climbing
    public float climbEndAnimLenght = 3f;
    Animator anim;
    Rigidbody rb;
    RootMotionSteering steeringScript;

    bool canClimb = false;

    void Start()
    {
        //Here we assign the references to Animator, Rigidbody  components
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    //This function is called from a trigger set on the level
    public void StartClimbing(Transform startNode)
    {
        //We turn of rigidbody physics, set the startNode for climbing, 
        //play the Idle to Climb animation, start to adjust the position with a couritine
        rb.isKinematic = true;
        climbNode = startNode;
        anim.SetBool("Climb", true);
        StartCoroutine("AdjustPosition");
      
    }
    IEnumerator AdjustPosition()
    {
        //We check if our character is close enough to the target climbNode (the climb start position)
        //If not, we adjust the position and wait one frame.
        while((transform.position - climbNode.position).magnitude > 0.05f)
        {
            yield return null;
            transform.position = Vector3.Lerp(transform.position, climbNode.position, Time.deltaTime * lerpSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, climbNode.rotation, Time.deltaTime * lerpSpeed);
        }
        transform.position = climbNode.position;
        transform.rotation = climbNode.rotation;
        //If our player is in the start node position, we enable the climb input. 
        canClimb = true;
    }

    //This function is called from a trigger set on the level
    public void EndClimbing()
    {
        //We disable climb movement, play the Climb End animation and start a couritine to enable the rigidbody physics again
        canClimb = false;
        anim.SetBool("Climb", false);
        StartCoroutine("ReEnableMovement");

    }

    IEnumerator ReEnableMovement()
    {
        //We wait for the climb animation to end and enable the rigidbody physics again
        yield return new WaitForSeconds(climbEndAnimLenght);
        rb.isKinematic = false;
    }


	
	// Update is called once per frame
	void Update () {

        //We check if our character can climb, and if player holds the Up Arrow.
        //If so, we play the ClimbUp animation.
        if (canClimb && Input.GetAxis("Vertical") > 0f)
        {
            anim.SetBool("ClimbUp", true);
        }
        else
        {
            anim.SetBool("ClimbUp", false);
        }

	}
}
