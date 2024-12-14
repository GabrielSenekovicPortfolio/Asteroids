using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityManager<T> where T : Enum
{
    public void StartNewWave();
    public void DisableEntity(Entity entity);
    public void CacheEntities();
    public bool AddEntity(T entityType, out GameObject result);
    public void ActivateEntity(Entity entity);
    public Entity GetEntityOfType(T entityType);
    public void HideAllEntities();
    public int CountActiveEntitiesOfType(T entityType);
}
