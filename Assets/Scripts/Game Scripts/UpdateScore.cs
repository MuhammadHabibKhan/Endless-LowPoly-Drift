using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTextHUD;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private int score;
    private int multiplier;

    void Update()
    {
        score = (int) PlayerPrefs.GetFloat("currentScore", 0);
        multiplier = (int) GameManager.instance.multiplier;
        multiplierText.text = multiplier.ToString();
        scoreTextHUD.text = score.ToString();
    }
}
