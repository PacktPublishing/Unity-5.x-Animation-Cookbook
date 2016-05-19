using UnityEngine;
using System.Collections;

public class Healing : MonoBehaviour {

    public GameObject healingEffectPrefab;
    public string healingAllowedState = "Base Layer.SpiderIdle";
    public float healingSpeed = 5f;
    bool isHealing = false;
    Animator anim;

	void Start () {

        anim = GetComponent<Animator>();

	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.H))
        {
            //We check if our character is in the Idle state, only then we allow it to trigger
            //the Heal coroutine
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(healingAllowedState))
            {
                //This flag prevents us from starting the healing action
                //when it is already active
                if (!isHealing)
                {
                    StartCoroutine("Heal");
                }
            }
        }

	
	}

    IEnumerator Heal()
    {
        //We instantiate a healing effect here
        GameObject.Instantiate(healingEffectPrefab, transform.position, transform.rotation);

        isHealing = true;
       
        Enemy enemyScript = GetComponent<Enemy>();

      
        //We check if our current hitPoints value is less than the initialHitPoints value
        while (enemyScript.hitPoints < (float)enemyScript.initialHitPoints)
        {
            //If so, we add hitPoints in time
            enemyScript.hitPoints += healingSpeed * Time.deltaTime;
            yield return null;
        }
        //We make sure our hitPoints are fully restored
        enemyScript.hitPoints = enemyScript.initialHitPoints;
        isHealing = false;
    }
}


