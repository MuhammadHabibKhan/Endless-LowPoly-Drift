using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerCar : MonoBehaviour
{
    private Vector3 MoveForce;

    Rigidbody m_Rigidbody;
    float force = 120f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        Input.gyro.enabled = true;
    }

    void Update()
    {
        //float direction = Input.GetAxis("Horizontal");
        float direction = Input.gyro.attitude.x;

        if (direction > 0) m_Rigidbody.AddForce(transform.right * force * -1);
        if (direction < 0) m_Rigidbody.AddForce(transform.right * force);
    }
}
