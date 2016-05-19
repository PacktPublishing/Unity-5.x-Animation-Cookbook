using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyWithHits : Enemy {

    //We've added this flag to prevent the character from playing hit reactions when already dead
    bool isAlive = true;


    public override void Hit(float damage)
    {
        if (!isAlive)
        {
            return;
        }

        //We play the hit reaction here
        anim.SetTrigger("Hit");

        //We continue with the base hit implementation here
        base.Hit(damage);

    }
    public override void Die()
    {
        isAlive = false;
        //We continue with the base hit implementation here
        base.Die();

    }
}
