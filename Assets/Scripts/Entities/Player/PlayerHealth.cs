using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PlayerHealth : MonoBehaviour, IDamagable, IPlayerHealth
{
    [SerializeField] LayerMask layerMask;

    IExtraLives extraLives;
    ISceneManager sceneManager;

    const int maxAmountOfLives = 10;
    const int startAmountOfLives = 3;
    int currentHP;

    [Inject]
    public void Initialize(IExtraLives extraLives, ISceneManager sceneManager)
    {
        this.extraLives = extraLives;
        this.sceneManager = sceneManager;

        currentHP = startAmountOfLives;

        extraLives.InitializeExtraLives(maxAmountOfLives, startAmountOfLives);
    }
    public void AddExtraLife()
    {
        if (currentHP < maxAmountOfLives)
        {
            currentHP++;
            extraLives.AddExtraLife(currentHP);
        }
    }

    public void RemoveExtraLife()
    {
        if (currentHP > 0)
        {
            currentHP--;
            extraLives.RemoveExtraLife(currentHP);
        }
        else if (currentHP == 0)
        {
            Die();
        }
    }

    public void TakeDamage(int value = 1)
    {
        RemoveExtraLife();
    }

    public void Die()
    {
        sceneManager.LoadStartScene();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            TakeDamage();
        }
    }

    public int GetCurrentHP()
    {
        return currentHP;
    }
}
