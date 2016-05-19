using UnityEngine;
using System.Collections;

public class LerpNormal : MonoBehaviour {

    RectTransform rectTransform;
    public float speed = 0.5f;
    public float maxPos = 300f;
    public float minPos = -300f;
    float timer = 0f;
    float lerpDir = 1f;
    float posX;
    float posY;
    float posZ;
    // Use this for initialization
    void Start () {

       
        rectTransform = GetComponent<RectTransform>();
        posY = rectTransform.localPosition.y;
        posZ = rectTransform.localPosition.z;

    }

    // Update is called once per frame
    void Update() {

        if (timer > 1f)
        {
            lerpDir = -1f;
        }
        else if (timer < 0f)
        {
            lerpDir = 1f;
        }

        timer += lerpDir*Time.deltaTime * speed;

        posX = Mathf.Lerp(minPos, maxPos, timer);
        rectTransform.localPosition = new Vector3(posX, posY, posZ);

	}
}
