using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    //public Transform car;

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = car.transform.position + new Vector3(0, 4, -10);
    //    //transform.rotate = car.transform.rotation;
    //}

    public Transform car;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    void Update()
    {
        Vector3 desiredPosition = car.position + car.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;

        Quaternion desiredrotation = car.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = desiredrotation;
    }
}
