using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    /*This scrip makes a camera rig (a game object with a camera 
    parented to it) follow a target. A camera rig is an object to which we parent the camera to. 
    We assume that the rig's position will be set to the target object's position.*/

    public Transform target;                //This variable stores the target Transform - our moving character
    public float smoothingTime = 0.2f;      //This is the time, the rotation of the camera rig will be smoothed in
    public bool adjustRotation = true;      //Makes the camera rig face the same way as the target object
    private Vector3 refVelocity;            //This is a variable used by the SmoothDamp function

    //We use FixedUpdate, because our character is using Rigidbody component to move
	void FixedUpdate () {

        //We attach the camera rig to the target (we copy the target position)
        transform.position = target.position;

        //Here we check if we want to adjust the rotation of the camera rig
        if (adjustRotation)
        {
            //Here we are forcing the camera rig to face the direction the target is facing, but we are smoothing this direction with the SmoothDamp function
            transform.LookAt(Vector3.SmoothDamp(transform.position + transform.forward, target.position + target.forward, ref refVelocity, smoothingTime), Vector3.up);
        }
	}
}
