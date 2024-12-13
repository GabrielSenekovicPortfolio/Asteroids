using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, EntityIdentifier
{
    [SerializeField] EntityType entityType;
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Position(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    public bool IsActive() => gameObject.activeInHierarchy;

    public EntityType GetEntityType { get => entityType; }
}
