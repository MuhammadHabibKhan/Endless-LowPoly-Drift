using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftScore : MonoBehaviour
{
    private CarController controller;
    private Vector3 forceNormalized;
    private Vector3 forwardVector;
    private float score = 0;
    private Rigidbody rb;

    float driftResetTimer = 0f;  
    float driftResetThreshold = 1.5f;

    void Start()
    {
        controller = GetComponent<CarController>();
        rb = GetComponent<Rigidbody>();
        GetVector();
    }

    void Update()
    {
        if (GameManager.instance.currentState == GameManager.GameState.Playing || GameManager.instance.currentState == GameManager.GameState.Resume) IsDrift();
    }

    void GetVector()
    {
        forceNormalized = controller.MoveForce.normalized;
        forwardVector = transform.forward;
    }

    void IsDrift()
    {
        GetVector();
        float distanceVector = Vector3.Distance(forceNormalized, forwardVector);

        if (distanceVector > 0.01f && rb.velocity.magnitude > 0.01f)
        {
            score += 0.01f;
            //PlayerPrefs.SetInt("score"
        }
        else if (distanceVector <= 0.01f && rb.velocity.magnitude > 0.01f)
        {
            driftResetTimer += Time.deltaTime;

            if (driftResetTimer >= driftResetThreshold)
            {
                
            }
        }
    }

}
