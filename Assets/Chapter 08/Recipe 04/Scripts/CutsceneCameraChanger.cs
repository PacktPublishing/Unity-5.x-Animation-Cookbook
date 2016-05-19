using UnityEngine;
using System.Collections;

public class CutsceneCameraChanger : MonoBehaviour {

    public Camera[] cutsceneCameras;
	
	// This function is called as an animation event from the cutscene animation
	public void ChangeCamera (int newCameraIndex) {

        //First we deactivate all the cameras
        for (int i = 0; i < cutsceneCameras.Length; i++)
        {
            cutsceneCameras[i].gameObject.SetActive(false);
        }

        //And we activate the camera specified with the newCameraIndex parameter
        cutsceneCameras[newCameraIndex].gameObject.SetActive(true);


    }
}
