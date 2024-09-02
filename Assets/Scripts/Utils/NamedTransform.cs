using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Transform point", menuName = "SO/Transform")]
public class NamedTransform : ScriptableObject
{
    
    
    [SerializeField] private GameObject point;

    public Transform Point => point.transform;
    [field: SerializeField] public string Name { get; private set; }
}
