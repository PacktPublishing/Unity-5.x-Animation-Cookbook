using UnityEngine;
using System.Collections;

public class NavAgentWithRigidBody : MonoBehaviour {
    
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
        //We turn off the position and rotation updating in the Nav Mesh Agent component,
        //because we will use root motion for that
        agent.updatePosition = false;
        agent.updateRotation = false;

        //We calculate the angle between the forward axis of our character and the desired velocity vector of the agent. We multiply the angle by the sign (1 or -1) of the 
        //dot product of our desired velocity vector and our character's right axis. This dot will be greater than 0 if our desired velocity vector points towards
        //the right axis of our character, and it will be less than 0, when it points in the left direction. 
        direction = Vector3.Angle(transform.forward, agent.desiredVelocity) * Mathf.Sign(Vector3.Dot(agent.desiredVelocity, transform.right));

        //Speed is the magnitude of our desired velocity vector
        speed = agent.desiredVelocity.magnitude;

        //We set the Direction and Speed parameters in our Animator Controller to make the character move. 
        anim.SetFloat("Direction", direction, 0.2f, Time.deltaTime);
        anim.SetFloat("Speed", speed, 0.2f, Time.deltaTime);


        //We force the agent position to be the same as the transform position (to make them synchronized)
        agent.nextPosition = transform.position;
    }
}

