using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mission_2 : MonoBehaviour
{

    public GameObject prefab;
    public int collected;
    const int MIN = 10;
    const int MAX = 15;
    int count = 0;
    string missionText = "You lost {0} teeth. Get them back. \n You have collected {1} teeth.";

    string animText1 = "Where are my teeth?";
    string animText2 = "I need to get them back!";

    public void StartMission()
    {
        count = Random.Range(MIN, MAX + 1);
        collected = 0;
        List<GameObject> objs = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
        List<GameObject> selObjs = new List<GameObject>();

        for (int i=0; i< count; i++)
        {
            int index = Random.Range(0, objs.Count);
            selObjs.Add(objs[index]);
            objs.RemoveAt(index);
        }

        foreach(GameObject o in selObjs)
        {
            GameObject newObj = Instantiate(prefab);
            newObj.transform.rotation = o.transform.rotation;
            newObj.transform.position = o.transform.position + o.transform.up * .5f;
        }

        UpdateText();
        AnimatedText();
    }

    public void EndMission()
    {

    }

    public void Collect()
    {
        collected++;

        if (collected == count)
        {
            GameObject.FindObjectOfType<GameController>().EndLevel();
        }

        UpdateText();
    }

    public void UpdateText()
    {
        GameObject.FindObjectOfType<GameController>().SetMissionText(string.Format(missionText, count, collected));
    }

    public void AnimatedText()
    {
        GameObject.FindObjectOfType<GameController>().ShowAnimatedText(animText1, animText2);
    }
}
