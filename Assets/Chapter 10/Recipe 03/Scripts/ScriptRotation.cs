using UnityEngine;
using System.Collections;

public class ScriptRotation : MonoBehaviour {

    public float rotationSpeed = 90f;
    public Vector3 rotateAxis = Vector3.forward;
    public Space rotationSpace = Space.World;


	// Update is called once per frame
	void Update () {
        //The Rotate() function rotates the transform by a given degree,
        //around a given axis. The rotation can be handled in world space or in
        //local space.
        transform.Rotate(rotateAxis * rotationSpeed * Time.deltaTime, rotationSpace);
    }
}
