using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Vector3 closedRotation;
    public Vector3 openedRotation;
    public GameObject doorMesh;
    public GameObject lightObj;
    public Transform frontPos;
    public Transform insidePos;
    public bool opened = false;
    bool initialState = false;

    public Door destination;

    void Awake()
    {
        initialState = opened;
    }

    public void Restart()
    {
        if (initialState) Open(); else Close();
    }

    public void Open()
    {
        LeanTween.rotateLocal(doorMesh, openedRotation, 1f).setDelay(1f).setEaseInOutCubic().setOnStart(()=>lightObj.SetActive(true));
        opened = true;
    }

    public void Close()
    {
        LeanTween.rotateLocal(doorMesh, closedRotation, 1f).setDelay(1f).setEaseInOutCubic().setOnComplete(() => lightObj.SetActive(false)); ;
        opened = false;
    }

    public void Switch()
    {
        if (opened)
            Close();
        else
            Open();
    }
}
