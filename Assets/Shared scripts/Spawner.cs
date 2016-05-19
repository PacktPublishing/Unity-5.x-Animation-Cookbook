using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public bool spawnAtStart = true;
    public float spawnAfterTime = 2f;
    public GameObject[] prefabs;

	// Use this for initialization
	void Start () {

        //If we want to spawn an object when the game starts, we start the SpawnAfterTimeCoroutine
        if (spawnAtStart)
        {
            StartCoroutine("SpawnAfterTime");
        }

	}

    IEnumerator SpawnAfterTime()
    {
        //Here we wait for the spawnAfterTime amount of seconds and spawn the prefab in our spawner position and with our spawner rotation
        yield return new WaitForSeconds(spawnAfterTime);
        Spawn(transform.position, transform.rotation);
    }
	
    //This function instantiates all prefabs defined in the prefabs[] array.
    //All game objects are spawned in the same position and with the same rotation
	public void Spawn (Vector3 spawnPosition, Quaternion spawnRotation) {

        for(int i = 0; i < prefabs.Length; i++)
        {
            if (prefabs[i] != null)
            {
                GameObject.Instantiate(prefabs[i], spawnPosition, spawnRotation);
            }
        }
       

	}
}
