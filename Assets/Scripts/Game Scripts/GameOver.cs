using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static GameManager;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject AdsManager;

    [SerializeField] private TextMeshProUGUI scoreTextGameOver;
    [SerializeField] private TextMeshProUGUI coinCountGameOver;

    private void Awake()
    {
        // Reference to the Canvas GameObject
        gameOverCanvas = gameObject;
        gameOverCanvas.SetActive(false);
        OnEnable();
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

        // Reset current score to zero when GameOver screen is destroyed
        GameManager.instance.RemoveScore();
    }

    // Respond to the event when the game state changes
    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.GameOver && gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            
            int score = (int)PlayerPrefs.GetFloat("currentScore", 0);
            scoreTextGameOver.text = score.ToString();
            coinCountGameOver.text = GameManager.instance.coinCount.ToString();
            
            PlayerPrefs.Save();
            AdsManager.GetComponent<DisplayInterstitial>().ShowAD();
        }
        else if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

}