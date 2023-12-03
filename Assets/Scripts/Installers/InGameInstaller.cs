using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InGameInstaller : MonoInstaller<InGameInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IWrappingManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IExtraLives>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IScoreManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IPlayerFetcher>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IEntityManager>().FromComponentInHierarchy().AsSingle();
    }
}