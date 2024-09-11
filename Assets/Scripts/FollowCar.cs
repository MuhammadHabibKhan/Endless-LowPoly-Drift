using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour
{
    public Transform car;

    // Update is called once per frame
    void Update()
    {
        transform.position = car.transform.position + new Vector3(0, 1, -5);
        //transform.rotate = car.transform.rotation;
    }
}
