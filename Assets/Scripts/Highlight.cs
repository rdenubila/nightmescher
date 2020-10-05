using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{

    public float rotationVelocity;
    public float movementVelocity;
    public float movementDistance;
    float angle = 0;
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        angle += movementVelocity;
        transform.Rotate(0, rotationVelocity, 0);
        transform.localPosition = initialPosition + Mathf.Sin(angle * Mathf.Deg2Rad) * Vector3.up * movementDistance;
    }
}
