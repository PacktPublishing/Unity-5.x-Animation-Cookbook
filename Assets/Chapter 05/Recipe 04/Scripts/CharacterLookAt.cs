using UnityEngine;
using System.Collections;
[System.Serializable]

public class CharacterLookAt : MonoBehaviour {

    //We store the bone we want to affect in this variable
    public Transform bone;
    //This is the target our character will look at
    public Transform target;
    //Most often bone's forward axis is not alligned with the face, so we need to apply additional rotation to match it
    public Vector3 additionalRotation;
    //This is the maximum angle the head can rotate in every axis
    public float maxAngle = 60f;
    //This is the time in which we will damp the look at vector, to make it look more smooth
    public float dampTime = 0.2f;
    //This is the weight of the look at. If it's set to 0, the look at will be turned off
    public float weight = 1f;

    //This is the final lookat direction
    Vector3 finalLookVector;
    //This is the raw look at direction (not clamped in any way)
    Vector3 lookDirection;
    //This is the final calculated rotation
    Quaternion rotation;
    //This is a reference vector used by the Vector3.SmoothDamp() function
    Vector3 dampVelocity;

	void LateUpdate () {

        //If weight is less or equal 0, we don't want to do any calculations
        if (weight <= 0f)
        {
            return;
        }

        //Here we calculate the raw lookDirection vector. We also use the SmoothDamp function to make the changes
        //of this vector less sudden (more smooth).
        lookDirection = Vector3.SmoothDamp(lookDirection, target.position - bone.position, ref dampVelocity, dampTime);

        //We check if the angle between our transform's forward vector and  the raw lookDirection vector is greater than maxAngle
        if (Vector3.Angle(lookDirection, transform.forward) > maxAngle)
        {
            //If so, our finalLookVector is calculated by rotating the transform's forward to the lookDirection vector by 
            //the maxAngle degrees. This way we create a "cone of vision". 
            finalLookVector = Vector3.RotateTowards(transform.forward, lookDirection, Mathf.Deg2Rad*maxAngle, 0.5f);
        }
        else
        {
            //If we are in the "cone of vision", we don't modify the lookDirection.
            finalLookVector = lookDirection;
        }

        //Here we draw two debug lines, to visualize the modified and original lookDirection
        //You can check them in the scene view. 
        Debug.DrawLine(bone.position, bone.position + finalLookVector, Color.green);
        Debug.DrawLine(bone.position, bone.position + lookDirection, Color.red);

        //Finally we calculate the bone rotation using the finalLookVector applying additional rotation.
        rotation = Quaternion.LookRotation(finalLookVector) * Quaternion.Euler(additionalRotation);
        //We also interpolate the final bone rotation between the original one and the one we've calculated. 
        //So setting the weight to 0, will turn of the look at system. If you want a smooth transition
        //to and from look at state, lerp the weight variable in time. 
        bone.rotation = Quaternion.Lerp(bone.rotation, rotation, weight);
    }
}
