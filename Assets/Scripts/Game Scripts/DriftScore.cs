using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftScore : MonoBehaviour
{
    private CarController controller;
    private Vector3 forceNormalized;
    private Vector3 forwardVector;
    private Rigidbody rb;

    float driftResetTimer = 0f;  
    float driftResetThreshold = 1.5f;
    float timeMultiplier = 1f;

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

        if (rb.velocity.magnitude > 0.00001f)
        {
            if (distanceVector <= 0.05f || distanceVector == 2)
            {
                driftResetTimer += Time.deltaTime;

                if (driftResetTimer >= driftResetThreshold)
                {
                    driftResetTimer = 0;
                    timeMultiplier = 1f;
                }
            }
            else
            {
                timeMultiplier += Time.deltaTime;
                GameManager.instance.AddScore(0.001f, timeMultiplier);
            }
        }
        else
        {
            timeMultiplier = 1f;
            driftResetTimer += Time.deltaTime;
        }
    }

}
