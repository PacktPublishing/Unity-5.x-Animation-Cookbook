using UnityEngine;
using System.Collections;

public class Tired : MonoBehaviour {

    public float tiredLerpSpeed = 1f;
    Animator anim;
    float weightTarget = 0f;
    float weight = 0f;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //When player presses space, we set a new value for the weightTarget variable.
            //This value can be set to 0 or 1 depending on the current value.
            //It is used to turn the additive layer on and off
            if (weightTarget < 0.5f)
            {
                weightTarget = 1f;
            }
            else if (weightTarget > 0.5f)
            {
                weightTarget = 0f;
            }
        }
        //Here we lerp the weight of the Additive layer (this layer's index is 1 in our controller) 
        weight = Mathf.Lerp(weight, weightTarget, Time.deltaTime * tiredLerpSpeed);
        //We set the current weight value for the Additive layer. This turns the layer on and off smoothly. 
        anim.SetLayerWeight(1, weight);
	}
}
