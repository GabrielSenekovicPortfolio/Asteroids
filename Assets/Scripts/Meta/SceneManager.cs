using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManager : ISceneManager
{
    const string keyForStartScene = "Start";
    //private AsyncOperationHandle<SceneInstance> loadHandle;

    public void Destroy()
    {
        //Addressables.UnloadSceneAsync(loadHandle);
    }

    public void LoadStartScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void LoadScene(string key)
    {
        //loadHandle = Addressables.LoadSceneAsync(key, LoadSceneMode.Single);
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(key, LoadSceneMode.Single);
    }
}
