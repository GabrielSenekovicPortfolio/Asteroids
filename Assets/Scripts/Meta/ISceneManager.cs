using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneManager
{
    public void LoadStartScene(); //SceneManager keeps track of what is "Home"
    public void LoadScene(string key);
    public void Destroy();
}
