using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RagdollWeight : MonoBehaviour {
    public Transform animatedRig;
    public Transform skinnedRig;
    public Transform ragdoll;
    public Transform faceDirectionHelper;
    public float groundCheckDistance = 1f;
    public LayerMask groundCheckMask;
    public Transform hips;
    public Transform head;
    public float blendSpeed = 2f;
    [Range(0f, 1f)]
    public float blendFactor = 0f;

    Rigidbody rb;
    Collider col;
    Animator anim;

    Transform[] skinnedRigTransforms;
    Transform[] ragdollTransforms;
    Transform[] animatedRigTransforms;

    Vector3 lookVector;
    Vector3 desiredLookVector;
    Vector3 finalPosition;

    RaycastHit groundHit;

    bool ragdollOn = false;
    void Start()
    {
        Init();
    }

    //In the Init() function we ger references to the bones of our three rigs,
    //to be able to blend between them later on. We also get the reference to
    //the Rigidbody, Animator and Collider components attached to our character.
    //Finally we turn the ragdoll game object off.
    void Init()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        skinnedRigTransforms = skinnedRig.GetComponentsInChildren<Transform>();
        ragdollTransforms = ragdoll.GetComponentsInChildren<Transform>();
        animatedRigTransforms = animatedRig.GetComponentsInChildren<Transform>();

        ragdoll.gameObject.SetActive(false);
    }

    //In the EnableRagdoll() function we first check if our blendFactor is not 
    //greater than 0.5 (that would mean we are still in the "ragdoll phase"). 
    //If it is less than 0.5f, we set all ragdoll bones' positions and rotations
    //to match the animatedRig's transforms (we match the pose of the ragdoll to the
    //current character pose). Finally we set the main Rigidbody component to kinematic
    //and enable the ragdoll game object. We also set the blendFactor to 1, which means
    //the ragdoll is fully enabled. 
    void EnableRagdoll()
    {
        if(blendFactor > 0.5f)
        {
            return;
        }
        for (int i = 0; i < ragdollTransforms.Length; i++)
        {
            ragdollTransforms[i].localPosition = animatedRigTransforms[i].localPosition;
            ragdollTransforms[i].localRotation = animatedRigTransforms[i].localRotation;
        }

        rb.isKinematic = true;
        ragdoll.gameObject.SetActive(true);
        ragdollOn = true;
        blendFactor = 1f;
    }

    //In the DisableRagdoll() function we check if our character lays 
    //face down or face up, and play an appropriate stand up animation.
    //After we trigger the animation we start the BlendFromRagdoll() co-routine
    //to blend smoothly from the ragdoll rig to the animatedRig.
    void DisableRagdoll()
    {
        bool faceUp = Vector3.Dot(faceDirectionHelper.forward, Vector3.up) > 0f;
        if (faceUp)
        {
            anim.SetTrigger("StandUpFaceUp");
        }
        else
        {
            anim.SetTrigger("StandUpFaceDown");
        }
        StartCoroutine("BlendFromRagdoll");
    }
    //BlendFromRagdoll() co-routine decreases the blendFactor in time
    //and checks if it's still greater than 0. If it is less equal
    //to 0, we set it to be exactly 0, enable the main Rigidbody component
    //again and disable the radgoll.
    IEnumerator BlendFromRagdoll()
    {
        while (blendFactor > 0f)
        {
            blendFactor -= Time.deltaTime * blendSpeed;
            yield return null;
        }
        blendFactor = 0f;
        rb.isKinematic = false;
        blendFactor = 0f;
        ragdollOn = false;
        ragdoll.gameObject.SetActive(false);
    }

    //In the FixedUpdate() function, if the radoll is on we move our character's Rigidbody to the position
    //of the ragdoll's hips. We additionally check the ground position, to make sure, our main Rigidbody
    //stands on ground and doesn't levitate. We also rotate the Rigidbody so, the character looks in the
    //hips -> head direction. We intentionally omit the Y component of the desiredLookVector, to prevent our
    //character's capsule from tilting. Moving our character's Rigidbody to the position of the ragdolls hips,
    //makes the blending from ragdoll to animation easier. 
    void FixedUpdate()
    {
        if (ragdollOn)
        {
            desiredLookVector = head.position - hips.position;
            desiredLookVector.y = 0f;
            desiredLookVector = desiredLookVector.normalized;
            lookVector = Vector3.Slerp(transform.forward, desiredLookVector, Time.deltaTime);


            if (Physics.Raycast(hips.position, Vector3.down, out groundHit, groundCheckDistance, groundCheckMask))
            {
                finalPosition = groundHit.point;
            }
            else
            {
                finalPosition = hips.position;
            }

            rb.MovePosition(finalPosition); 
            
            rb.MoveRotation(Quaternion.LookRotation(lookVector));
            
        }
    }
    //In the Update() function we check if player pressed Space.
    //If so, we enable or disable the ragdoll depending on whether
    //it is enabled or disabled at the moment.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ragdollOn)
            {
                DisableRagdoll();
            }
            else
            {
                EnableRagdoll();
            }
        }
    }
    
    //In the LateUpdat() function we constantly interpolate the localPosition and localRotation of all
    //the bones of the skinnedRig (the one our character's mesh is using). The position and rotation
    //of the bones is interpolated between the position and rotation of the ragdoll and the animatedRig.
    //We use the blendFactor variable for the interpolation. 
    void LateUpdate()
    {
            for (int i = 0; i < skinnedRigTransforms.Length; i++)
            {
                skinnedRigTransforms[i].localPosition = Vector3.Lerp(animatedRigTransforms[i].localPosition, ragdollTransforms[i].localPosition, blendFactor);
                skinnedRigTransforms[i].localRotation = Quaternion.Lerp(animatedRigTransforms[i].localRotation, ragdollTransforms[i].localRotation, blendFactor);
            }
        
    }

}
