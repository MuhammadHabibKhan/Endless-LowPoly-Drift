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

    private RotateWheels wheelScript;

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
            //float Tilt = Vector3.Distance(MoveForce, transform.forward);
            //wheelScript.Tilt(Tilt);
        }
    }
}
