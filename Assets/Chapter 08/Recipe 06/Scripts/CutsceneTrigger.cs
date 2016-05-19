using UnityEngine;
using System.Collections;

public class CutsceneTrigger : MonoBehaviour {

    public Animator cutsceneAnimator;
    public string animationTrigger = "Cutscene";
    public GameObject onScreenInfo;
    public float rotationAdjustmentSpeed = 5f;
    public float positionAdjustmentSpeed = 5f;
    bool inTrigger = false;
    GameObject player;
    // This function is called when we enter the trigger
	void OnTriggerEnter (Collider other) {
        //We check if the object entering the trigger is player
        if (other.gameObject.CompareTag("Player"))
        {
            //Here we remember the player game object
            player = other.gameObject;
            //We set the in trigger flag to true
            inTrigger = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //We check if the object exiting the trigger is player
        if (other.gameObject.CompareTag("Player"))
        {
            //We set the in trigger flag to false
            inTrigger = false;
        }
    }

    // Update is called once per frame
    void Update () {

        if (inTrigger)
        {
            //If our player is in the trigger, we display on screen info
            onScreenInfo.SetActive(true);
            //If player preses space we start the cutscene
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //We disable the trigger collider
                GetComponent<BoxCollider>().enabled = false;
                inTrigger = false;
                //We start a coroutine to adjust player's position and rotation before playing the cutscene
                StartCoroutine("StartCutscene");
            }
        }
        else
        {
            onScreenInfo.SetActive(false);
        }
	}

    IEnumerator StartCutscene()
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        bool positionAdjusted = false;
        bool rotationAdjusted = false;
        while(true)
        {
            yield return null;
            //We check the distance from the player to the cutscene root
            if ((player.transform.position - transform.position).magnitude <= 0.01f)
            {
                //If it's less than 1cm, we set the positionAdjusted flag to true
                positionAdjusted = true;
            }
            else
            {
                //If not, we interpolate player's position to match the cutscene root's position
                player.transform.position = Vector3.Lerp(player.transform.position, transform.position, positionAdjustmentSpeed * Time.deltaTime);
            }
            //We check the agnle between the player's forward and cutscene root's forward
            if (Vector3.Angle(player.transform.forward, transform.forward) <= 1f)
            {
                //If it's less than 1 degree, we set the rotationAdjusted flag to true
                rotationAdjusted = true;
            }
            else
            {
                //If not, we interpolate player's rotation to match the cutscene root's rotation
                player.transform.rotation = Quaternion.Lerp(player.transform.rotation, transform.rotation, positionAdjustmentSpeed * Time.deltaTime);
            }

            //If position and rotation are matched we break the loop
            if (positionAdjusted && rotationAdjusted)
            {
                break;
            }
        }
        //After matching position and rotation we play the cutscene animation
        //for both the cutscene object and player
        player.GetComponent<Animator>().SetTrigger(animationTrigger);
        cutsceneAnimator.SetTrigger(animationTrigger);
    }

}
