using UnityEngine;
using System.Collections;

public class WaitAndSwitch : StateMachineBehaviour {
    public string triggerName = "Switch";
    public float waitTime = 5f;
    bool switched = false;
    float startTime;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        startTime = Time.time;
        switched = false;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Here we check if current time is greater than startTime + waitTime.
        //This ensures we were waiting for the waitTime period.
        if (Time.time >= startTime + waitTime && !switched)
        {
            //We need to set the switched flag to prevent from setting the same trigger more than once
            switched = true;
            
            //We set the triggerName trigger on our animator component
            animator.SetTrigger(triggerName);
        }
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
