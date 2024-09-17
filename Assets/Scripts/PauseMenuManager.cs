using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public void OnPressResume()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Resume);
    }

    public void OnPressSettings()
    {
        
    }

    public void OnPressMainMenu()
    {
        GameManager.instance.SetGameState(GameManager.GameState.MainMenu);
    }

    public void OnPressQuit()
    {
        Application.Quit();
    }
}
