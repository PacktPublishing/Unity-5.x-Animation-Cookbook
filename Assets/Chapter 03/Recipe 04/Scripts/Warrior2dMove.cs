using UnityEngine;
using System.Collections;

public class Warrior2dMove : MonoBehaviour {

    float hor = 0f;
    Animator animator;
    float speed = 0f;

	void Start () {

        //Finding the Animator component and creating a reference to it
        animator = GetComponent<Animator>();

	}
	
	void Update () {

        hor = Input.GetAxis("Horizontal");
        speed = Mathf.Abs(hor);

        //Here we check if we are walking right or left. We change the local X scale of the
        //character to flip it instead of creating new animations for left and right side
        if (hor > 0f)
        {
            transform.localScale = Vector3.one;
          
        }
        else if (hor < 0f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
           
        }

        //Here we set the Speed parameter of the Animator Controller to switch between
        //Idle and Walk animations

        animator.SetFloat("Speed", speed);

        //We move the character only if the speed is greater than 0.1
        //because this is the condition for Walk animation to start playing
        if (speed > 0.1f)
        {
            transform.Translate(Vector3.right * hor * Time.deltaTime);
        }
        
	
	}
}
