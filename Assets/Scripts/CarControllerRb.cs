using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarControllerRb : MonoBehaviour
{
    public float MoveSpeed = 100f;
    public float MaxSpeed = 18f;
    public float Drag = 0.991f;
    public float SteerAngle = 20f;
    public float Traction = 0.01f;

    private Rigidbody rb;
    private Vector3 MoveForce;

    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Optional: Set drag on the Rigidbody itself if you want
        rb.drag = 0; // We'll handle drag manually in the code
    }

    void Update()
    {
        // Steering input (no need to add physics here, we use Rigidbody for rotation)
        float steerInput = Input.GetAxis("Horizontal");

        // Steering
        Quaternion turnRotation = Quaternion.Euler(0f, steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Traction: Bringing the MoveForce vector back in line with the car's forward direction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);

        // Adjust the force to face forward using Lerp for smooth drift correction
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
    }

    void FixedUpdate()
    {
        // Moving: Add force forward based on vertical input (throttle/brake)
        float moveInput = Input.GetAxis("Vertical");
        MoveForce += transform.forward * moveInput * MoveSpeed * Time.fixedDeltaTime;

        // Apply force to Rigidbody for movement
        rb.AddForce(MoveForce * Time.fixedDeltaTime, ForceMode.VelocityChange);

        // Clamp the velocity to MaxSpeed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);

        // Apply manual drag to gradually slow down
        MoveForce *= Drag;
    }
}
