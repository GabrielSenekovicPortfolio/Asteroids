using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EntityIdentifier
{
    public EntityType GetEntityType { get;}
    public void Activate();
    public void Deactivate();
    public void Position(Vector3 newPosition);
    public bool IsActive();
}
