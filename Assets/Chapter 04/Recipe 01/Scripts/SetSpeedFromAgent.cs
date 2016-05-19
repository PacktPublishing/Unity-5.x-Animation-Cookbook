using UnityEngine;
using System.Collections;

public class SetSpeedFromAgent : MonoBehaviour {

    //We store the reference to the NavmeshAgent component in this variable
    NavMeshAgent agent;
    
    //We store the reference to the Animator component in this variable
    Animator anim;
	
	void Start () {
        //We assign the NavMeshAgent component to our agent variable
        agent = GetComponent<NavMeshAgent>();

        //We assign the Animator component to our anim variable
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            agent.speed = 4f;
        }
        else
        {
            agent.speed = 1f;
        }
        
        //We set the "Speed" parameter of the Animator Controller to the magnitude of the agent's velocity.
        //This is used by the Blend Tree to blend between walk and run animations. 
        //We are using the damp time (0.2 seconds) to damp any sudden changes and make sure our blends look smooth.
        anim.SetFloat("Speed", agent.desiredVelocity.magnitude, 0.2f, Time.deltaTime);
	
	}
}
