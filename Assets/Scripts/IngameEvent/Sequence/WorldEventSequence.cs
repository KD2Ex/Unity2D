using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WorldEventSequence : MonoBehaviour
{
    [SerializeField] private List<WorldEvent> events;

    public UnityEvent OnSequenceEnd;
    
    public void StartSequence()
    {
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        foreach (var worldEvent in events)
        {
            Debug.Log(worldEvent.name);
            worldEvent.Initialize(transform);
            
            StartCoroutine(worldEvent.Event());
            yield return new WaitUntil(() => worldEvent.Finished);
        }
        
        OnSequenceEnd.Invoke();
    }
}
