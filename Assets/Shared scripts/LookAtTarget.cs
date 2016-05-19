using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour {


    public Transform target;

	void Update () {

        //Here we make the transform look at the target transform each frame
        transform.LookAt(target);
	
	}
}
