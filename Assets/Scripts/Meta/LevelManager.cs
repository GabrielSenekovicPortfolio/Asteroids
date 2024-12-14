using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using DG.Tweening;
using Zenject;
using System.Threading.Tasks;
[DisallowMultipleComponent]
public class LevelManager : MonoBehaviour, ILevelManager
{
    [Inject] IEntityManager<EntityType> entityManager;

    Level currentLevel;
    int levelIndex;
    public Level CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private void Awake()
    {
        levelIndex = 1;
    }

    private void Start()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendCallback(() =>
        {
            LoadLevelAsync($"Level {levelIndex}").ContinueWith(task =>
            {
                currentLevel = task.Result;
                entityManager.CacheEntities();
            }, TaskScheduler.FromCurrentSynchronizationContext()); 
        });
    }
    public void ClearLevel()
    {
        levelIndex++;
    }

    async Task<Level> LoadLevelAsync(string levelKey)
    {
        AsyncOperationHandle<Level> handle = Addressables.LoadAssetAsync<Level>(levelKey);
        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.LogError($"Failed to load level: {levelKey}");
            return null;
        }
    }
}
