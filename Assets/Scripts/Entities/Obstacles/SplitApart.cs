using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Random = UnityEngine.Random;

public class SplitApart : MonoBehaviour, IDamagable
{
    [Inject] IEntityManager<EntityType> entityManager;

    [SerializeField] LayerMask layerMask;

    IShootProjectile<EntityType> shootProjectile;

    public void TakeDamage(int value = 1)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            float addedVelocity = Random.Range(1.0f, 2.0f);
            shootProjectile.Shoot(randomDirection.normalized, addedVelocity);
        }
        if (TryGetComponent(out IScore score))
        {
            score.AddScore();
        }
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
        entityManager.DisableEntity(GetComponent<Entity>());
    }

    private void Awake()
    {
        shootProjectile = GetComponent<IShootProjectile<EntityType>>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)) && gameObject.activeInHierarchy)
        {
            TakeDamage();
        }
    }
}
