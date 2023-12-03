using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using Zenject;

public class ShootAtTarget : MonoBehaviour, IShootAtTarget
{
    IShootProjectile shootProjectile;
    IMovement movement;

    Transform target;

    [SerializeField] float angle;
    [SerializeField] float shootTimerMax;
    float shootTimer;

    [Inject]
    public void Init(IPlayerFetcher playerFetcher)
    {
        SetTarget(playerFetcher.FetchPlayer());
    }


    public void SetDirectionTowardsTarget()
    {
        Vector2 right = Quaternion.Euler(0, 0, 90) * movement.GetFacingDirection();
        Vector2 directionToTarget = (target.transform.position - transform.position).normalized;
        int directionToTurn = Vector3.Dot(right, directionToTarget) < 0 ? -1 : 1;
        movement.SetTurnDirection(directionToTurn);
    }

    public bool IsTargetWithinAngle()
    {
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        Vector3 facingDirecton = movement.GetFacingDirection();
        float angleToTarget = Vector2.Angle(facingDirecton, directionToTarget);
        return angleToTarget <= angle;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void Shoot()
    {
        shootProjectile.Shoot(movement.GetFacingDirection());
    }

    private void Awake()
    {
        movement = GetComponent<IMovement>();
        shootProjectile = GetComponent<IShootProjectile>();
    }

    private void Update()
    {
        if(IsTargetWithinAngle())
        {
            movement.SetThrustOnOff(true);
            movement.SetTurnDirection(0);
            if(shootTimer >= shootTimerMax)
            {
                Shoot();
                shootTimer = 0;
            }
        }
        else
        {
            movement.SetThrustOnOff(false);
            SetDirectionTowardsTarget();
        }
        shootTimer += Time.deltaTime;
    }
}
