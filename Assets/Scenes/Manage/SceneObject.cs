using UnityEngine;

[CreateAssetMenu]
public class SceneObject : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
}
