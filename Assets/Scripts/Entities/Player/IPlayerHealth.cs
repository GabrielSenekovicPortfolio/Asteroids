using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerHealth : IHealth
{
    public void AddExtraLife();
    public void RemoveExtraLife();
}
