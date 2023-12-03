using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class StartMenu : MonoBehaviour
{
    [Inject] ISceneManager sceneManager;
    [Inject] IGameExiter gameExiter;

    [SerializeField] string gameKey;
    public void StartGame()
    {
        sceneManager.LoadScene(gameKey);
    }

    public void QuitGame()
    {
        gameExiter.ExitGame();
    }
}
