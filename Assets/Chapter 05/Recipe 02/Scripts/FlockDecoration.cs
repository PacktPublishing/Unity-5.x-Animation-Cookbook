using UnityEngine;
using System.Collections;

public class FlockDecoration : MonoBehaviour {
    GameObject[] birds;

    void Start () {

        //We find all the birds in the scene (they have the "Bird" tag assigned)
        birds = GameObject.FindGameObjectsWithTag("Bird");

        for (int i = 0; i < birds.Length; i++)
        {
            Animator anim = birds[i].GetComponent<Animator>();
            
            //Here we try to desynchronize the animations of the whole flock, by setting the 
            //animation playback speed to a random value.
            anim.speed = Random.Range(0.8f, 1.3f);
        }

	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < birds.Length; i++)
            {
                Animator anim = birds[i].GetComponent<Animator>();

               //After we press the space button we invert the bool parameter "Fly" in each bird's Animator Controller
               //If the "Fly" parameter is true, birds try to fly, if it's false they try to land.
                anim.SetBool("Fly", !anim.GetBool("Fly"));
            }
        }
	
	}
}
