using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneLocation
{
    public string name;
    public Vector3 location;
    public Transform point;
}

[CreateAssetMenu]
public class ScenesLocation : ScriptableObject
{
    public List<SceneLocation> Items;
}
