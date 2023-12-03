using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IWrappingManager
{
    public struct WrapEntry
    {
        public Transform originalTransform;
        public SpriteRenderer originalRenderer;
        public Transform doubleTransform;
    }
    public void AddObject(Transform transform, SpriteRenderer renderer);
    public void RemoveObjects();
}
