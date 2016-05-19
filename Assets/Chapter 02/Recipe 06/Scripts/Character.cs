using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    /*We are going to store the reference to a blood effect prefab in this variable*/
    public GameObject bloodEffect;

    /*This variable is set to true when the character object was already killed*/
    bool isKilled = false;

	/*This function is called by the death trap,
    when we enter it*/
	public void Kill () {
        /*If the character was already killed by the trap,
        we don't want to do anything*/
        if (isKilled)
        {
            return;
        }
        /*If it was not killed, we set the isKilled
        variable to true*/
        isKilled = true;

        /*We check if the character has a Rigidbody component*/
        Rigidbody rb = GetComponent<Rigidbody>();

        if (rb != null)
        {

            /*If we find the component, we need to set it to kinematic
            to prevent our character from being launched in the air by
            the collision with our trap*/
            rb.isKinematic = true;
        }

        /*Here we spawn a blood effect prefab stored in the bloodEffect variable*/
        GameObject.Instantiate(bloodEffect, transform.position + Vector3.up*2f, Quaternion.identity);

        /*We are getting all the Renderer components of our character*/
      
        Renderer[] r = GetComponentsInChildren<Renderer>();

        for(int i = 0; i < r.Length; i++)
        {
            /*We are turning all the renderers of, making the object dissapear*/
            r[i].enabled = false;
        }

        /*We are also checking if our character uses our SimpleMove script
        if so, we are turning it off to prevent player from moving the character
        after death*/
        SimpleMove move = GetComponent<SimpleMove>();

        if (move != null)
        {
            move.enabled = false;
        }

	}
}
