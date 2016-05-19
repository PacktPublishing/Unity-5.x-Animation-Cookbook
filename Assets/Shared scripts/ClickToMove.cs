using UnityEngine;
using System.Collections;

public class ClickToMove : MonoBehaviour {

    //The Navmesh Agent we are trying to move
    //Navmesh Agents are used with Navmeshes for pathfinding
    public NavMeshAgent agent;

    //A RaycastHit variable to store the result of a raycast
    RaycastHit hit;

	// Update is called once per frame
	void Update () {
        
        //We check if player pressed the left mouse button
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //If so, we cast a ray from the mouse position on screen with the main camera direction, and store the result in the hit variable. 
            //Physics.Raycast() returns true if the ray hits any collider
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                //We are giving the Navmesh Agent a new destination
                agent.SetDestination(hit.point);
            }
        }
	
	}
}
