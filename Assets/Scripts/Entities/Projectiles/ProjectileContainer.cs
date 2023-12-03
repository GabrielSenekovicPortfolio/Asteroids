using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
[System.Serializable]
public class ProjectileContainer
{
    [SerializeField] List<GameObject> projectiles = new List<GameObject>();

    int currentIndex = 0;

    public void Start()
    {
        for(int i = 0; i < projectiles.Count; i++)
        {
            projectiles[i].SetActive(false);
        }
    }
    public GameObject GetProjectile()
    {
        currentIndex++;
        currentIndex %= projectiles.Count;
        GameObject projectile = projectiles[currentIndex];
        projectile.SetActive(true);
        return projectile;
    }
    public GameObject GetLastProjectile()
    {
        return projectiles[currentIndex];
    }
}
