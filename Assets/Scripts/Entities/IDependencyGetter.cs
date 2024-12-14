using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependencyGetter<T>
{
    List<T> GetDependencies();
}
