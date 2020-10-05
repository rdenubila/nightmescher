using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public ColorPalette[] colorPalettes;
    public Transform playerStartPosition;
    public GameObject generalCam;
    public GameObject playerClothes;
    public Player _player;
    public PlayableDirector initialCutScene;
    public int mission = 0;
    public Mission_1 m1;
    public Mission_2 m2;
    public Mission_3 m3;
    public Text missionlabel;
    public Text animatedText1;
    public Text animatedText2;

    private void Start()
    {
        ShowGeneralCamera(10f);
        RestartLevel();
    }

    void RestartLevel()
    {
        RandomColors();
        _player.transform.position = playerStartPosition.position;
        if(_player.navMeshAgent) _player.navMeshAgent.Warp(playerStartPosition.position);
        initialCutScene.Play();

        Door[] doors = GameObject.FindObjectsOfType<Door>();
        foreach (Door door in doors) door.Restart();

        mission++;
        if (mission > 3) mission = 1;

        switch (mission)
        {
            case 1:
                m1.StartMission();
                break;
            case 2:
                m2.StartMission();
                break;
            case 3:
                m3.StartMission();
                break;
        }
    }

    public void SetMissionText(string txt)
    {
        missionlabel.text = txt;
    }

    public void ShowAnimatedText(string txt1, string txt2)
    {

        animatedText1.text = txt1;
        animatedText2.text = txt2;

        animatedText1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -650);
        animatedText2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -650);

        LeanTween.move(animatedText1.GetComponent<RectTransform>(), new Vector2(0, 250), .5f).setDelay(.25f).setEaseInOutCubic();
        LeanTween.move(animatedText1.GetComponent<RectTransform>(), new Vector2(0, 650), .5f).setDelay(2f).setEaseInOutCubic();

        LeanTween.move(animatedText2.GetComponent<RectTransform>(), new Vector2(0, 250), .5f).setDelay(2f).setEaseInOutCubic();
        LeanTween.move(animatedText2.GetComponent<RectTransform>(), new Vector2(0, 650), .5f).setDelay(2f+1f).setEaseInOutCubic();
    }

    public void EndLevel()
    {

        switch (mission)
        {
            case 1:
                m1.EndMission();
                break;
            case 2:
                m2.EndMission();
                break;
            case 3:
                m3.EndMission();
                break;
        }

        RestartLevel();
    }

    void RandomColors()
    {
        GameObject[] objs;
        ColorPalette c = colorPalettes[Random.Range(0, colorPalettes.Length)];

        // CENA
        objs = GameObject.FindGameObjectsWithTag("Scene");
        foreach (GameObject o in objs) o.GetComponent<Renderer>().material.SetColor("_BaseColor", c.scene);

        // Props
        objs = GameObject.FindGameObjectsWithTag("Props");
        foreach (GameObject o in objs) o.GetComponent<Renderer>().material.SetColor("_BaseColor", c.props);

        // Interact
        objs = GameObject.FindGameObjectsWithTag("Interact");
        foreach (GameObject o in objs) o.GetComponent<Renderer>().material.SetColor("_BaseColor", c.interact);

        // Player
        objs = GameObject.FindGameObjectsWithTag("Cloths");
        foreach (GameObject o in objs)
        {
            o.GetComponent<Renderer>().material.SetColor("_PrimaryColor", c.playerPrimary);
            o.GetComponent<Renderer>().material.SetColor("_SecondaryColor", c.playerSecondary);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                _player.ClearInteractions();
                Debug.LogWarning(hit.collider.tag);
                if (hit.collider.CompareTag("Lever"))
                {
                    _player.InteractLever(hit.collider.GetComponent<Lever>());
                }
                else if(hit.collider.CompareTag("Door"))
                {
                    Door d = hit.collider.GetComponent<Door>();
                    if (d.opened)
                        _player.SetDoor(d);
                    else
                        _player.SetDestination(d.frontPos.position);
                }
                else
                {
                    _player.SetDestination(hit.point);
                }
            }
        }
    }

    public void ShowGeneralCamera(float time, float delay)
    {
        StartCoroutine(ProcessGeneralCamera(time, delay));
    }

    public void ShowGeneralCamera(float time)
    {
        StartCoroutine(ProcessGeneralCamera(time, 0f));
    }

    IEnumerator ProcessGeneralCamera(float time, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        generalCam.SetActive(true);
        yield return new WaitForSeconds(time);
        generalCam.SetActive(false);

    }
}
