using UnityEngine;
using System.Collections;

public class EdgeTrigger : MonoBehaviour {
    //This is the transform that will work aa the target for our character
    //when we grab the edge. Our character will math this rooTarget transform's 
    //position and rotation.
    public Transform rootTarget;

    //This variable holds the distance in which we will re-enable the trigger after it was used. 
    //We need a method to disable and enable the trigger to prevent our character from grabbing the
    //edge when we don't want it. 
    public float reEnableDistance = 2f;
    //We store the player transform in this variable, after our character enters the trigger. 
    //It is used to check the distance from the character to the rootTarget, to re-enable the trigger.
    Transform playerTransform;
    //This flag tells us that we want to start checking the distance to re-enable the trigger.
    bool checkToEnable = false;

    //This function is called when a rigid body enters the trigger
	void OnTriggerEnter (Collider other) {

        //We are checking if the entering object has the Player tag (to make sure it is our character)
        if (other.gameObject.CompareTag("Player"))
        {
            //We store the playerTransform to use it later for checking the distance
            playerTransform = other.gameObject.transform;

            //We get Animator and EdgeGrab components. EdgeGrab is the script that 
            //handles grabbing the edge on our character's side. 
            Animator anim = other.gameObject.GetComponent<Animator>();
            EdgeGrab grab = other.gameObject.GetComponent<EdgeGrab>();
      
            if (grab != null )
            {
                //We check if our character is plaing the InAir or Jump animations. 
                //Only then we want to trigger the Grab function. 
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.InAir")
                    || anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Jump"))
                {
                    //We trigger the Grab() function in the EdgeGrab script attached to the player
                    //It handles the grab on the character's side. 
                    grab.Grab(rootTarget);

                    //We disable the trigger's collider to prevent the character from grabbing the edge
                    //again, after it climbs it. We also set the checkToEnable flag to start checking when
                    //we can re-enable the trigger.
                    gameObject.GetComponent<Collider>().enabled = false;
                    checkToEnable = true;
                }
            }
           
        }

	}
    void Update()
    {
        //If the checkToEnable flag is true, we check the distance between our player character
        //and the rootTarget transform. If it is bigger than the reEnableDistance, we enable the 
        //trigger again and stop checking further. 
        if (checkToEnable)
        {
            if (playerTransform == null)
            {
                gameObject.GetComponent<Collider>().enabled = true;
            }
            else if((playerTransform.position - rootTarget.position).magnitude > reEnableDistance)
            {
                checkToEnable = false;
                gameObject.GetComponent<Collider>().enabled = true;
            }
        }
    }
}
