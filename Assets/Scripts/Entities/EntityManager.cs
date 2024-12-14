using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Threading.Tasks;
using Unity.Mathematics;

using Random = Unity.Mathematics.Random;
using static UnityEngine.EventSystems.EventTrigger;

public class EntityManager : MonoBehaviour, IEntityManager<EntityType>
{
    [Inject] ILevelManager levelManager;

    [ThreadStatic] Random? random;
    public Random ThreadSafeRandom
    {
        get
        {
            if (random == null)
            {
                random = new Random((uint)(System.DateTime.Now.Ticks + 2 * 12345));
            }
            return random.Value;
        }
    }

    Entity player;
    List<Entity> cachedPlayerPrefabs = new List<Entity>();
    List<Entity> cachedEntityPrefabs = new List<Entity>();
    Dictionary<EntityType, List<Entity>> spawnedEntities = new Dictionary<EntityType, List<Entity>>();
    List<Entity> activeEntities = new List<Entity>();

    private void Awake()
    {
        SpawnPlayer();
    }

    public void StartNewWave()
    {
        activeEntities.Clear();
        if(!PrepareWhatToSpawn(levelManager.CurrentLevel, out Dictionary<EntityType, int> chosenEntities)) { return; }
        //Place out the entities randomly
        foreach(var entity in chosenEntities)
        {
            for(int i = 0; i < entity.Value; i++)
            {
                int amountToCreate = 0;
                if (spawnedEntities.ContainsKey(entity.Key))
                {
                    foreach (var spawnedEntity in spawnedEntities[entity.Key])
                    {
                        spawnedEntity.Activate();
                        activeEntities.Add(spawnedEntity);
                    }
                    amountToCreate = entity.Value - spawnedEntities[entity.Key].Count;
                }
                else
                {
                    spawnedEntities.Add(entity.Key, new List<Entity>());
                    amountToCreate = entity.Value;
                }
                Entity prefab = cachedEntityPrefabs.FirstOrDefault(e => e.GetEntityType == entity.Key);
                if (InGameInstaller.Instance.SpawnEntities(prefab, transform, amountToCreate, out List<Entity> newEntities))
                {
                    spawnedEntities[entity.Key].AddRange(newEntities);
                }
            }
        }
        for(int i = 0; i < activeEntities.Count; i++)
        {
            Vector2 position = new Vector2(ThreadSafeRandom.NextFloat(-1f, 1f), ThreadSafeRandom.NextFloat(-1.0f, 1.0f)).normalized;
            position *= ThreadSafeRandom.NextFloat(0.6f, 0.8f);
            position.x += 1; position.y += 1;
            position.x /= 2.0f; position.y /= 2.0f;
            activeEntities[i].Activate();
            activeEntities[i].Position(Camera.main.ViewportToWorldPoint(position));
        }
    }
    public void SpawnPlayer()
    {
        AddressableLoader<EntityType>.LoadEntityAsync(EntityType.PLAYER).ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Task failed: {task.Exception}");
                return;
            }
            cachedPlayerPrefabs.AddRange(task.Result.OfType<Entity>().ToList());
            InGameInstaller.Instance.SpawnEntities(cachedPlayerPrefabs.FirstOrDefault(p => p.GetEntityType == EntityType.PLAYER), transform, 1, out List<Entity> spawnedEntities);
            player = spawnedEntities[0];
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }
    public void CacheEntities()
    {
        cachedEntityPrefabs.Clear();
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            List<EntityType> entityTypes = levelManager.CurrentLevel.spawnData.Select(s => s.type).Distinct().ToList();
            AddressableLoader<EntityType>.LoadEntitiesAsync(entityTypes).ContinueWith(task =>
            {
                cachedEntityPrefabs.AddRange(task.Result.OfType<Entity>().ToList());

                StartNewWave();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        });
    }

    bool PrepareWhatToSpawn(Level level, out Dictionary<EntityType, int> chosenEntities)
    {
        chosenEntities = new Dictionary<EntityType, int>();
        int upperLimit = level.spawnData.Count;
        for (int i = 0; i < level.amountOfEnemiesToSpawn; i++)
        {
            int randomIndex = ThreadSafeRandom.NextInt(0,upperLimit);
            if (chosenEntities.ContainsKey(level.spawnData[randomIndex].type))
            {
                chosenEntities[level.spawnData[randomIndex].type]++;
            }
            else
            {
                chosenEntities.Add(level.spawnData[randomIndex].type, 1);
            }
        }
        return chosenEntities.Count > 0;
    }
    public void DisableEntity(Entity entity)
    {
        entity.gameObject.SetActive(false);
        activeEntities.Remove(entity);
    }

    public bool AddEntity(EntityType entityType, out GameObject result)
    {
        result = null;
        Entity prefab = cachedEntityPrefabs.FirstOrDefault(e => e.GetEntityType == entityType);
        if(prefab == null)
        {
            prefab = cachedPlayerPrefabs.FirstOrDefault(e => e.GetEntityType == entityType);
        }
        if (prefab != null && InGameInstaller.Instance.SpawnEntities(prefab, transform, 1, out List<Entity> newEntities))
        {
            if(spawnedEntities.ContainsKey(entityType))
            {
                spawnedEntities[entityType].AddRange(newEntities);
            }
            else
            {
                spawnedEntities.Add(entityType, newEntities);
            }
        }
        else
        {
            Debug.LogError(entityType.ToString() + " had not been cashed!");
            return false; 
        }
        result = newEntities[0].gameObject;
        return result != null;
    }
    public void ActivateEntity(EntityType entityType)
    {
        var entity = spawnedEntities[entityType].FirstOrDefault(e => e.GetEntityType == entityType && !e.gameObject.activeInHierarchy);
        if(entity != null)
        {
            activeEntities.Add(entity);
            entity.gameObject.SetActive(true);
        }
    }

    public Entity GetEntityOfType(EntityType entityType)
    {
        return activeEntities.FirstOrDefault(e => e.GetEntityType == entityType);
    }

    public void HideAllEntities()
    {
        foreach(var entity in activeEntities)
        {
            entity.Deactivate();
        }
        activeEntities.Clear();
    }

    public int CountActiveEntitiesOfType(EntityType entityType)
    {
        return activeEntities.Where(e => e.GetEntityType == entityType).Count();
    }
}
