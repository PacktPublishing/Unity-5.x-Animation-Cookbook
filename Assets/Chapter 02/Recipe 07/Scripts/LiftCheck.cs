using UnityEngine;
using System.Collections;

public class LiftCheck : MonoBehaviour {

    Elevator elevatorScript;
	// Use this for initialization
	void Start () {
 
        /*We try to find the Elevator script on the root transform (we are assuming this is the Elevator game object)*/
        elevatorScript =  transform.root.GetComponent<Elevator>();
	}
	
	// This function is called when our character enters the trigger
	void OnTriggerEnter (Collider other) {

        /*We check if we've found the Elevator script*/
        if (elevatorScript != null)
        {
            /*We check if the object which entered the trigger is the Player*/
            if (other.gameObject.CompareTag("Player"))
            {
                /*We enable the Elevator script (and enable the input)*/
                elevatorScript.enabled = true;
            }
        }
	}

    // This function is called when our character exits the trigger
    void OnTriggerExit(Collider other)
    {
        /*We check if we've found the Elevator script*/
        if (elevatorScript != null)
        {
            /*We check if the object which exited the trigger is the Player*/
            if (other.gameObject.CompareTag("Player"))
            {
                /*We disable the Elevator script (and disable the input)*/
                elevatorScript.enabled = false;
            }
        }
    }
}
