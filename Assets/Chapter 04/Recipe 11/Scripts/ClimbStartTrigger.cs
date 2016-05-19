using UnityEngine;
using System.Collections;

public class ClimbStartTrigger : MonoBehaviour {


    public Transform startNode;
    //This trigger starts the climb action
    void OnTriggerEnter (Collider other) {

        if (other.gameObject.CompareTag("Player"))
        {
            Climb climbScript = other.gameObject.GetComponent<Climb>();
            climbScript.StartClimbing(startNode);
        }
	
	}
}
