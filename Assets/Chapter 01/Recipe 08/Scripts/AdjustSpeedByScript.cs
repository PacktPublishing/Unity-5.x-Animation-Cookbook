using UnityEngine;
using System.Collections;

public class AdjustSpeedByScript : MonoBehaviour {

    //This is a variable, in which we store the reference to the Animator component
    private Animator anim;
    //We store the wanted animation speed in this variable, the default value is 2 (200%).
    public float newAnimationSpeed = 2f;
	void Start () {

        //At the start of the game we assign the Animator component to our anim variable
        anim = GetComponent<Animator>();
	}
	
	void Update () {

        //We check if player pressed the Space button
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //And set the playback speed of the whole Animator Controller (it multiplies all states animation playback speed)
            anim.speed = newAnimationSpeed;

            /*If you want to change the speed of just one animation state, 
            add a float parameter to your Animator Controller, use this parameter
            in the Multiplier field in the animation state Inspector
            and change the parameter using:

            anim.SetFloat("YourParameterName", newAnimationSpeed);

            function, where YourParamterName is the name of your paramter in the Animator Controller,
            and newAnimationSpeed is the float value you want to set the parameter and playback speed to*/ 
        }
	
	}
}
