using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public TextMeshProUGUI highScore;
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
        highScore.text = GameManager.instance.HighScore.ToString();
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
}
