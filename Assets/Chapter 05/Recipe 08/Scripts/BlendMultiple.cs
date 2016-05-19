using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlendMultiple : MonoBehaviour {

    //This helper variables store the values of the corresponding parameters
    //in the Animator Controller
    public float Angry = 0f;
    public float Smile = 0f;
    public float BlinkLeft = 0f;
    public float BlinkRight = 0f;
    public float BrowsDown = 0f;

    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();

	}
	void Update () {

        //We set the parameter values in the controller every frame
        anim.SetFloat("Angry", Angry);
        anim.SetFloat("Smile", Smile);
        anim.SetFloat("BlinkLeft", BlinkLeft);
        anim.SetFloat("BlinkRight", BlinkRight);
        anim.SetFloat("BrowsDown", BrowsDown);
    }

    //Functions called by a series of sliders in the UI
    //Used just for easier visualization
    public void SetSmile(Slider slider)
    {
        Smile = slider.value;
    }
    public void SetAngry(Slider slider)
    {
        Angry = slider.value;
    }
    public void SetBlinkLeft(Slider slider)
    {
        BlinkLeft = slider.value;
    }
    public void SetBlinkRight(Slider slider)
    {
        BlinkRight = slider.value;
    }
    public void SetBrowsDown(Slider slider)
    {
        BrowsDown = slider.value;
    }

}
