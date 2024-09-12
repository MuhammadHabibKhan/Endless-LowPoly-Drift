using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteerCar : MonoBehaviour
{
    private Vector3 MoveForce;

    Rigidbody m_Rigidbody;
    public float m_Thrust = 120f;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //MoveForce += transform.right * 20f * Input.GetAxis("Horizontal") * Time.deltaTime;
        //transform.localPosition += MoveForce * Time.deltaTime;

        float direction = Input.GetAxis("Horizontal");

        if (direction < 0) m_Rigidbody.AddForce(transform.right * m_Thrust * -1);
        if (direction > 0) m_Rigidbody.AddForce(transform.right * m_Thrust * 1);
    }
}
