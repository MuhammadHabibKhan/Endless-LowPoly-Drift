using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class SettingsMenus : MonoBehaviour
{
    private GameObject settingsCanvas;

    // Using Start instead of Awake here bcs GameManager is not initialized if in Awake()
    private void Start()
    {
        settingsCanvas = gameObject;
        settingsCanvas.SetActive(false);
        // Subscribe to event
        if (GameManager.instance != null) GameManager.instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        GameManager.instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    // Respond to the event when the game state changes
    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Settings && settingsCanvas != null)
        {
            settingsCanvas.SetActive(true);
        }
        else if (settingsCanvas != null)
        {
            settingsCanvas.SetActive(false);
        }
    }

    public void OnPressedClose()
    {
        GameManager.instance.SetGameState(GameManager.GameState.ReturnMainMenu);
    }
}
