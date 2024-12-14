using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InGameInstaller : MonoInstaller<InGameInstaller>
{
    static InGameInstaller instance;
    public static InGameInstaller Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public override void InstallBindings()
    {
        Container.Bind<IWrappingManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IExtraLives>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IScoreManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IEntityManager<EntityType>>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ILevelManager>().FromComponentInHierarchy().AsSingle();
    }
    public bool SpawnEntities(Entity prefab, Transform transform, int amount, out List<Entity> spawnedEntities)
    {
        Debug.Assert(prefab != null);
        spawnedEntities = new List<Entity>();
        for(int i = 0; i < amount; i++)
        {
            GameObject newEntity = Container.InstantiatePrefab(prefab, transform);
            if(newEntity.TryGetComponent(out Entity entity))
            {
                spawnedEntities.Add(entity);
            }
            else
            {
                Debug.LogError("Couldn't get Entity class from: " + newEntity.name);
            }
        }
        return spawnedEntities.Count > 0;
    }
}