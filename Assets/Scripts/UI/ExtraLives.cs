using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLives : MonoBehaviour, IExtraLives
{
    [SerializeField] GameObject extraLifePrefab;
    List<GameObject> extraLifeIcons = new List<GameObject>();

    public void AddExtraLife(int currentHealth)
    {
        extraLifeIcons[currentHealth - 1].SetActive(true);
    }

    public void InitializeExtraLives(int maxHealth, int startHealth)
    {
        for(int i = 0; i < maxHealth; i++)
        {
            GameObject newIcon = Instantiate(extraLifePrefab, transform);
            extraLifeIcons.Add(newIcon);
            if(i >= startHealth)
            {
                newIcon.SetActive(false);
            }
        }
    }

    public void RemoveExtraLife(int currentHealth)
    {
        extraLifeIcons[currentHealth].SetActive(false);
    }
}
