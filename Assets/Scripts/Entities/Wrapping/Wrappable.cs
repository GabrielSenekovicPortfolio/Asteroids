using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Wrappable : MonoBehaviour, IWrappable
{
    [SerializeField] SpriteRenderer rendererToWrap;
    [Inject]
    public void Init(IWrappingManager wrappingManager)
    {
        InitializeWrappable(wrappingManager);
    }
    public void InitializeWrappable(IWrappingManager wrappingManager)
    {
        wrappingManager.AddObject(transform, rendererToWrap);
    }
}
