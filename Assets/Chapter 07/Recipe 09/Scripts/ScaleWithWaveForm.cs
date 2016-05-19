using UnityEngine;
using System.Collections;

public class ScaleWithWaveForm : MonoBehaviour
{
    
    public AudioSource audioSource;
    public float lerpSpeed = 100f;
    float[] samples;
    float currentSample;
    void Start()
    {
        //We create an array for the audio sample
        //The size in samples is equal to 512 - we will use it to avarage the samples
        samples = new float[1024];
        StartCoroutine("SampleAudio");
    }

    IEnumerator SampleAudio()
    {
        while (true)
        {
            //In this coroutine, we get the data from the audioSource
            audioSource.GetOutputData(samples, 0);
            currentSample = 0f;
            for (int i = 0; i < samples.Length; i++)
            {
                //Here we sum up the squared values of the samples
                currentSample += (samples[i]* samples[i]);
            }
            //And calculate the average RMS value
            currentSample = Mathf.Sqrt(currentSample / samples.Length);

            //Here we scale the transform and use the Vector3.Lerp function to make the changes more smooth. 
            //We also multiply the currentSample value by 10, to make the changes more visible. 
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * currentSample*10f, Time.deltaTime*lerpSpeed);
            yield return null;
        }
    }
}