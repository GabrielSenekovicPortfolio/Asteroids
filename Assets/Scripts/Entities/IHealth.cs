using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{
    //Use on enemy ships
    public void Die();
    public int GetCurrentHP();
}
