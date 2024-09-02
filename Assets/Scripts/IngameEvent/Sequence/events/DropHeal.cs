using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHeal : WorldEvent
{
    [SerializeField] private GameObject healItem;

    public override IEnumerator Event()
    {
        yield break;
    }
}
