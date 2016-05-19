using UnityEngine;
using System.Collections;

public class LerpContinuous : MonoBehaviour {

    public float speed = 0.5f;
    RectTransform rectTransform;

	void Start () {
        rectTransform = GetComponent<RectTransform>();
    }
	
	void Update () {

        //Here, Lerp function interpolates the position of the rectTransform between itself
        //and mouse position every frame. This way we have a continous interpolation. 
        //The object will move faster if it is further away from the mouse pointer and slower
        //when it comes closer. 
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, Time.deltaTime * speed);

	}
}
