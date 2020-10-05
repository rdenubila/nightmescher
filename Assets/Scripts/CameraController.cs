using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float horizontalMin = 0;
    public float horizontalMax = 0;
    public float verticalMin = 0;
    public float verticalMax = 0;
    public float easing = .2f;


    // Update is called once per frame
    void Update()
    {
        float xPos = Input.mousePosition.x / Screen.width;
        xPos = Mathf.Clamp(xPos, 0f, 1f);

        float yPos = Input.mousePosition.y / Screen.height;
        yPos = Mathf.Clamp(yPos, 0f, 1f);

        Vector3 dest = new Vector3( 
                        -horizontalMin + xPos*(horizontalMin+horizontalMax),
                        -verticalMin + yPos * (verticalMin + verticalMax), 
                        0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, dest, easing);
    }
}
