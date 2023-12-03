using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;

public class EntityManager : MonoBehaviour, IEntityManager
{
    [SerializeField] List<EntityIdentifier> entities = new List<EntityIdentifier>();
    [System.Serializable]public struct SpawnData
    {
        public EntityType type;
    }
    [SerializeField] List<SpawnData> spawnData = new List<SpawnData>();

    float amountOfEntitiesToSpawn;
    const int entitiesSpawnedStartValue = 3;
    List<EntityIdentifier> spawnedEntities = new List<EntityIdentifier>();

    private void Awake()
    {
        amountOfEntitiesToSpawn = entitiesSpawnedStartValue;
        StartNewWave();
    }

    public void StartNewWave()
    {
        amountOfEntitiesToSpawn = Mathf.Clamp(amountOfEntitiesToSpawn, 0, entities.Count);
        List<EntityIdentifier> entitiesToSpawn = new List<EntityIdentifier>(entities);
        //Place out the entities randomly
        for (int i = 0; i < (int)amountOfEntitiesToSpawn; i++)
        {
            Vector2 position = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            position *= Random.Range(0.6f, 0.8f);
            position.x += 1; position.y += 1;
            position.x /= 2.0f; position.y /= 2.0f;
            int index = Random.Range(0, entitiesToSpawn.Count);
            entitiesToSpawn[index].gameObject.SetActive(true);
            entitiesToSpawn[index].transform.position = Camera.main.ViewportToWorldPoint(position);
            AddEntity(entitiesToSpawn[index]);
        }
        amountOfEntitiesToSpawn *= 1.15f;
    }
    public void OnDisableEntity()
    {
        if(spawnedEntities.All(s => !s.gameObject.activeSelf))
        {
            spawnedEntities.Clear();
            StartNewWave();
        }
    }

    public void AddEntity(EntityIdentifier entityIdentifier)
    {
        spawnedEntities.Add(entityIdentifier);
    }

    public EntityIdentifier GetEntityOfType(EntityType entityType)
    {
        return entities.FirstOrDefault(e => e.GetEntityType == entityType);
    }

    public void HideAllEntities()
    {
        entities.ForEach(e => e.gameObject.SetActive(false));
        spawnedEntities.Clear();
    }

    public int CountActiveEntitiesOfType(EntityType entityType)
    {
        return spawnedEntities.Where(e => e.GetEntityType == entityType).Count();
    }
}
