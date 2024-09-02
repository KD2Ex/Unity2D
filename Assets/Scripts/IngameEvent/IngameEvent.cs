using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameEvent : MonoBehaviour
{
    public bool InProcess { get; protected set; }
    public bool Finished { get; protected set; }

    public virtual IEnumerator Event()
    {
        yield return null;
    }

    public virtual void StartEvent()
    {
    }
}
