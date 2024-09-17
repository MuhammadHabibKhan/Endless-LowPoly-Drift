using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseSettings : MonoBehaviour
{
    public void OnPressedClose()
    {
        GameManager.instance.SetGameState(GameManager.GameState.MainMenu);
    }
}
