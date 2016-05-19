using UnityEngine;
using System.Collections;

public class ActionPoint : MonoBehaviour {

    public NavMeshAgent agent;
    public string actionTrigger = "Action";
    public bool matchPosition = true;
    public bool matchRotation = true;
    public bool matchBeforeAction = true;
    public float actionDistance = 1f;
    public float lerpSpeed = 1f;

    Transform agentTransform;
    Animator anim;

	void Start () {

        agentTransform = agent.transform;
        anim = agentTransform.GetComponent<Animator>();

        //We start the PerformAction coroutine when the game starts.
        //You can also start it after the player clicks on the action point. 
        StartCoroutine("PerformAction");
    }
	//This coroutine matches the character's position and rotation to the action point's position and rotation
	IEnumerator PerformAction () {
        
        //We check if our character is close enough to the action point
        while ((agentTransform.position - transform.position).sqrMagnitude > actionDistance * actionDistance)
        {
            //If not, we set the Nav Mesh Agent's destination to the action point's position
            agent.SetDestination(transform.position);
            agent.Resume();
            yield return null;
        }
        //If we are close enough we stop the Nav Mesh Agent
        agent.Stop();
        if (!matchBeforeAction)
        {
            //If we want don't want to match the character's position and rotation before we start the action,
            //we can start the action now
            anim.SetTrigger(actionTrigger);
        }
        while (matchRotation == true || matchRotation == true)
        {
          
            yield return null;
            //If we need to match the position, we check if our character is further away than an arbitrary "error value"
            if (matchPosition && (agentTransform.position - transform.position).sqrMagnitude > 0.01f)
            {
                //We interpolate the position of our character to match the position of the action point
                agentTransform.position = Vector3.Lerp(agentTransform.position, transform.position, Time.deltaTime * lerpSpeed);
            }
            else if(matchPosition)
            {
                //If we are close enough, we set the position of the character to exactly match the position of the action point
                matchPosition = false;
                agentTransform.position = transform.position;
            }
            //If we need to match the rotation, we check the angle between our character's forward axis and our action point's forward axis.
            if (matchRotation && Vector3.Angle(agentTransform.forward, transform.forward) > 1f)
            {
                //If it's greater than 1 degree, we interpotale the character's rotation to match the action point's rotation
                agentTransform.rotation = Quaternion.Lerp(agentTransform.rotation, transform.rotation, Time.deltaTime * lerpSpeed);
            }
            else
            {
                //If the difference is less than 1 degree, we set the character's rotation to match exactly the action point's rotation
                agentTransform.rotation = transform.rotation;
                matchRotation = false;
            }
        }
        //If we wanted to match before we start playing the action, we play the action now
        if (matchBeforeAction)
        {
            anim.SetTrigger(actionTrigger);
        }
	}
}
