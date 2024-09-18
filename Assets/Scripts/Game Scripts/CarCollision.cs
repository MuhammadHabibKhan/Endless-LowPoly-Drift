using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        string collidedObjectName = collision.gameObject.name;

        if (collidedObjectName != "Road")
        {
            Debug.Log(collidedObjectName);
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }
}
