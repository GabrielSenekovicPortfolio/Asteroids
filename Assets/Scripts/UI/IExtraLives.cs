using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IExtraLives
{
    public void InitializeExtraLives(int maxHealth, int startHealth);
    public void AddExtraLife(int currentHealth);
    public void RemoveExtraLife(int currentHealth);
}
