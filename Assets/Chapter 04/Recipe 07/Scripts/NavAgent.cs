using UnityEngine;
using System.Collections;

public class NavAgent : MonoBehaviour {

    //This variables stores the reference to the Nav Mesh Agent component
    NavMeshAgent agent;

    //This variable stores the reference to the Animator component
    Animator anim;

    //This variable is used to calculate the speed value and set the Speed parameter in the Animator Controller
    //It is a public variable, because we want to use it in another script later
    [HideInInspector]
    public float speed = 0f;

    //This variable is used to calculate the direction we want to go to and set the Direction parameter in the Animator Controller
    //It is a public variable, because we want to use it in another script later
    [HideInInspector]
    public float direction = 0f;

    // Use this for initialization
    void Start()
    {
        //We set the references to the Nav Mesh Agent component and the Animator component
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //We turn off the rotation updating in the Nav Mesh Agent component,
        //because we will use root motion for that
        agent.updateRotation = false;

        //We calculate the angle between the forward axis of our character and the desired move vector. We multiply the angle by the sign (1 or -1) of the 
        //dot product of our desired move direction vector and our character's right axis. This dot will be greater than 0 if our desired move vector points towards
        //the right axis of our character, and it will be less than 0, when i points in the left direction. 
        direction = Vector3.Angle(transform.forward, agent.desiredVelocity) * Mathf.Sign(Vector3.Dot(agent.desiredVelocity, transform.right));

        //Speed is the magnitude of our desired move vector
        speed = agent.desiredVelocity.magnitude;

        //We set the Direction, DirectionRaw, Speed and SpeedRaw parameters in our Animator Controller to make the character move. 
        anim.SetFloat("Direction", direction, 0.2f, Time.deltaTime);
        anim.SetFloat("DirectionRaw", direction);
        anim.SetFloat("Speed", speed, 0.2f, Time.deltaTime);
        anim.SetFloat("SpeedRaw", speed);
    }
    //This function is called every frame after all Animator states have been evaluated
    void OnAnimatorMove()
    {
        //We set the agent's velocity to be the equal to the root node velocity (delta position divided by the delta time).
        agent.velocity = anim.deltaPosition / Time.deltaTime;

        //We set the transform rotation to be the same as the root node rotation
        transform.rotation = anim.rootRotation;
    }

}
