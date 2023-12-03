using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Score : MonoBehaviour, IScore
{
    [Inject] IScoreManager scoreManager;

    [SerializeField] int score;
    public void AddScore()
    {
        scoreManager.AddScore(score);
    }
}
