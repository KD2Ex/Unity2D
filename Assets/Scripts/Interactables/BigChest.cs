using System.Collections;
using UnityEngine;

public class BigChest : Interactable
{
    [SerializeField] private int gold;
    private IngameEvent ingameEvent;

    private void OnEnable()
    {
        ingameEvent = GetComponent<IngameEvent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEnter(other.gameObject);
        /*Debug.Log(other.gameObject.name);
        transform.SetParent(other.gameObject.transform);*/
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other.gameObject);
    }
    
    public override void OnInteraction()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open")) return;
        if (ingameEvent)
        {
            if (ingameEvent.InProcess) return;
            StartCoroutine(WaitForEvent());
            return;
        }
        
        animator.SetTrigger("Open");
        // gold += x; ?
    }

    private IEnumerator WaitForEvent()
    {
        StartCoroutine(ingameEvent.Event());
        while (!ingameEvent.Finished)
        {
            yield return null;
        }
        ingameEvent = null;
    }
    
    protected override void TriggerEnter(GameObject other)
    {
        base.TriggerEnter(other);
    }
    
    protected override void TriggerExit(GameObject other)
    {
        base.TriggerExit(other);
    }
    
}
