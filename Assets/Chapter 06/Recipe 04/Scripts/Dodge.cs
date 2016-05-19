using UnityEngine;
using System.Collections;

public class Dodge : MonoBehaviour {


    Animator anim;
	void Start () {

        anim = GetComponent<Animator>();

	}
	

	void Update () {

        //We trigger the Dodge parameter when player presses Space. 
        //Root motion does the rest of the job
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Dodge");
        }

	}
}
