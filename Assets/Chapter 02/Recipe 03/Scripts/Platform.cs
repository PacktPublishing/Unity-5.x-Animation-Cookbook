using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    /*This function is called by Unity every time this object starts to 
    collide with any other game object with a Collider component attached.
    The Collision collisionInfo object parameter stores the information
    about the collision and the object we are colliding with.*/
    void OnCollisionEnter(Collision collisionInfo)
    {
        /*We are checking if the object we are colliding with has a RigidBody component
        and the RigidBody is not set to kinematic. Optionally we can also check
        the tag of the object we are colliding with here (to make it work only for the player for instance).*/
        if (collisionInfo.rigidbody != null && !collisionInfo.rigidbody.isKinematic)
        {
            /*We are setting the parent of the object we are colliding with
            to the platform game object (the object out script is attached to).
            This will make our character move with the platform instead of slide from it.*/
            collisionInfo.transform.parent = transform;
        }
    }
    /*This function is called by Unity every time this object stop colliding with any 
    object with a Collider component attached. The CollisionInfo collision info parameter
    stores the same information as in the OnCollisionEnter function.*/
    void OnCollisionExit(Collision collisionInfo)
    {
        /*We are checking the same conditions as before*/
        if (collisionInfo.rigidbody != null && !collisionInfo.rigidbody.isKinematic)
        {
            /*We are setting the parent of the object we are colliding with to null.
            The object has no parent at all and stops moving with the platform*/
            collisionInfo.transform.parent = null;
        }
    }
}
