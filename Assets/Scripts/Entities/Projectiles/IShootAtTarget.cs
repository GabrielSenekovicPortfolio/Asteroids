using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShootAtTarget
{
    public void SetTarget(Transform target);
    public void SetDirectionTowardsTarget();
    public bool IsTargetWithinAngle();
    public void Shoot();
}
