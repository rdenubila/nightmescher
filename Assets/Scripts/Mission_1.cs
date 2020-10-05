using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mission_1 : MonoBehaviour
{

    public PlayableDirector endTimeline;
    public GameObject endTrigger;
    string missionText = "Reach the highest point on the map.";

    string animText1 = "Is this a dream?";
    string animText2 = "I need to wake up!";


    public void StartMission()
    {
        endTrigger.SetActive(true);
        UpdateText();
        AnimatedText();
    }

    public void EndMission()
    {
        endTrigger.SetActive(false);
    }


    public void PlayTimeline()
    {
        endTimeline.Play();
    }

    public void UpdateText()
    {
        GameObject.FindObjectOfType<GameController>().SetMissionText( missionText );
    }

    public void AnimatedText()
    {
        GameObject.FindObjectOfType<GameController>().ShowAnimatedText(animText1, animText2);
    }
}
