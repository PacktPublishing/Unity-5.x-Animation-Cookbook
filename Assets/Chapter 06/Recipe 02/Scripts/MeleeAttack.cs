using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeleeAttack : MonoBehaviour {

    public int damage = 10;
    public List<Enemy> enemiesInRange = new List<Enemy>();


	//This is the event called from our attack animation. This function has to be public.
	public void Attack () {
        
        //We find every enemy in our range and call the Hit() function on them
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            enemiesInRange[i].Hit(damage);
        }
	
	}
}
