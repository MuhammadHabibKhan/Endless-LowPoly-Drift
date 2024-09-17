using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class PauseButton : MonoBehaviour
{
    public Button pauseButton;

    public void TogglePause()
    {
        if (GameManager.instance.currentState == GameState.Playing || GameManager.instance.currentState == GameState.Resume)
        {
            GameManager.instance.SetGameState(GameState.Paused);
        }
        else if (GameManager.instance.currentState == GameState.Paused)
        {
            GameManager.instance.SetGameState(GameState.Resume);
        }
    }
}
