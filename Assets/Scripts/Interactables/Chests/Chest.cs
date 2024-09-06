using UnityEngine;
using UnityEngine.Events;

public class Chest : Interactable
{
    public UnityEvent OnInteractionEvent;

    protected override void Awake()
    {
        base.Awake();
        IsInteractable = true;
    }

    public override void OnInteraction()
    {
        base.OnInteraction();
        if (!IsInteractable) return;
        
        OnInteractionEvent.Invoke();
        IsInteractable = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEnter(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other.gameObject);
    }

    public void Open()
    {
        animator.SetTrigger("Open");
    }
}
