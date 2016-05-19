using UnityEngine;
using System.Collections;

public class TurretAim : MonoBehaviour {

    public Transform turretGun;
    public Transform turretPivot;
    public Transform aimTarget;
    public float dampTime = 1f;

    public float maxVerticalAngle = 70f;
    public float minVerticalAngle = 10f;

    Vector3 aimVector;
    Vector3 horizontalAimVector;
    Vector3 dampedTargetPosition;
    Vector3 refDampSpeed;

	void Update () {

        //Here we calculate the dampedTargerPosition to make the turret rotation smooth
        dampedTargetPosition = Vector3.SmoothDamp(dampedTargetPosition, aimTarget.position, ref refDampSpeed, dampTime);

        //We caluclate the aimVector for the turretGun.
        aimVector = dampedTargetPosition - turretGun.position;
        //We calculate the horizontalAimVector for the turretPivot.
        horizontalAimVector = aimVector;
        horizontalAimVector.y = 0f;

        //We rotate the turretPivot accordingly to the horizontalAimVector and turretGun accordingly to the aimVector
        turretPivot.rotation = Quaternion.LookRotation(horizontalAimVector);
        turretGun.rotation = Quaternion.LookRotation(aimVector);

    }
}
