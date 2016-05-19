using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HandleRagdoll : MonoBehaviour {
    public Transform charactersRig;
    Animator anim;
    Rigidbody mainRigidbody;
    [HideInInspector]
    public List<Rigidbody> ragdollRigidbodies = new List<Rigidbody>();
    Collider mainCollider;
    bool ragdolEnabled = false;

    void Init()
    {
        //We save the reference to the Animator component;
        anim = GetComponent<Animator>();
        //We save the main collider attached to the character
        mainCollider = GetComponent<Collider>();
        //We save all the ragdoll colliders

        //We save the main rigidbody attached to the character
        mainRigidbody = GetComponent<Rigidbody>();
        //We save all the rigidbodies attached to the ragdoll
        charactersRig.GetComponentsInChildren<Rigidbody>(true, ragdollRigidbodies);
        if (mainRigidbody != null && ragdollRigidbodies.Contains(mainRigidbody))
        {
            //We make sure that mainRigidbody is not part of the
            //ragdoll rigidbodies
            ragdollRigidbodies.Remove(mainRigidbody);
        }
    }

	// Use this for initialization
	void Awake () {
        Init();
    }

    void Start()
    {
        //We set the ragdoll to disabled, when the game starts
        EnableRagdoll(false, ragdollRigidbodies);
    }
    public void EnableRagdoll(bool enable)
    {
        EnableRagdoll(enable, ragdollRigidbodies);
    }
    public void EnableRagdoll(bool enable, List<Rigidbody> rigidbodies)
    {
        //If the ragdoll is enabled,we  set all the ragdolRigidbodies 
        //to non kinematic. If we disable the ragdoll, we
        //do the opposite.

        for (int i = 0; i < rigidbodies.Count; i++)
        {
            rigidbodies[i].isKinematic = !enable;
        }
        //If the ragdoll is enabled, we disable the mainCollider
        //and the Animator component. We also set the mainRidigbody 
        //to kinematic. If the ragdoll is disabled we do the opposite. 
        if (mainCollider != null)
        {
            mainCollider.enabled = !enable;
        }
        if (mainRigidbody != null)
        {
            mainRigidbody.isKinematic = enable;
        }
        if (anim != null)
        {
            anim.enabled = !enable;
        }

    }

	// Update is called once per frame
	void Update () {

        //When player presses space, we enable the ragdoll if it was not
        //enabled and disable it if it was enabled.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ragdolEnabled = !ragdolEnabled;
            EnableRagdoll(ragdolEnabled);
        }

	}
}
