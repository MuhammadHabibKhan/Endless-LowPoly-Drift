using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class PathGenerator : MonoBehaviour
{
    public Transform car;                // Reference to the car's transform
    public List<GameObject> pathPrefabs;    // List of path segment prefabs
    public int maxSegmentsOnScreen = 5;     // Max number of segments active at once
    public float spawnDistanceAhead = 100f; // How far ahead segments should spawn

    private List<GameObject> activeSegments = new List<GameObject>(); // List of active path segments
    private Vector3 nextSpawnPoint = Vector3.zero; // Where to spawn the next segment
    private float playerDistanceMoved = 0f;       // Keep track of player's movement

    void Start()
    {
        // Initial path generation (start with a few segments)
        for (int i = 0; i < maxSegmentsOnScreen; i++)
        {
            SpawnSegment();
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

        //Fetch the Road Segment Collider from the Prefab List
        Collider pathSegmentCollider;
        pathSegmentCollider = segment.GetComponent<Collider>();

        // Add the new segment to the active list
        activeSegments.Add(segment);

        // Update the spawn point for the next segment
        //nextSpawnPoint = segment.transform.position + new Vector3(0, 0, segmentLength);

        //Fetch the size of the Collider volume
        Vector3 colliderSize;
        colliderSize = pathSegmentCollider.bounds.size;

        Vector3 temp;
        temp = new Vector3(0f, 0f, colliderSize.z);

        nextSpawnPoint = segment.transform.position + temp;

        //Transform endPoint = segment.transform.Find("EndPoint");
        //Debug.Log(endPoint.position.x + " " + endPoint.position.y + " " + transform.position.z);
        //nextSpawnPoint = endPoint.position;
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

