using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GetRandomSceneObject : ScriptableObject
{
    [SerializeField] private List<SceneObject> items;

    [field: SerializeField] public SceneObject Item { get; private set; }

    public SceneObject Generate()
    {
        var index = Random.Range(0, items.Count);
        Item = items[index];

        return Item;
    }
}
