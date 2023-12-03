using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class ShootProjectile : MonoBehaviour, IShootProjectile
{
    [SerializeField] float projectileSpeed;

    [SerializeField] ProjectileContainer projectileContainer;

    private void Awake()
    {
        projectileContainer.Start();
    }

    public void Shoot(Vector2 direction, float addedVelocity = 1)
    {
        GameObject projectile = projectileContainer.GetProjectile();
        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        projectile.transform.position = transform.position;
        projectileBody.velocity = direction.normalized * (projectileSpeed + addedVelocity);
    }

    public GameObject GetLastProjectile()
    {
        return projectileContainer.GetLastProjectile();
    }
}
