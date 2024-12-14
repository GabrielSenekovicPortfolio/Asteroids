using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressableLoader<T> where T : Enum
{
    public static async Task<List<EntityIdentifier<T>>> LoadEntityAsync(T entityType)
    {
        List<EntityIdentifier<T>> entities = new List<EntityIdentifier<T>>();
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(entityType.ToString());
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            EntityIdentifier<T> entity = handle.Result.GetComponent<EntityIdentifier<T>>();
            if (entity.GetDependencies(out var dependencies))
            {
                entities.AddRange(await LoadEntitiesAsync(dependencies));
            }
            entities.Add(handle.Result.GetComponent<EntityIdentifier<T>>());
        }
        else
        {
            Debug.LogError($"Failed to load entity: {entityType.ToString()}");
            return null;
        }
        return entities;
    }
    public static async Task<List<EntityIdentifier<T>>> LoadEntitiesAsync(List<T> entityTypes)
    {
        List<EntityIdentifier<T>> entities = new List<EntityIdentifier<T>>();
        for (int i = 0; i < entityTypes.Count; i++)
        {
            if (entities.Any(e => EqualityComparer<T>.Default.Equals(entityTypes[i], e.GetEntityType))) { continue; }
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(entityTypes[i].ToString());
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                EntityIdentifier<T> entity = handle.Result.GetComponent<EntityIdentifier<T>>();
                if (entity.GetDependencies(out var dependencies))
                {
                    entities.AddRange(await LoadEntitiesAsync(dependencies));
                }
                entities.Add(handle.Result.GetComponent<EntityIdentifier<T>>());
            }
            else
            {
                Debug.LogError($"Failed to load entity: {entityTypes[i].ToString()}");
                return null;
            }
        }
        return entities;
    }
}
