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
    }

    void Update()
    {
        float direction = Input.GetAxis("Horizontal");

        if (direction < 0) m_Rigidbody.AddForce(transform.right * force * -1);
        if (direction > 0) m_Rigidbody.AddForce(transform.right * force);
    }
}
