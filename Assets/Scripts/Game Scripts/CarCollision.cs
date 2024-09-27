using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    private Rigidbody carRigidbody;
    private bool overFlag = false;

    private void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        string collidedObjectName = collision.gameObject.name;

        // use CompareTag for better performance??
        if (collidedObjectName != "Road")
        {
            //Debug.Log(collidedObjectName);
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    private void Update()
    {
        if (!overFlag)
        {
            RaycastHit hit;
            float rayLength = 20f;

            if (carRigidbody.velocity.y < 0)
            {
                if (!Physics.Raycast(transform.position, Vector3.down, out hit, rayLength))
                {
                    GameManager.instance.SetGameState(GameManager.GameState.GameOver);
                    overFlag = true;
                }
            }
        }
    }
}