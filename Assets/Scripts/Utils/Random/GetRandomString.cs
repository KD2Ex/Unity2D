using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GetRandomString : ScriptableObject
{
    [SerializeField] private List<string> items;

    [field: SerializeField] public string Item { get; private set; }

    private void OnEnable()
    {
        Debug.Log("Enable Random string getter");

        var index = Random.Range(0, items.Count);
        Item = items[index];
    }
}
