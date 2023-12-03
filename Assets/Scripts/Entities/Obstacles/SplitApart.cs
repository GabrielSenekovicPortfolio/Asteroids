using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using Random = UnityEngine.Random;

public class SplitApart : MonoBehaviour, IDamagable
{
    [Inject] IEntityManager entityManager;

    [SerializeField] LayerMask layerMask;

    IShootProjectile shootProjectile;

    public void TakeDamage(int value = 1)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 randomDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            float addedVelocity = Random.Range(1.0f, 2.0f);
            shootProjectile.Shoot(randomDirection.normalized, addedVelocity);
            entityManager.AddEntity(shootProjectile.GetLastProjectile().GetComponent<EntityIdentifier>());
        }
        if (TryGetComponent(out IScore score))
        {
            score.AddScore();
        }
        gameObject.SetActive(false);
        entityManager.OnDisableEntity();
    }

    private void Awake()
    {
        shootProjectile = GetComponent<IShootProjectile>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            TakeDamage();
        }
    }
}
