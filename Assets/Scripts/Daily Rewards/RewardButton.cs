using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Button[] ButtonList = FindObjectsOfType<Button>();

        for (int i = 0; i < ButtonList.Length; i++)
        {
            if (ButtonList[i].name == "RewardButton")
            {
                button = ButtonList[i];
            }
        }
    }

    public void SetRewardState()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Rewards);
    }
}
