using UnityEngine;
using Zenject;

public class HeadInstaller : MonoInstaller<HeadInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ISceneManager>().To<SceneManager>().AsSingle();
        Container.Bind<IGameExiter>().To<GameExiter>().AsSingle();
    }
}