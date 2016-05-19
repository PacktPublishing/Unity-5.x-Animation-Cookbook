using UnityEngine;
using System.Collections;

public class AutomaticDoor : MonoBehaviour {

    /*The anim variable is used to store the reference
    to the Animator component*/
    private Animator anim;

    void Start()
    {
        /*We assign the Animator component of the parent object
        because this script is attached to the trigger, which
        is the child object of our animated doors*/
        anim = transform.parent.GetComponent<Animator>();
    }
	/* This function is called when a Rigidbody intersects with
    the collider attached to our game object for the first time.
    Our collider has to be set to trigger. The Collider other
    parameter stores information about the object which collided
    with our trigger (entered the trigger).*/
	void OnTriggerEnter (Collider other) {

        //Here we check the tag of the object entering the trigger
        if (other.gameObject.CompareTag("Player"))
        {
            /*If the tag equals "Player", we set the 
            bool parameter "Open" to true in our
            Animator Controller - that plays the open 
            animation and opens the doors*/

            anim.SetBool("Open", true);
        }
	
	}
	
    /* This function is called when a Rigidbody exists the trigger
    (stops colliding with our trigger collider).*/
	void OnTriggerExit (Collider other) {

        /*Again, we check if the object was the player*/
        if (other.gameObject.CompareTag("Player"))
        {
            /*If it's true, we set the bool parameter "Open" 
            to false in our Animator Controller. That plays
            the close animation and closes the doors.*/
            anim.SetBool("Open", false);
        }
    }
}
