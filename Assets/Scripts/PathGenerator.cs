using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PathGenerator : MonoBehaviour
{
    public Transform player;                // Reference to the player's transform
    public List<GameObject> pathPrefabs;    // List of path segment prefabs
    public float segmentLength = 50f;       // Length of each segment
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
        if (Vector3.Distance(player.position, nextSpawnPoint) < spawnDistanceAhead)
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

        // Update the spawn point for the next segment
        nextSpawnPoint = segment.transform.position + new Vector3(0, 0, segmentLength); // Adjust Z for forward movement
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

