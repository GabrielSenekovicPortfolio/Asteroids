using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DisableOnCollision : MonoBehaviour, IDamagable
{
    [Inject] IEntityManager<EntityType> entityManager;

    [SerializeField] LayerMask layerMask;

    public void TakeDamage(int value = 1)
    {
        if (TryGetComponent(out IScore score))
        {
            score.AddScore();
        }
        gameObject.SetActive(false);
        entityManager.DisableEntity(GetComponent<Entity>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (layerMask == (layerMask | (1 << collision.gameObject.layer)))
        {
            TakeDamage();
        }
    }
}
