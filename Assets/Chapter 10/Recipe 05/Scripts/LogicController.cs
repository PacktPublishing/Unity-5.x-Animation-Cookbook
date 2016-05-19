using UnityEngine;
using System.Collections;

public class LogicController : MonoBehaviour {

    public string switchTriggerName = "Switch";
    public Transform activeObject;
    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {

        //If player presses Space, we set the switch trigger
        //in the animator - that controls the graph flow
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger(switchTriggerName);
        }

	}
}
