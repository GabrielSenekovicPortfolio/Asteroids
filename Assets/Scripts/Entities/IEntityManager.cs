using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityManager
{
    public void StartNewWave();
    public void OnDisableEntity();
    //Make sure AddEntity is called before OnDisableEntity();
    public void AddEntity(EntityIdentifier entityIdentifier);
    public EntityIdentifier GetEntityOfType(EntityType entityType);
    public void HideAllEntities();
    public int CountActiveEntitiesOfType(EntityType entityType);
}
