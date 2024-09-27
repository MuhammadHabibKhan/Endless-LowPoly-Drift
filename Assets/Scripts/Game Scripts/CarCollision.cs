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
            //Debug.Log(collidedObjectName);
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
        }
    }

    //private void Update()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position, Vector3.down, out hit))
    //    {
    //        while (hit.collider.name == "Road")
    //        {
    //            continue;
    //        }
    //        GameManager.instance.SetGameState(GameManager.GameState.GameOver);
    //    }
    //}
}
