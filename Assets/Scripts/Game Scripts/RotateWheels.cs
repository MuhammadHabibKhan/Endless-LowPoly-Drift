using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    GameObject[] wheels;
    bool directionChange = false;
    float oldDirection;
    
    void Start()
    {
        wheels = GameObject.FindGameObjectsWithTag("wheel");       
    }

    public void Rotate(float axis)
    {
        float rotationSpeed = 100f * axis;

        for (int i = 0; i < wheels.Length; i++)
        {
            GameObject wheel = wheels[i];

            if (wheel != null)
            {
                wheel.transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
            }
        }
    }

    public void Tilt(float angle)
    {
        oldDirection = angle;

        for (int i = 0; i < wheels.Length; i++)
        {
            GameObject wheel = wheels[i];

            if (wheel != null)
            {
                wheel.transform.Rotate(0, 1, 0);
            }
        }
    }
}