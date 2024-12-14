using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface EntityIdentifier<T> where T : Enum
{
    public T GetEntityType { get;}
    public void Activate();
    public void Deactivate();
    public void Position(Vector3 newPosition);
    public bool IsActive();
    public bool GetDependencies(out List<T> dependencies);
}
