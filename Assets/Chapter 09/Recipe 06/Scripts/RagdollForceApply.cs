using UnityEngine;
using System.Collections;

public class RagdollForceApply : MonoBehaviour {
    public LayerMask ragdollBodyPartsLayer;
    public float forceMagnitude = 100f;
    HandleRagdoll ragdollHandler;
    Rigidbody hitBodyPart;
    RaycastHit hitInfo;
    Vector3 force;

	void Start () {
        //We store the reference to the HandleRagdoll script
        ragdollHandler = GetComponent<HandleRagdoll>();

    }

    void AddForce()
    {
        //This function turns the ragdoll on (using the HandleRagdoll script)
        ragdollHandler.EnableRagdoll(true);
        //And it applies the force in the camera direction, with the forceMagniture strenght
        force = (hitInfo.point - Camera.main.transform.position).normalized * forceMagnitude;
        hitBodyPart.AddForce(force, ForceMode.Impulse);
       
    }

	// Update is called once per frame
	void Update () {
        
        //If player presses the left mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //We create a ray from the mouse cursor position in the camera's direction and
            //we check if we hit a collider that has one of the layers described by the ragdollBodyPartsLayer
            //Layer Mask. 
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, 100f, ragdollBodyPartsLayer))
            {
                //If we managed to hit a collider, we try to get the Rigidbody component from it
                //and store it in our hitBodyPart variable
                hitBodyPart = hitInfo.collider.gameObject.GetComponent<Rigidbody>();
                if (hitBodyPart != null)
                {
                    
                    //If we manage to find the Rigidbody component,
                    //we call the AddForce() function that turns the ragdoll on
                    //and applies the force to the hitBodyPart
                    AddForce();
                }
            }
        }
	
	}
}
