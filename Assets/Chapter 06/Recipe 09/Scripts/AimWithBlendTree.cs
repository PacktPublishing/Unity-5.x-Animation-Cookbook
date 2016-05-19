using UnityEngine;
using System.Collections;

public class AimWithBlendTree : MonoBehaviour {

    public Transform target;
    public Transform aimNode;
    public float aimSmooth = 0.5f;
    Animator anim;

    float aimHor = 0f;
    float aimVer = 0f;

    Vector3 aimVector;
    Vector3 horAimVector;

    void Start () {

        anim = GetComponent<Animator>();

	}
	
	void Update () {

        //Here we calculate the aimVector - the vector from our aimNode to the target
        aimVector = target.position - aimNode.position;

        //Here we calculate a horizontal version of the aimVector by getting rid of the Y component
        horAimVector = aimVector;
        horAimVector.y = 0f;

        //Here we calculate the angle between the horAimVector and the transforms's forward axis.
        //We also use the Mathf.Sign() function to multiply the angle by 1 or -1 depenging on the direction
        //of the horAimVector. The Dot() function will return a value greater than 0 if the horAimVector and
        //aimNode's right axis have similar direction. 
        aimHor = Vector3.Angle(horAimVector, transform.forward) * Mathf.Sign(Vector3.Dot(horAimVector, transform.right));
        //We calculate the aimVer angle in a very similar way. Instead of using aimNode's forward axis we calculate the angle
        //between the aimVector and the horAimVector - to get the vertical aim angle. 
        aimVer = Vector3.Angle(horAimVector, aimVector) * Mathf.Sign(Vector3.Dot(aimVector, Vector3.up));

        //We set the AimHor and AimVer parameters in the controller
        anim.SetFloat("AimHor", aimHor, aimSmooth, Time.deltaTime);
        anim.SetFloat("AimVer", aimVer, aimSmooth, Time.deltaTime);

    }
}
