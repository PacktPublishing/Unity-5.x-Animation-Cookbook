using UnityEngine;
using System.Collections;

public class RandomAction : MonoBehaviour {

    public int numActions = 3;

	// Use this for initialization
	void Start () {

        //Here we create random number and set it as the value of the Random parameter in the Animator Controller
        //This parameter is used in a Blend Tree to play a randomized animation
        Animator anim = GetComponent<Animator>();
        int randomValue = Random.Range(0, numActions);
        anim.SetFloat("Random", (float)randomValue);
	}

}
