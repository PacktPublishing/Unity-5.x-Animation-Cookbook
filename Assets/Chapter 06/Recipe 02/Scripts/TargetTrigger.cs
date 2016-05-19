using UnityEngine;
using System.Collections;

public class TargetTrigger : MonoBehaviour {

    MeleeAttack attackScript;

	// Use this for initialization
	void Start () {

        //Here we assign the MeleeAttack script from the root of our character's hierarchy.
        attackScript = transform.root.GetComponent<MeleeAttack>();

    }
	
	//If we collide with an enemy, we add it to enemiesInRange list in the MeleeAttack script
	void OnTriggerEnter (Collider other) {

        Enemy e = other.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            if (!attackScript.enemiesInRange.Contains(e))
            {
                attackScript.enemiesInRange.Add(e);
            }
        }
	}
    //If an enemy exits our target trigger, we remove it from the enemiesInRangeList in the MeleeAttack script
    void OnTriggerExit(Collider other)
    {

        Enemy e = other.gameObject.GetComponent<Enemy>();
        if (e != null)
        {
            if (attackScript.enemiesInRange.Contains(e))
            {
                attackScript.enemiesInRange.Remove(e);
            }
        }
    }
}
