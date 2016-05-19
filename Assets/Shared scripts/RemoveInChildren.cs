using UnityEngine;
using System.Collections;

public class RemoveInChildren : MonoBehaviour {
    public enum ComponentToRemove
    {
        Rigidbody,
        Collider,
        Joint,
        MonoBehavior,

    }

    public ComponentToRemove componentToRemove;

    public void RemoveComponents()
    {
        Component[] components = GetComponentsInChildren<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            

            if (components[i].GetType().ToString().Contains(componentToRemove.ToString()))
            {
                Debug.Log("Removing: " + components[i].ToString());
                DestroyImmediate(components[i]);
            }
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
