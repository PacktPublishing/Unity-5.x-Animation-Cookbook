using UnityEngine;
using System.Collections;

public class ClimbEndTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        //This trigger ends the climb action
        if (other.gameObject.CompareTag("Player"))
        {
            Climb climbScript = other.gameObject.GetComponent<Climb>();
            climbScript.EndClimbing();
        }

    }
}
