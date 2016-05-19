using UnityEngine;
using System.Collections;

public class ApplyForce : MonoBehaviour {

    public Transform towards;    //the object towards which we want to move the rigid body
    public float forceMagnitude = 10f;      //the magnitude of the applied force

	void Start () {
        //We find the Rigidbody component
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            //If it is present, we calculate the force vector
            Vector3 forceVector = towards.position - transform.position;
            forceVector = forceVector.normalized * forceMagnitude;
            //And apply the force in the Impulse mode
            rb.AddForce(forceVector, ForceMode.Impulse);
        }

	}
	
}
