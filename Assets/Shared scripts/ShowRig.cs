using UnityEngine;
using System.Collections;

public class ShowRig : MonoBehaviour {
    public Transform rigRoot;
    public Color linesColor = Color.yellow;
    Transform[] allBones;
    void Start()
    {
        allBones = rigRoot.GetComponentsInChildren<Transform>();
    }
    //This script renders a line to from all rig's bones to all their parents.
    //This way we can simply visualize a rig without a mesh
	void OnDrawGizmos () {
        if (rigRoot == null)
        {
            return;
        }
        if (allBones == null)
        {
            allBones = rigRoot.GetComponentsInChildren<Transform>();
            return;
        }
        for (int i = 0; i < allBones.Length; i++)
        {
            if (allBones[i].parent != null)
            {
                Gizmos.color = linesColor;
                Gizmos.DrawLine(allBones[i].position, allBones[i].parent.position);
            }
        }

	}
	
}
