using UnityEngine;
using System.Collections;

public class CharacterLookAtIK : MonoBehaviour {

    //This variable stores the reference to the Animator component
    Animator anim;
    //This is the target our character will look at
    public Transform target;
    //This is the time in which we will damp the look at vector, to make it look more smooth
    public float dampTime = 0.2f;
    //This is the weight of the look at. If it's set to 0, the look at will be turned off
    public float weight = 1f;

    //We use a helper variable to be able to make the look at position changes more smooth
    Vector3 targetPosition;
    //This is a reference vector used by the Vector3.SmoothDamp() function
    Vector3 dampVelocity;
	
    //Start is called when the game starts
	void Start () {
	
        //We assign the anim variable when the game starts.
        anim = GetComponent<Animator>();
    }
	
	//This function is called after all animation states were evaluated. It is used for IK operations. 
    //You need to enable the IK Pass in the character's Animator Controller layer properties. 
	void OnAnimatorIK(int layerIndex) {
        //We check if our look at weight is less or equal than 0. If so, we don't do anything. 
        if (weight <= 0f)
        {
            return;
        }
        //Here we damp the changes of the look at targetPosition
        targetPosition = Vector3.SmoothDamp(targetPosition, target.position, ref dampVelocity, dampTime);
        //This function sets the look at position for a Humanoid character.
        anim.SetLookAtPosition(targetPosition);
        //This function sets the weight of the look at for a Humanoid character. 
        //If you want a smooth transition to and from look at state, lerp the weight variable in time. 
        anim.SetLookAtWeight(weight);
    }
}
