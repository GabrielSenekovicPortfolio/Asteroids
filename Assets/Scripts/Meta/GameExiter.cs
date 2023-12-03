using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameExiter : IGameExiter
{
    [Inject] ISceneManager sceneManager;
    public void ExitGame()
    {
        sceneManager.Destroy();
        Application.Quit();
    }
}
