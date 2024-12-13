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
        Container.Bind<IPlayerFetcher>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IEntityManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ILevelManager>().FromComponentInHierarchy().AsSingle();
    }
    public bool SpawnEntities(Entity prefab, Transform transform, int amount, out List<Entity> spawnedEntities)
    {
        spawnedEntities = new List<Entity>();
        for(int i = 0; i < amount; i++)
        {
            spawnedEntities.Add(Container.InstantiatePrefab(prefab, transform).GetComponent<Entity>());
        }
        return spawnedEntities.Count > 0;
    }
}