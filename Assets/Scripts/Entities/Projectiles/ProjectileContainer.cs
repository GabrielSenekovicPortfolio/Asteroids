using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[System.Serializable]
public class ProjectileContainer<T> where T : Enum
{
    List<GameObject> projectiles = new List<GameObject>();

    int currentIndex = 0;

    public void Start(List<T> entities, IEntityManager<T> entityManager)
    {
        for(int i = 0; i < entities.Count; i++)
        {
            entityManager.AddEntity(entities[i], out GameObject result);
            result.SetActive(false);
            projectiles.Add(result);
        }
    }
    public GameObject GetProjectile()
    {
        if(projectiles.Count == 0) { return null; }
        currentIndex++;
        currentIndex %= projectiles.Count;
        GameObject projectile = projectiles[currentIndex];
        return projectile;
    }
    public GameObject GetLastProjectile()
    {
        return projectiles[currentIndex];
    }
}
