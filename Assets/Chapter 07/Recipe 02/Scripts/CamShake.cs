using UnityEngine;
using System.Collections;

public class CamShake : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            //We set the Shake trigger in the animator when player presses space
            //this triggers the shake animation
            anim.SetTrigger("Shake");
        }

	}
}
