using UnityEngine;
using System.Collections;

public class SetRawDirectionAndSpeed : MonoBehaviour {


    //We are storing the reference to the RootMotionSteering script in this variable.
    //RootMotionSteering script is responsible for calculating the desired speed and direction
    //of our character
    RootMotionSteering steeringScript;

    //We store the reference to the Animator component in this variable
    Animator anim;

	void Start () {

        //We assing the RootMotionSteering script and Animator component to our variables when the game starts
        steeringScript = GetComponent<RootMotionSteering>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        //We set the DirectionRaw and SpeedRaw parameters to be equal to the calculated direction and speed (without any damping)
        anim.SetFloat("DirectionRaw", steeringScript.direction);
        anim.SetFloat("SpeedRaw", steeringScript.speed);

	
	}
}
