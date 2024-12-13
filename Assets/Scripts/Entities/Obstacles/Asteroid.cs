using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : Entity
{
    [SerializeField] LayerMask layerMask;

    IShootProjectile shootProjectile;

    private void Awake()
    {
        shootProjectile = GetComponent<IShootProjectile>();
    }
}
