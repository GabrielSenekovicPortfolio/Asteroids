using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootProjectile
{
    //Calls Spawn, and makes it move in a given direction with a given velocity
    public void Shoot(Vector2 direction, float addedVelocity = 1);
    public GameObject GetLastProjectile();
}
