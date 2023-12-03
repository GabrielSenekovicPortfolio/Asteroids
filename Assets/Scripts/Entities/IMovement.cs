using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    public void SetTurnDirection(int value);
    public void SetThrustOnOff(bool value);
    public Vector2 GetFacingDirection();
}
