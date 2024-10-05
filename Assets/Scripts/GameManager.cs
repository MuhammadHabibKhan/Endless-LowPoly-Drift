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
        ReturnMainMenu,
        Playing,
        Paused,
        Resume,
        Settings,
        Rewards,
        GameOver
    }
    public event Action<GameState> OnGameStateChanged; // event for when state changes
    public static GameManager instance;

    public GameState currentState;
    public GameState prevState;

    public float multiplier;
    public float gameTime;

    public int coinCount;
    private int totalCoinCount;

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
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
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

            case GameState.ReturnMainMenu:
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
                Time.timeScale = 0;
                AddScoreCoins();
                score = 0;
                AudioManager.instance.PlaySFX("game-over");
                break;

            case GameState.Settings:
                break;

            case GameState.Rewards:
                break;
        }
        OnGameStateChanged?.Invoke(newState);
        //Debug.Log(newState);
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

    public void AddScore(float amount, float mul)
    {
        multiplier = mul;
        score += (amount * multiplier);

        if (score > PlayerPrefs.GetFloat("highScore", 0f))
        {
            PlayerPrefs.SetFloat("highScore", score);
        }
        PlayerPrefs.SetFloat("currentScore", score);
    }

    public void AddScoreCoins()
    {
        coinCount = (int) (score / 10);
        Debug.Log("count: " + coinCount + " score: " + score);
        //totalCoinCount = PlayerPrefs.GetInt("TotalCoinCount", 0);
        //totalCoinCount += coinCount;
        //PlayerPrefs.SetInt("TotalCoinCount", totalCoinCount);
        AddCoins(coinCount);
    }

    public void AddCoins(int count)
    {
        totalCoinCount = PlayerPrefs.GetInt("TotalCoinCount", 0);
        totalCoinCount += count;
        PlayerPrefs.SetInt("TotalCoinCount", totalCoinCount);
    }

    public void RemoveScore()
    {
        PlayerPrefs.SetFloat("currentScore", 0f);
    }

}
