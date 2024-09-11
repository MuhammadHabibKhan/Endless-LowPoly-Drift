using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float MoveSpeed = 100;
    public float MaxSpeed = 18;
    public float Drag = 0.999f;
    public float SteerAngle = 30;
    public float Traction = 0.001f;

    // Variables
    private Vector3 MoveForce;

    // Update is called once per frame
    void Update()
    {
        // Moving
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;

        // Steering
        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed); // cap the MoveForce magnitude to MaxSpeed

        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);

        // Using Lerp (linear interpolation) we avoid the drift continuing sideways and bring the move direction back to the forward direction and make it the new move direction
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
    }
}
