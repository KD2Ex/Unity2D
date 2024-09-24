using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneLocation
{
    public SceneObject scene;
    public Transform point;
}

[CreateAssetMenu]
public class ScenesLocation : ScriptableObject
{
    public List<SceneLocation> Items;
}
