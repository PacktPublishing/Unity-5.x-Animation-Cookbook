using UnityEngine;
using System.Collections;

public class ParticleAnims : MonoBehaviour {

    public float gravityModifier = 0f;
    public Color color = Color.white;
    ParticleSystem particles;

	// Use this for initialization
	void Start () {

        particles = GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {

        //Here we set the properties of the Particle System
        //Those properties are public variables of this script,
        //thus they can be animated with the Animation View
        particles.gravityModifier = gravityModifier;
        particles.startColor = color;

    }
}
