using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System.Linq;
using DG.Tweening;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EntityManager : MonoBehaviour, IEntityManager
{
    [Inject] ILevelManager levelManager;

    List<Entity> cachedEntityPrefabs = new List<Entity>();
    Dictionary<EntityType, List<Entity>> spawnedEntities = new Dictionary<EntityType, List<Entity>>();
    List<Entity> activeEntities = new List<Entity>();

    public void StartNewWave()
    {
        activeEntities.Clear();
        if(!PrepareWhatToSpawn(levelManager.CurrentLevel, out Dictionary<EntityType, int> chosenEntities)) { return; }
        //Place out the entities randomly
        foreach(var entity in chosenEntities)
        {
            for(int i = 0; i < entity.Value; i++)
            {
                foreach(var spawnedEntity in spawnedEntities[entity.Key])
                {
                    spawnedEntity.Activate();
                    activeEntities.Add(spawnedEntity);
                }
                int amountToCreate = entity.Value - spawnedEntities[entity.Key].Count;
                Entity prefab = cachedEntityPrefabs.FirstOrDefault(e => e.GetEntityType == entity.Key);
                if(InGameInstaller.Instance.SpawnEntities(prefab, transform, amountToCreate, out List<Entity> newEntities))
                {
                    spawnedEntities[entity.Key].AddRange(newEntities);
                }
            }
        }
        for(int i = 0; i < activeEntities.Count; i++)
        {
            Vector2 position = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            position *= Random.Range(0.6f, 0.8f);
            position.x += 1; position.y += 1;
            position.x /= 2.0f; position.y /= 2.0f;
            activeEntities[i].Activate();
            activeEntities[i].Position(Camera.main.ViewportToWorldPoint(position));
        }
    }
    public void CacheEntities()
    {
        cachedEntityPrefabs.Clear();
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            LoadEntitiesAsync().ContinueWith(task =>
            {
                cachedEntityPrefabs = task.Result;

                StartNewWave();
            });
        });
    }
    async System.Threading.Tasks.Task<List<Entity>> LoadEntitiesAsync()
    {
        List<Entity> entities = new List<Entity>();
        for(int i = 0; i < levelManager.CurrentLevel.spawnData.Count; i++)
        {
            AsyncOperationHandle<Entity> handle = Addressables.LoadAssetAsync<Entity>(levelManager.CurrentLevel.spawnData[i].ToString());
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                entities.Add(handle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load level: {levelManager.CurrentLevel.spawnData[i].ToString()}");
                return null;
            }
        }
        return entities;
    }

    bool PrepareWhatToSpawn(Level level, out Dictionary<EntityType, int> chosenEntities)
    {
        chosenEntities = new Dictionary<EntityType, int>();
        for(int i = 0; i < level.amountOfEnemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0,level.spawnData.Count);
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
    public void OnDisableEntity()
    {
        if (!spawnedEntities.All(s => s.Value.All(z => !z.IsActive())))
        {
            spawnedEntities.Clear();
            StartNewWave();
        }
    }

    public void AddEntity(EntityIdentifier entityIdentifier)
    {
        //spawnedEntities.Add(entityIdentifier);
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
