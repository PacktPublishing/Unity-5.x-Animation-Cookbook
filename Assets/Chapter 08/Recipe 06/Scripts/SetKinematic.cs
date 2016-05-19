using UnityEngine;
using System.Collections;

public class SetKinematic : MonoBehaviour {

    //This functions is called as an Animation Event
    //it sets the isKinematic parameter of the Rigid Body
    //attached to the same game object

    public void NotKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
