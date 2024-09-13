using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class PathGenerator : MonoBehaviour
{
    public Transform car;                // Reference to the car's transform
    public List<GameObject> pathPrefabs;    // List of path segment prefabs
    public int maxSegmentsOnScreen = 10;     // Max number of segments active at once
    public float spawnDistanceAhead = 1f; // How far ahead segments should spawn

    private List<GameObject> activeSegments = new List<GameObject>(); // List of active path segments
    private Vector3 nextSpawnPoint = Vector3.zero; // Where to spawn the next segment
    //private float playerDistanceMoved = 0f;       // Keep track of player's movement

    void Start()
    {
        //Application.targetFrameRate = 60;
        //nextSpawnPoint = car.position;

        // Initial path generation (start with a few segments)
        for (int i = 0; i < maxSegmentsOnScreen; i++)
        {
            //SpawnSegment();
        }
    }

    void Update()
    {
        // Check if the player has moved far enough to spawn a new segment
        if (Vector3.Distance(car.position, nextSpawnPoint) < spawnDistanceAhead)
        {
            SpawnSegment();
            RemoveOldSegment();
        }
    }

    // Function to spawn a new segment
    void SpawnSegment()
    {
        // Select a random segment prefab from the list
        GameObject segment = Instantiate(pathPrefabs[Random.Range(0, pathPrefabs.Count)], nextSpawnPoint, Quaternion.identity);

        // Add the new segment to the active list
        activeSegments.Add(segment);

        ////Fetch the Road Segment Collider from the Prefab List
        //Collider pathSegmentCollider;
        //pathSegmentCollider = segment.GetComponent<Collider>();

        ////Update the spawn point for the next segment

        ////Fetch the size of the Collider volume
        //Vector3 colliderSize;
        //colliderSize = pathSegmentCollider.bounds.size;

        //// Get distance from the z axis
        //Vector3 distanceToNextSegment;
        //distanceToNextSegment = new Vector3(0f, 0f, colliderSize.z);

        //// Update the next spawn point
        //nextSpawnPoint = segment.transform.position + distanceToNextSegment;

        //Update the next spawn point using the EndPoint of the current segment
        Transform endPoint = segment.transform.Find("EndPoint");

        if (endPoint != null)
        {
            nextSpawnPoint = new Vector3 (0f, 0f, endPoint.position.z + endPoint.position.z);
            //Debug.Log(nextSpawnPoint.z);
        }
        else
        {
            Debug.LogError("EndPoint not found on segment prefab!");
        }
    }

    // Function to remove the oldest segment (when it is out of view)
    void RemoveOldSegment()
    {
        if (activeSegments.Count > maxSegmentsOnScreen)
        {
            Destroy(activeSegments[0]);
            activeSegments.RemoveAt(0);
        }
    }
}

