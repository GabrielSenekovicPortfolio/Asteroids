using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScoreManager
{
    void InitializeScore();
    void AddScore(int value);
    void UpdateText();
    void ResetScore();
}
