using UnityEngine;
using System.Collections;

public class ChangeShape : MonoBehaviour {

    public float blendShapeLerpSpeed = 1f;

    SkinnedMeshRenderer skinnedRenderer;
    
    float weight = 0f;
    float weightTarget = 0f;

    // Use this for initialization
    void Start () {

        skinnedRenderer = GetComponent<SkinnedMeshRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //When player presses space, we set a new value for the weightTarget variable.
            //This value can be set to 0 or 100 depending on the current value.
            //It is used to set the blend shape weight and change the shape of the object
            if (weightTarget < 50f)
            {
                weightTarget = 100f;
            }
            else if (weightTarget > 50f)
            {
                weightTarget = 0f;
            }
        }
        //Here we lerp the weight of the first Blend Shape  (we use only one Blend Shape in this example) 
        weight = Mathf.Lerp(weight, weightTarget, Time.deltaTime * blendShapeLerpSpeed);
        skinnedRenderer.SetBlendShapeWeight(0, weight);

	}
}
