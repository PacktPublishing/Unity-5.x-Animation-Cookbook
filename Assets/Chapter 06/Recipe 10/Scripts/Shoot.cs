using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Shoot : MonoBehaviour {

    public LayerMask bodyPartsLayer;
    public float maxDistance = 100f;
    public Text bodyPartText;
    RaycastHit hit;

	void Update () {


        Transform cameraTransform = Camera.main.transform;
            //We shoot a ray and try to hit a body part
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, maxDistance, bodyPartsLayer))
            {
                //If we hit a collider that has the bodyPartsLayer assigned, we check if it has a BodyPart component
                BodyPart p = hit.collider.gameObject.GetComponent<BodyPart>();
                if (p != null)
                {
                    //If so, we display the name of the body part.
                    //Here we could decide what hit animation we should play
                    bodyPartText.text = "Hit body part: " + p.bodyPartName;
                }
                else
                {
                //If not, we display a miss animation
                bodyPartText.text = "No body part was hit";
                }
            }
            else
            {
            //If we don't hit any collider, we display a miss animation
                 bodyPartText.text = "No body part was hit";
            }
        }
}
