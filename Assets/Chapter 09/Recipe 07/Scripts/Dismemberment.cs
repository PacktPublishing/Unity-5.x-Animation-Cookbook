using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dismemberment : MonoBehaviour {
    public Transform head;
    public Transform headMarker;
    public Transform neckMarker;
    public GameObject headPrefab;
    public GameObject neckPrefab;
    public Vector3 randomForce1;
    public Vector3 randomForce2;
    public Vector3 randomTorque1;
    public Vector3 randomTorque2;

    bool wasSevered = false;

    HandleRagdoll ragdollHandler;

    void Start()
    {
        ragdollHandler = GetComponent<HandleRagdoll>();
    }

    Vector3 RandomVector(Vector3 v1, Vector3 v2)
    {
        float x = Random.Range(v1.x, v2.x);
        float y = Random.Range(v1.y, v2.y);
        float z = Random.Range(v1.z, v2.z);
        return new Vector3(x, y, z);
    }

    void Decapitate()
    {
        //We only can decapitate the character once, so we set the wasSevered flag to true
        wasSevered = true;
        if (ragdollHandler != null)
        {
            //If we have a HandleRagdoll script attached, we should enable the ragdoll
            //and disable the script
            ragdollHandler.EnableRagdoll(true);
            ragdollHandler.enabled = false;
        }
        Joint headJoint = head.GetComponent<Joint>();
        if (headJoint != null)
        {
            //Because we will scale the head transform to 0, we should destroy the head joint
            Destroy(headJoint);
        }
        Collider headCollider = head.GetComponent<Collider>();
        if (headCollider != null)
        {
            //And the collider on the head
            Destroy(headCollider);
        }
        Rigidbody headRigidBody = head.GetComponent<Rigidbody>();
        if (headRigidBody != null)
        {
            //And the rigid body attached to it
            Destroy(headRigidBody);
        }
        //We spawn a headPrefab in the place of the headMarker (this is a Transform that stores original head position)
        GameObject spawnedHead = (GameObject)GameObject.Instantiate(headPrefab, headMarker.position, headMarker.rotation);
        Rigidbody spawnedHeadRB = spawnedHead.GetComponent<Rigidbody>();
        if (spawnedHeadRB != null)
        {
            //After we spawn the head, we add a force and torque to it to launch it in the air and rotate
            spawnedHeadRB.AddForce(RandomVector(randomForce1, randomForce2), ForceMode.Impulse);
            spawnedHeadRB.AddTorque(RandomVector(randomTorque1, randomTorque2),ForceMode.Impulse);
        }
        //Then we scale the head to 0, to hide it
        head.localScale = Vector3.zero;
        //We also spawn a neckPrefab to mask the scaled down head.
        GameObject spawnedNeck = (GameObject)GameObject.Instantiate(neckPrefab, neckMarker.position, neckMarker.rotation);
        spawnedNeck.transform.parent = neckMarker;
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && !wasSevered)
        {
            Decapitate();
        }

	}
}
