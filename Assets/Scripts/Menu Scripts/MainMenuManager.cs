using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI coinCountText;
    private int highScoreInt;
    private int coinCount;

    private void OnEnable()
    {
        if (GameManager.instance != null) GameManager.instance.OnGameStateChanged += HandleGameStateChanged;
    }

    private void OnDestroy()
    {
        GameManager.instance.OnGameStateChanged -= HandleGameStateChanged;
    }

    private void HandleGameStateChanged(GameState newState)
    {
        if (newState == GameState.MainMenu || newState == GameState.ReturnMainMenu)
        {
            DisplayCoinCount();
            DisplayHighScore();
        }
    }

    public void PlayGame()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Playing);
    }

    private void Start()
    {
        AudioManager.instance.PlayMusic("menu");
        OnEnable();
        DisplayHighScore();
        DisplayCoinCount();
    }

    // For Android Back button exit 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit(); 
        }
    }

    public void OnPressedSettings()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Settings);
    }

    void DisplayHighScore()
    {
        highScoreInt = (int) PlayerPrefs.GetFloat("highScore", 0);
        highScore.text = highScoreInt.ToString();
    }

    void DisplayCoinCount()
    {
        coinCount = PlayerPrefs.GetInt("TotalCoinCount", 0);
        coinCountText.text = coinCount.ToString();
    }
}
