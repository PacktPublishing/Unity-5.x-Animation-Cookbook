using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float hitPoints = 50;

    //This is a reference to a health bar UI element
    public Image healthBar;
    [HideInInspector]
    public float initialHitPoints = 0;
    [HideInInspector]
    public Animator anim;
    void Start()
    {
        //We save the initial hitPoints value to be able to animate the health bar
        initialHitPoints = hitPoints;

        anim = GetComponent<Animator>();
    }

    public virtual void Hit(float damage)
    {
        hitPoints -= damage;

     

        if (hitPoints <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        anim.SetBool("Dead", true);
    }

    void Update()
    {
        //Here we can update our health bar accordingly to our current HP percentage
        healthBar.fillAmount = hitPoints / initialHitPoints;
    }

}
