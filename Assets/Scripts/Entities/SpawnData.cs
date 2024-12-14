using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Data", menuName = "Asteroids/Spawn Data")]
[System.Serializable]
public class SpawnData : ScriptableObject
{
    public EntityType type;
}