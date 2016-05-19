using UnityEngine;
using System.Collections;

public class CameraLook : MonoBehaviour {

    //This script is attached to a cameraRig game object containing cameraPivot game object as a child. 
    //cameraPivot has the camera as a child. The scripts rotates the cameraRig horizontaly and cameraPivot verticaly
    //based on mouse input. 
    public Transform cameraPivot;
    public Transform target;
    public float rotationSpeed = 50f;
    public float verticalMin = -60f;
    public float verticalMax = 70f;

    Quaternion localCameraRotation;
    float localXRotation;

    float hor;
    float ver;
	
	void LateUpdate () {


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hor = Input.GetAxis("Mouse X");
        ver = Input.GetAxis("Mouse Y");

        ver = Mathf.Clamp(ver, -1f, 1f);

        localCameraRotation = cameraPivot.localRotation;

        localCameraRotation *= Quaternion.Euler(ver*rotationSpeed, 0f, 0f);

        localXRotation = localCameraRotation.eulerAngles.x;

        transform.Rotate(Vector3.up * hor * rotationSpeed, Space.World);
        if(target != null)
            transform.position = target.position;

        if (localXRotation > 180f)
        {
            localXRotation -= 360f;
        }

        if (localXRotation > verticalMin && localXRotation < verticalMax)
        {
            cameraPivot.localRotation = localCameraRotation;
        }
    

    }
}
