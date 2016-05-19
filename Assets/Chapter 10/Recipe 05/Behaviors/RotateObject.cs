using UnityEngine;
using System.Collections;

public class RotateObject : StateMachineBehaviour {

    public float rotationSpeed = 45f;
    public Vector3 rotationAxis = Vector3.up;
    public Space rotationSpace = Space.World;
    LogicController controller;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        controller = animator.gameObject.GetComponent<LogicController>();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //We check if we have the LogicController script attached
        if (controller == null)
        {
            return;
        }
        //We check if the activeObject field in the LogicController script references
        //a game object in the scene
        if (controller.activeObject == null)
        {
            return;
        }
        //We rotate the referenced object around the rotationAxis with the rotationSpeed
        //and in the rotationSpace (world or local)
        controller.activeObject.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, rotationSpace);
	}

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
