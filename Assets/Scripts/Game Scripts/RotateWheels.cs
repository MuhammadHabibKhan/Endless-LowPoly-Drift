using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheels : MonoBehaviour
{
    GameObject[] wheels;
    bool rightFlag = false;
    bool leftFlag = false;
    
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

    public void TiltWheels(float steerDirection)
    {
        GameObject leftWheel = wheels[0];
        GameObject rightWheel = wheels[1];

        for (int i = 0; i < wheels.Length; i++)
        {
            GameObject wheel = wheels[i];

            if (wheel.name == "05.wheel_fl") leftWheel = wheel;
            else if (wheel.name == "05.wheel_fr") rightWheel = wheel;
        }

        if (steerDirection < 0 && leftFlag == false) // Turning right
        {
            // Tilt wheels to the right (negative Y-axis)
            Vector3 rotationOffset = new Vector3(0, -40, 0); // Y rotation to the right
            Quaternion desiredRotation = Quaternion.Euler(rotationOffset);
            leftWheel.transform.localRotation = desiredRotation;
            rightWheel.transform.localRotation = desiredRotation;

            // Update the flags
            leftFlag = true;
            rightFlag = false;
        }
        else if (steerDirection > 0 && rightFlag == false) // Turning left
        {
            // Tilt wheels to the left (positive Y-axis)
            Vector3 rotationOffset = new Vector3(0, 40, 0); // Y rotation to the left
            Quaternion desiredRotation = Quaternion.Euler(rotationOffset);
            leftWheel.transform.localRotation = desiredRotation;
            rightWheel.transform.localRotation = desiredRotation;

            // Update the flags
            leftFlag = false;
            rightFlag = true;
        }
        else if (steerDirection == 0 && (leftFlag || rightFlag)) // Steering is neutral
        {
            // Reset the wheel rotation to straight
            Quaternion defaultRotation = Quaternion.Euler(Vector3.zero);
            leftWheel.transform.localRotation = defaultRotation;
            rightWheel.transform.localRotation = defaultRotation;

            // Reset the flags
            rightFlag = false;
            leftFlag = false;
        }
    }
        
}

