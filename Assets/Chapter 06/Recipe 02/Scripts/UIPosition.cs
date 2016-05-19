using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPosition : MonoBehaviour {

    public Transform targetPosition;
    public Vector3 offset;
    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }
	
	void Update () {

        //We keep this UI element position on screen the same as out targetPosition transform's

        rectTransform.position = Camera.main.WorldToScreenPoint(targetPosition.position) + offset;
	}
}
