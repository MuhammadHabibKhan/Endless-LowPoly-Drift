using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
    [SerializeField] private TextMeshProUGUI coinCountText;
    private int highScoreInt;
    private int coinCount;

    public void PlayGame()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Playing);
    }

    private void Start()
    {
        AudioManager.instance.PlayMusic("menu");
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
