using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        /*When the player presses the E key, we are setting the Move trigger on the Animator component. 
        We are assuming the Animator component is present on the game object our script is attached to*/
        if (Input.GetKeyDown(KeyCode.E))
        {
            GetComponent<Animator>().SetTrigger("Move");
        }

	}
}
