using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public List<Door> doors;

    public float angle = 30f;
    public GameObject haste;
    bool positive = true;
    public Transform playerPos;
    public Transform playerHandPos;

    private void Start()
    {
        haste.transform.localEulerAngles = GetAngle();
    }

    public void Interact()
    {
        foreach(Door door in doors) door.Switch();
        //iTween.RotateTo(haste, GetAngle(), .5f).;
        LeanTween.rotateLocal(haste, GetAngle(), .5f).setDelay(.25f).setEaseInOutCubic();

    }

    Vector3 GetAngle()
    {
        positive = !positive;

        return new Vector3(positive ? angle : -angle, 0, 0);
    }

}
