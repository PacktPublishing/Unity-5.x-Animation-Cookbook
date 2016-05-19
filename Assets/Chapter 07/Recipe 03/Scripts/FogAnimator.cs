using UnityEngine;
using System.Collections;

public class FogAnimator : MonoBehaviour {

    public float fogDensity = 0f;
    public Color fogColor = Color.gray;

    Animator anim;

	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}

	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //We set the ChangeFog trigger in the controller, when player presses Space.
            //This plays an animation in which we change the fogDensity and fogColor parameters. 
            anim.SetTrigger("ChangeFog");
        }

        //Here we set the fogColor and fogDensity to be the same as our script's public variables.
        //This way we can change fog's color and density by animating our script's public variables. 
        RenderSettings.fogColor = fogColor;
        RenderSettings.fogDensity = fogDensity;

        //Here we also change the camera's background color to match the fog's color
        Camera.main.backgroundColor = fogColor;
	
	}
}
