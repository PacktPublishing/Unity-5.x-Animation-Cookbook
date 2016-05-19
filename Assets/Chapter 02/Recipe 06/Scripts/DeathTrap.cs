using UnityEngine;
using System.Collections;

public class DeathTrap : MonoBehaviour {

	
    /*This function is called when a Rigidbody enters the trigger object*/
	void OnTriggerEnter(Collider other) {

        /*We are checking if the object which entered the trigger
        has a Character script, if so we are calling the Kill() method on it*/
        Character characterScript = other.gameObject.GetComponent<Character>();

        if (characterScript != null)
        {
            characterScript.Kill();
        }
	
	}
}
