using UnityEngine;
using System.Collections;

public class PickUpIK : MonoBehaviour {

    //We store the reference to the target transform in this variable
    public Transform ikTarget;
    Animator anim;
    float ikWeight = 0f;
	void Start () {

        anim = GetComponent<Animator>();

	}

    void Update()
    {
        //We play the Pickup animation when player presses Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Pickup");
        }
    }
    //This function is called in the IK Pass (after all animations were evaluated)
	void OnAnimatorIK (int layerIndex) {

        //We set the ikWeight to the value of the PickupIK Animation Curve defined in the Pickup animation
        ikWeight = anim.GetFloat("PickupIK");
        //We set the IK Position for left hand to the ikTarget transform's position
        anim.SetIKPosition(AvatarIKGoal.LeftHand, ikTarget.position);
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
	
	}
}
