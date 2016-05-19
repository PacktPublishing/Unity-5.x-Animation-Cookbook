using UnityEngine;
using System.Collections;

public class CameraColor : MonoBehaviour {

    public Color cameraColor = Color.blue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //Here we set the background color of the camera to our public variable's value
        Camera.main.backgroundColor = cameraColor;

	}
}
