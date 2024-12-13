using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityManager
{
    public void StartNewWave();
    public void OnDisableEntity();
    public void CacheEntities();
    public void AddEntity(EntityIdentifier entityIdentifier);
    public Entity GetEntityOfType(EntityType entityType);
    public void HideAllEntities();
    public int CountActiveEntitiesOfType(EntityType entityType);
}
