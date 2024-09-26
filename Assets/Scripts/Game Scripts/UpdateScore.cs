using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextHUD;
    [SerializeField] private TextMeshProUGUI scoreTextGameOver;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private TextMeshProUGUI coinCountGameOver;

    private int score;
    private int multiplier;

    void Update()
    {
        score = (int) PlayerPrefs.GetFloat("currentScore", 0);
        multiplier = (int) GameManager.instance.multiplier;
        multiplierText.text = multiplier.ToString();
        scoreTextHUD.text = score.ToString();
        scoreTextGameOver.text = score.ToString();
        coinCountGameOver.text = GameManager.instance.coinCount.ToString();
    }
}
