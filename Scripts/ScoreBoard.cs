using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    int currentScore;
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }
    public void UpdateScore(int amount)
    {
        currentScore += amount;
        scoreText.text = currentScore.ToString();
    }
}
