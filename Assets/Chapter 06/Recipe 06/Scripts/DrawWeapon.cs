using UnityEngine;
using System.Collections;

public class DrawWeapon : MonoBehaviour {

    public Transform weapon;
    public Transform handMarker;
    public Transform sheathMarker;

    public float lerpSpeed = 5f;
    public bool weaponInHand = false;

    Animator anim;

	void Start () {

        anim = GetComponent<Animator>();

	}

    //This function is called from an Animation Event in the Draw animation
    public void Draw()
    {
        //If the weapon is already in hand, we change it's parent to the sheathMarker
        if (weaponInHand)
        {
            weaponInHand = false;
            weapon.parent = sheathMarker;
        }
        //If it's not, we change it's parent to the handMarker
        else
        {
            weaponInHand = true;
            weapon.parent = handMarker;
        }
    }

	void Update () {

        //We draw / sheath weapon when player presses Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Draw");
        }
        //Here we Lerp weapon's position and rotation with it's parent slot to be sure our weapon will always match the slot's position
        if (weapon.parent != null)
        {
           if ((weapon.position - weapon.parent.position).sqrMagnitude > 0.0001f)
           {
                weapon.position = Vector3.Lerp(weapon.position, weapon.parent.position, Time.deltaTime * lerpSpeed);
                weapon.rotation = Quaternion.Lerp(weapon.rotation, weapon.parent.rotation, Time.deltaTime * lerpSpeed);
           }
        }

	}
}
