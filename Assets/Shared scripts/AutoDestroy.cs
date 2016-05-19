using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

    public float destroyTime = 5f;

	
	void Start () {
        //This function destroys the object after the destroyTime time.
        Destroy(gameObject, destroyTime);
	}
}
