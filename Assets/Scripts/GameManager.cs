using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        Resume,
        GameOver
    }
    public event Action<GameState> OnGameStateChanged; // event for when state changes
    public static GameManager instance;
    public GameState currentState;
    public float gameTime;
    public int cointCount;
    public int HighScore { get; private set; }
    public int score;

    private void Awake()
    {
        // Get high score stored in player prefs
        HighScore = PlayerPrefs.GetInt("High Score", 0);

        // Ensure that only one instance of the GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate instances
        }
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;

        switch (currentState)
        {
            case GameState.MainMenu:
                LoadLevel(0);
                Time.timeScale = 1;
                break;

            case GameState.Playing:
                LoadLevel(1);
                Time.timeScale = 1;
                break;

            case GameState.Paused:
                Time.timeScale = 0; 
                break;

            case GameState.Resume:
                Time.timeScale = 1;
                break;

            case GameState.GameOver:
                score = 0;
                Time.timeScale = 0;
                break;
        }
        OnGameStateChanged?.Invoke(newState);
        Debug.Log(newState);
    }

    public void LoadLevel(int levelNo)
    {
        SceneManager.LoadScene(levelNo);
    }

    void Update()
    {
        if (currentState == GameState.Playing)
        {
            gameTime += Time.deltaTime;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (score > HighScore) HighScore = score;
        PlayerPrefs.SetInt("High Score", HighScore);
        PlayerPrefs.Save();
    }

}
