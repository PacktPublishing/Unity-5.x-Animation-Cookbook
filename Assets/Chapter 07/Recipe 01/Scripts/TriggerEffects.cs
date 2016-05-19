using UnityEngine;
using System.Collections;

public class TriggerEffects : MonoBehaviour {

    AudioSource source;

    void Start()
    {
        //We need an AudioSource component to play sounds
        source = GetComponent<AudioSource>();
    }

	// This function is called from an animation as an Animation Event
	public void Effect (string effectResourceName) {

        //We load a resource (stored in the Resources folder) with the name specified in the Animation Event
        GameObject effectResource = (GameObject)Resources.Load(effectResourceName);
        if (effectResource != null)
        {
            //If we find the resource, we instantiate it.
            GameObject.Instantiate(effectResource, transform.position, Quaternion.identity);
        }

	}
	
	//This function is also called from the animation as an Animation Event
	public void Sound (string soundResourceName) {

        //We try to load an AudioClip from the Resources folder
        AudioClip clip = (AudioClip)Resources.Load(soundResourceName);
        if (clip != null)
        {
            //We change the pitch to randomize the sound a little
            source.pitch = Random.Range(0.9f, 1.2f);
            //Here we play the loaded sound once
            source.PlayOneShot(clip);
        }
	}
}
