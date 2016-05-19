using UnityEngine;
using System.Collections;

public class MathAnim : MonoBehaviour {

    //This is the resolution of our Line Renderer
    public int linePoints = 100;
    
    //We can manipulate all those public variables to create different kinds of animated curves
    public float lineLenght = 20f;
    public float amplitudeY = 5f;
    public float amplitudeZ = 5f;
    public float frequencyY = 1f;
    public float frequencyZ = 1f;
    public float speedY = 1f;
    public float speedZ = 1f;
    LineRenderer lRenderer;

    float timerY = 0f;
    float timerZ = 0f;
    Vector3[] positions;

    void SetLinePositions()
    {
        //We first check if a Line Renderer component is attached to the same game object.
        if (lRenderer == null)
        {
            return;
        }

        //Here we update our timers, that we will use to animate the Line Renderer's points
        timerY += Time.deltaTime * speedY;
        timerZ += Time.deltaTime * speedZ;

        //Here we set the values for every Line Renderer point. We use the Mathf.Sin() and Mathf.Cos() functions
        //to calculate the Y and Z positions depending on the current X position and the value of timerY and timerZ.
        //This way we create a periodical curve, animated in time.
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i].x = lineLenght * (float)i / (float)positions.Length;
            positions[i].y = amplitudeY * Mathf.Sin(frequencyY * ((2f*Mathf.PI * (float)i / (float)positions.Length) + timerY));
            positions[i].z = amplitudeZ * Mathf.Cos(frequencyZ * ((2f * Mathf.PI * (float)i / (float)positions.Length) + timerZ));
        }
        //Finally we set the positions of the Line Renderer
        lRenderer.SetPositions(positions);
    }
	void Start () {
        
        //We create a new array. It will be useful for manipulating Line Renderer points' positions.
        positions = new Vector3[linePoints];
        lRenderer = GetComponent<LineRenderer>();
        if (lRenderer != null)
        {
            //If a Line Renderer component is attached to the same game object,
            //we set its vertex count to the linePoints number
            lRenderer.SetVertexCount(linePoints);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //We move our transform back in X, to have it centered on the screen
        transform.position = new Vector3(-0.5f * lineLenght, 0f, 0f);

        //And we call the SetLinePositions() function to calculate Line Renderer's points
        SetLinePositions();

    }
}
