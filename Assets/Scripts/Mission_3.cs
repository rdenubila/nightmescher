using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mission_3 : MonoBehaviour
{

    public GameObject prefab;
    public int collected;
    const int MIN = 10;
    const int MAX = 15;
    int count = 0;
    string missionText = "Be seen naked by {0} people. \n You have been seen by {1} people";

    string animText1 = "My God, I'm naked!";
    string animText2 = "They're all looking at me";

    public void StartMission()
    {

        GameObject.FindObjectOfType<GameController>().playerClothes.SetActive(false);

        count = Random.Range(MIN, MAX + 1);
        collected = 0;
        List<GameObject> objs = GameObject.FindGameObjectsWithTag("EnemySpawnPoint").ToList();
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
            newObj.transform.position = o.transform.position;
        }

        UpdateText();
        AnimatedText();
    }

    public void EndMission()
    {
        List<GameObject> objs = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        foreach (GameObject o in objs)
        {
            Destroy(o);
        }

        GameObject.FindObjectOfType<GameController>().playerClothes.SetActive(true);

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
        GameObject.FindObjectOfType<GameController>().SetMissionText( string.Format(missionText, count, collected) );
    }

    public void AnimatedText()
    {
        GameObject.FindObjectOfType<GameController>().ShowAnimatedText(animText1, animText2);
    }
}
