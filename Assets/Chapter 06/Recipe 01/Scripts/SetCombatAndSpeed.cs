using UnityEngine;
using System.Collections;

public class SetCombatAndSpeed : MonoBehaviour {

    Animator anim;
    bool combat = false;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
	
	}
	

	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //We invert the Combat parameter in the controller every time
            //player presses Space
            combat = !combat;
            anim.SetBool("CombatState", combat);
        }

        //If player holds the Up Arrow, we set the Speed parameter to 1 to make our character
        //play walk animation. 
        if (Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetFloat("Speed", 1f);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }

	
	}
}
