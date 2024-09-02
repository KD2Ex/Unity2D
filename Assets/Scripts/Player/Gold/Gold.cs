using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [field: SerializeField] public int Value { get; private set; }

    public void Add(int value)
    {
        if (value <= 0) return;
        
        Value += value;
    }

    public void Spent(int value)
    {
        if (Value - value < 0) return;

        Value -= value;
    }
}
