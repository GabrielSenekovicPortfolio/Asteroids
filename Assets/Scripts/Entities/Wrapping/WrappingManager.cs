using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WrappingManager : MonoBehaviour, IWrappingManager
{
    List<IWrappingManager.WrapEntry> wrapEntries = new List<IWrappingManager.WrapEntry>();
    public void Update()
    {
        foreach(IWrappingManager.WrapEntry entry in wrapEntries)
        {
            Vector2 newPosition = entry.originalTransform.position;
            Vector2 viewPortPos = Camera.main.WorldToViewportPoint(entry.originalTransform.position);
            float xOffset = viewPortPos.x < 0 ? 1 : viewPortPos.x > 1 ? -1 : 0;
            float yOffset = viewPortPos.y < 0 ? 1 : viewPortPos.y > 1 ? -1 : 0;

            newPosition.x = (viewPortPos.x + xOffset);
            newPosition.y = (viewPortPos.y + yOffset);

            newPosition = Camera.main.ViewportToWorldPoint(newPosition);

            if (!entry.originalRenderer.isVisible)
            {
                entry.originalTransform.position = new Vector3(newPosition.x, newPosition.y, 0);
            }
            entry.doubleTransform.gameObject.SetActive(entry.originalTransform.gameObject.activeSelf);
            entry.doubleTransform.position = newPosition;
            entry.doubleTransform.rotation = entry.originalTransform.rotation;
        }
    }
    public void AddObject(Transform transform, SpriteRenderer renderer)
    {
        GameObject newDouble = new GameObject("Double");
        newDouble.SetActive(transform.gameObject.activeSelf);
        SpriteRenderer doubleRenderer = newDouble.AddComponent<SpriteRenderer>();
        doubleRenderer.sprite = renderer.sprite;

        IWrappingManager.WrapEntry newEntry = new IWrappingManager.WrapEntry();
        newEntry.originalTransform = transform;
        newEntry.originalRenderer = renderer;
        newEntry.doubleTransform = newDouble.transform;
        wrapEntries.Add(newEntry);
    }

    public void RemoveObjects()
    {
        for(int i = 0; i < wrapEntries.Count; i++)
        {
            Object.Destroy(wrapEntries[i].doubleTransform.gameObject);
        }
        wrapEntries.Clear();
    }
}
