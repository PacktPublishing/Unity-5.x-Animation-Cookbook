using UnityEngine;
using System.Collections;

public class PlayAndStop : MonoBehaviour {

    //This variable stores the reference to the character's Animator component
    public Animator characterAnimator;
    //This variable stores the reference to the object's Animator component
    public Animator objectAnimator;

    bool play = false;
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //When the player presses Space, we invert the PlayAnim variable in both Animator components at the same time
            play = !play;
            characterAnimator.SetBool("PlayAnim", play);
            objectAnimator.SetBool("PlayAnim", play);
        }
	
	}
}
