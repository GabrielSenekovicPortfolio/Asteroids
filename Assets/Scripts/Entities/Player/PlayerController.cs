using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[DisallowMultipleComponent]
public class PlayerController : MonoBehaviour
{
    [Inject] IGameExiter gameExiter;

    IShootProjectile<EntityType> shootProjectile;
    PlayerMovement movement;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        shootProjectile = GetComponent<ShootProjectile>();
    }
    public void OnTurn(InputValue value)
    {
        movement.SetTurnDirection((int)value.Get<float>());
    }
    public void OnThrust(InputValue value)
    {
        movement.SetThrustOnOff(value.isPressed ? true : false);
    }
    public void OnHyperSpace()
    {
        movement.SetRandomPosition();
    }
    public void OnFire()
    {
        shootProjectile.Shoot(movement.GetFacingDirection());
    }
    public void OnPauseGame()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }
    public void OnQuitGame()
    {
        gameExiter.ExitGame();
    }
}
