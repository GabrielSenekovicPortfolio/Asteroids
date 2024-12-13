using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EntityManager;

[CreateAssetMenu(fileName = "Level", menuName = "Asteroids/Level")]
public class Level : ScriptableObject
{
    public int amountOfEnemiesToSpawn;
    public List<SpawnData> spawnData = new List<SpawnData>();
}
