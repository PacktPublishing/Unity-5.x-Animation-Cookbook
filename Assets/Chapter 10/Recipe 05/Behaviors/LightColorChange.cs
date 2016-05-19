using UnityEngine;
using System.Collections;

public class LightColorChange : StateMachineBehaviour {
    public string lightGameObjectName;
    public Color color = Color.white;
    public bool randomColor = false;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //We cannot reference a scene object in the controller (controller is an asset).
        //So we need to find it by tag or name.
        GameObject go = GameObject.Find(lightGameObjectName);
        if (go != null)
        {
            //If we manage to find our game object, we get its Light component
            Light light = go.GetComponent<Light>();
            if (light != null)
            {
                if (randomColor)
                {
                    //If we set the randomColor flag to true, 
                    //we will choose a random color for the light
                    light.color = Random.ColorHSV();
                }
                else
                {
                    //If the randomColor flag is false,
                    //we set the light color to the color variable's value
                    light.color = color;
                }
               
            }
        }
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
