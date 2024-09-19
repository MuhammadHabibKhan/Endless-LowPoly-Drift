using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score;

    void Update()
    {
        score = (int) PlayerPrefs.GetFloat("currentScore", 0);
        scoreText.text = score.ToString();
    }
}
