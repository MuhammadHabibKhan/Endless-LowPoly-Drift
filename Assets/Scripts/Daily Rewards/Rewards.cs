using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class Rewards : MonoBehaviour
{
    private GameObject rewardCanvas;

    private void Start()
    {
        // Reference to the Canvas GameObject
        rewardCanvas = gameObject;
        rewardCanvas.SetActive(false);
        if (GameManager.instance != null) GameManager.instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnEnable()
    {
        if (GameManager.instance != null)
        {
            // Subscribe to the GameManager's state change event
            GameManager.instance.OnGameStateChanged += HandleGameStateChanged;
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        GameManager.instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    // Respond to the event when the game state changes
    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Rewards)
        {
            rewardCanvas.SetActive(true);

        }
        else
        {
            rewardCanvas.SetActive(false);
        }
    }
}
