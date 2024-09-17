using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    private void Awake()
    {
        // Reference to the Canvas GameObject
        pauseMenuCanvas = gameObject;
        //DontDestroyOnLoad(pauseMenuCanvas);
        pauseMenuCanvas.SetActive(false);
        OnEnable();
    }

    private void Start()
    {
        pauseMenuCanvas = gameObject;    
    }

    private void OnEnable()
    {
        // Subscribe to the GameManager's state change event
        GameManager.instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        GameManager.instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    // Respond to the event when the game state changes
    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.Paused)
        {
            pauseMenuCanvas.SetActive(true);
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
        }
    }
}
