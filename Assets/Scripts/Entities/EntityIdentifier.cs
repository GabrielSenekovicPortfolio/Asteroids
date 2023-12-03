using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityIdentifier : MonoBehaviour
{
    [SerializeField] EntityType entityType;

    public EntityType GetEntityType
    {
        get
        {
            return entityType;
        }
    }
}
