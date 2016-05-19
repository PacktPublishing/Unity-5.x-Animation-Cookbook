using UnityEngine;
using System.Collections;

public class DirectBlendShapes : MonoBehaviour {

    public int blendShapeIndex = 0;
    public float blendShapeWeight = 0f;

    SkinnedMeshRenderer skinnedRenderer;

	void Start () {

        skinnedRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
	
	void Update () {

        //We can change the blend shape weight via scripts, but we need to know
        //the index of the blend shape in the blend shapes array.
        skinnedRenderer.SetBlendShapeWeight(blendShapeIndex, blendShapeWeight);

    }
}
