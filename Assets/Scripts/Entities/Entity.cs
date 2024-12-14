using log4net.Util;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Entity : MonoBehaviour, EntityIdentifier<EntityType>
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

    public bool GetDependencies(out List<EntityType> dependencies)
    {
        dependencies = new List<EntityType>();
        var components = GetComponents<IDependencyGetter<EntityType>>();
        if(components.Count() > 0)
        {
            for(int i = 0; i < components.Count(); i++)
            {
                dependencies.AddRange(components[i].GetDependencies());
            }
        }
        dependencies = dependencies.Distinct().ToList();
        return dependencies != null && dependencies.Count > 0;
    }

    public EntityType GetEntityType { get => entityType; }
}
