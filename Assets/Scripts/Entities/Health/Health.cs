using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Health : MonoBehaviour, IHealth, IDamagable
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] int maxAmountOfLives;
    int currentHP;

    public void Die()
    {
        if(TryGetComponent(out IScore score))
        {
            score.AddScore();
        }
        gameObject.SetActive(false);
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }

    public void TakeDamage(int value = 1)
    {
        currentHP--;
        if(currentHP <= 0)
        {
            Die();
        }
    }

    private void Awake()
    {
        currentHP = maxAmountOfLives;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            TakeDamage();
        }
    }
}
