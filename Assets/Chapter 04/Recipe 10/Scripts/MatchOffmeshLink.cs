using UnityEngine;
using System.Collections;

public class MatchOffmeshLink : MonoBehaviour {

    Animator anim;
    NavMeshAgent agent;
    Rigidbody rb;


    Vector3 targetPos = Vector3.zero;
    Vector3 targetDirection;
    Quaternion targetRot;
    AnimatorStateInfo currentAnimState;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
	
	}

    //This function is called from an Animation Event
    public void CompleteOffMeshLink()
    {
        //We tell the NavMeshAgent component that we've completed the OffMeshLink
        //We also make sure to set the JumpOffMesh parameter to false and to set the Rigid Body component
        //to normal
        agent.CompleteOffMeshLink();
        anim.SetBool("JumpOffMesh", false);
        rb.isKinematic = false;
    }
	
	// Update is called once per frame
	void Update () {

        //Here we save the curren AnimatorStateInfo
        currentAnimState = anim.GetCurrentAnimatorStateInfo(0);

        //We check if our agent is trying to travers an Offmesh Link
        if (agent.isOnOffMeshLink)
        {
            //If so we calculate the target position and target rotation of the character
            //We also start playing the JumpOffMesh animation
            anim.SetBool("JumpOffMesh", true);
            targetPos = agent.currentOffMeshLinkData.endPos;
            targetDirection = targetPos - transform.position;
            targetDirection.y = 0f;
            targetRot = Quaternion.LookRotation(targetDirection);

        }
        //We check if we are playing the JumpOffMesh animation and if we are not in a transition
        if (currentAnimState.IsName("Base Layer.JumpOffMesh") && !anim.IsInTransition(0))
        {
            //We set the Rigid Body component to kinematic to turn off collisions and gravity
            rb.isKinematic = true;
 
            //We use the MatchTarget function on the Animator (it needs to be called every frame) to
            //match our character's root node with the target position (the end of the OffMeshLink. 
            //We set the weights to 1 and we set the start normalized time to 0.25f and end normalized time to 0.75f.
            //This is because we have blends between animations. You may need to adjust those values in your particular case. 
            anim.MatchTarget(targetPos, targetRot, AvatarTarget.Root, new MatchTargetWeightMask(Vector3.one, 1f), 0.25f, 0.75f);
        }
    }
}
