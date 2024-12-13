using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelManager
{
    //For loading separate levels within the game scene
    public Level CurrentLevel { get; set; }
}
