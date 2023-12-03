using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour, IScoreManager
{
    [SerializeField] TextMeshProUGUI scoreText;

    int scoreValue = 0;

    private void Awake()
    {
        InitializeScore();
    }
    public void AddScore(int value)
    {
        scoreValue += value;
        UpdateText();
    }

    public void InitializeScore()
    {
        ResetScore();
    }

    public void ResetScore()
    {
        scoreValue = 0;
    }

    public void UpdateText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
    }
}