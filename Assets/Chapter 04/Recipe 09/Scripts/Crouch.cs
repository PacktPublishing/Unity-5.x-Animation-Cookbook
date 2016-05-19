using UnityEngine;
using System.Collections;

public class Crouch : MonoBehaviour {


    Animator anim;
    CapsuleCollider capsule;
    bool cantStandUp = false;

	void Start () {

        anim = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider>();

    }
	

	void Update () {
        
        //We check if the ceiling is more thay 2.25m higher than our character feet. 
        //This is an arbitrary value. We start checking 25cm above the ground to prevent from
        //hitting any unecessary obstacles. 
        cantStandUp = Physics.Raycast(transform.position+Vector3.up * 0.25f, Vector3.up, 2f);
        
        //We check if player holds the C key or if our character can't stand up
        if (Input.GetKey(KeyCode.C) || cantStandUp)
        {
            //We play Crouch animations
            anim.SetFloat("Crouch", 1f, 0.25f, Time.deltaTime);
            //We check if our Capsule Collider is not scaled down already
            //This is just because it doesn't make sense to scale it every frame to the same value
            if (capsule.height > 1.5f)
            {
                //If it's true, we scale the Capsule Collider and move it's center so it still
                //starts where the feet are. This makes our character's collision smaller.
                capsule.height = 1f;
                capsule.center = new Vector3(0f, 0.5f, 0f);
            }
        }
        else
        {
            //We stop playing Crouch animations
            anim.SetFloat("Crouch", 0f, 0.25f, Time.deltaTime);
            //We check if our Capsule Collider was scaled down before
            if (capsule.height < 1.5f)
            {
                //We scale our Capsule Collider back to it's original value (2 meters high)
                capsule.height = 2f;
                capsule.center = new Vector3(0f, 1f, 0f);
            }
        }
	
	}
}
