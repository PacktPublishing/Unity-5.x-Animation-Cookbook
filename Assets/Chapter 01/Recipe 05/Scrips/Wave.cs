using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

    //This is a variable storing a reference to the Animator component 
    //assigned to the same object the script is assigned to
    private Animator anim;
	
	void Start () {

        //Here we get the Animator component and assign it to the anim variable
        anim = GetComponent<Animator>();

	}
	
	void Update () {

        //This if statement checks whether the Space key was pressed down
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //This function sets the trigger parameter "Wave" on the Animator Controller
            //assigned to our Animator component (referenced in the anim variable)
            anim.SetTrigger("Wave");
        }
	
	}
}
