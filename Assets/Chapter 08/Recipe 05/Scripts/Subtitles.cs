using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Subtitles : MonoBehaviour {

    string subtitlesText;
    public Text textComponent;
    public GameObject subtitlesContainer;

    public void SetSubtitles(string text)
    {
        subtitlesText = text;
    }

	void Update () {
        //If we don't have any text to show, we disable subtitles
        if (string.IsNullOrEmpty(subtitlesText) && subtitlesContainer.activeSelf)
        {
            subtitlesContainer.SetActive(false);
        }
        //If we have a text to show, we enable subtitles if they were disabled
        else if (!string.IsNullOrEmpty(subtitlesText) && !subtitlesContainer.activeSelf)
        {
            subtitlesContainer.SetActive(true);
        }
        //We set the textComponent's text to our subtitles
        else
        {
            textComponent.text = subtitlesText;
        }

	}
}
