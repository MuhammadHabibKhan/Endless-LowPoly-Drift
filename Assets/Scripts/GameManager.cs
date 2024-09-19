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
        Settings,
        GameOver
    }
    public event Action<GameState> OnGameStateChanged; // event for when state changes
    public static GameManager instance;

    public GameState currentState;
    public GameState prevState;

    public float gameTime;
    public int cointCount;

    //private float highScore;
    private float score = 0;

    private void Awake()
    {
        // Ensure that only one instance of the GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes
        }
        else if (instance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //highScore = PlayerPrefs.GetFloat("highScore", 0f);
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
                AudioManager.instance.StopMusic();
                AudioManager.instance.PlayMusic("background");
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
                AudioManager.instance.PlaySFX("game-over");
                break;

            case GameState.Settings:
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

    public void AddScore(float amount)
    {
        score += amount;
        //Debug.Log("score: " + score + "high: " + highScore);

        if (score > PlayerPrefs.GetFloat("highScore", 0f))
        {
            //Debug.Log("score: " + score + "high: " + highScore);
            PlayerPrefs.SetFloat("highScore", score);
        }
        PlayerPrefs.SetFloat("currentScore", score);
        PlayerPrefs.Save();
    }

    public void RemoveScore()
    {
        PlayerPrefs.SetFloat("currentScore", 0f);
    }

}
