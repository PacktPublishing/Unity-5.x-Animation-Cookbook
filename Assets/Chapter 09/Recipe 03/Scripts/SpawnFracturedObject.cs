using UnityEngine;
using System.Collections;

public class SpawnFracturedObject : MonoBehaviour {

    public GameObject fracturedObjectPrefab;
    Rigidbody rb;
    Vector3 lastRigidBodyVelocity;

    void OnCollisionEnter(Collision col)
    {
        //We fracture our game object on collision
        Fracture();
    }

    void Fracture()
    {
        //We spawn a new pre-fractured game object and apply the lastRigidBodyVelocity
        //to each of its rigid bodies. Then we destroy this game object.
        GetComponent<Collider>().enabled = false;
        GameObject fracturedObject = (GameObject)GameObject.Instantiate(fracturedObjectPrefab, transform.position, transform.rotation);
        Rigidbody[] rigidBodies = fracturedObject.GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < rigidBodies.Length; i++)
        {
            rigidBodies[i].velocity += lastRigidBodyVelocity;
        }
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //We save the last velocity of the main rigid body
        lastRigidBodyVelocity = rb.velocity;

	}
}
