using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBars : MonoBehaviour {

    //The fillSpeed variable determines the fill amount increment of the progress bar per second
    public float fillSpeed = 0.5f;
    Image image;

	void Start () {

        //We assign a reference to the Image component of the same game object
        image = GetComponent<Image>();
	
	}

	void Update () {

        //We increase the fillAmount of the Image component in time
        image.fillAmount += Time.deltaTime * fillSpeed;

	}
}
