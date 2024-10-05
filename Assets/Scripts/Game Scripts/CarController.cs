using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float MoveSpeed;
    public float MaxSpeed;
    public float Drag;
    public float SteerAngle;
    public float Traction;
    public int start;

    private RotateWheels wheelScript;

    // Variables
    public Vector3 MoveForce;

    private float horizontalInput = 0f; // Simulates the horizontal axis input (-1 for left, 1 for right)
    private float targetHorizontalInput = 0f; // Target value for smooth turning
    public float turnSmoothSpeed = 2f; // Smoothing speed for turning

    void Start()
    {
        // Enable the gyroscope
        Input.gyro.enabled = true;
    }

    // Functions to be called on button press/release
    public void MoveRightPressed()
    {
        targetHorizontalInput = 1; // Move Right
    }

    public void MoveLeftPressed()
    {
        targetHorizontalInput = -1; // Move Left
    }

    public void StopMovement()
    {
        targetHorizontalInput = 0; // Stop moving
    }

    // Update is called once per frame
    void Update()
    {
        // Smoothly interpolate between current and target horizontal input
        horizontalInput = Mathf.Lerp(horizontalInput, targetHorizontalInput, turnSmoothSpeed * Time.deltaTime);

        // Moving
        //MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        //transform.position += MoveForce * Time.deltaTime;

        // Steering
        //float steerInput = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Moving (using simulated vertical input from UI buttons)
        //MoveForce += transform.forward * MoveSpeed * verticalInput * Time.deltaTime;
        MoveForce += transform.forward * MoveSpeed * Time.deltaTime * start;
        transform.position += MoveForce * Time.deltaTime;

        //float steerInput = -Input.gyro.attitude.x * 1.2f; // Gyroscope's yaw value (rotation around Y-axis)
        //transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        transform.Rotate(Vector3.up * horizontalInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        // Drag and max speed limit
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed); // cap the MoveForce magnitude to MaxSpeed

        // Traction
        Debug.DrawRay(transform.position, MoveForce.normalized * 3, Color.red);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);

        // Using Lerp (linear interpolation) we avoid the drift continuing sideways and bring the move direction back to the forward direction and make it the new move direction
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;
        
        // Rotate and Tilt Wheels
        GameObject Wheel = GameObject.FindGameObjectWithTag("wheel");
        wheelScript = Wheel.GetComponent<RotateWheels>();
        
        if (wheelScript != null)
        {
            wheelScript.Rotate(Input.GetAxis("Vertical"));
            //wheelScript.TiltWheels(steerInput);
            wheelScript.TiltWheels(horizontalInput);
        }
    }
}
