using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScore;
    private int highScoreInt;

    public void PlayGame()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Playing);
    }

    private void Start()
    {
        if (!AudioManager.instance.musicSource.isPlaying)
        {
            AudioManager.instance.PlayMusic("menu");
        }
        displayHighScore();
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

    void displayHighScore()
    {
        highScoreInt = (int) PlayerPrefs.GetFloat("highScore", 0);
        highScore.text = highScoreInt.ToString();
    }
}
