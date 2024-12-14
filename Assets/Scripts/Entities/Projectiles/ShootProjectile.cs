using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
[DisallowMultipleComponent]
public class ShootProjectile : MonoBehaviour, IShootProjectile<EntityType>, IDependencyGetter<EntityType>
{
    [Inject] IEntityManager<EntityType> entityManager;

    [SerializeField] float projectileSpeed;
    [SerializeField] List<EntityType> projectiles;

    ProjectileContainer<EntityType> projectileContainer;

    [Inject] void Initialize(IEntityManager<EntityType> entityManager)
    {
        projectileContainer = new ProjectileContainer<EntityType>();
        projectileContainer.Start(projectiles, entityManager);
    }

    public void Shoot(Vector2 direction, float addedVelocity = 1)
    {
        GameObject projectile = projectileContainer.GetProjectile();
        Rigidbody2D projectileBody = projectile.GetComponent<Rigidbody2D>();
        entityManager.ActivateEntity(GetLastProjectile().GetComponent<EntityIdentifier<EntityType>>().GetEntityType);
        projectile.transform.position = transform.position;
        projectileBody.velocity = direction.normalized * (projectileSpeed + addedVelocity);
    }

    public GameObject GetLastProjectile()
    {
        return projectileContainer.GetLastProjectile();
    }

    public List<EntityType> GetDependencies() => projectiles.Distinct().ToList();
}
