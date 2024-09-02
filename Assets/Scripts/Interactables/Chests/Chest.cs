using UnityEngine;
using UnityEngine.Events;

public class Chest : Interactable
{
    public UnityEvent OnOpen;

    public override void OnInteraction()
    {
        base.OnInteraction();
        OnOpen.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEnter(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other.gameObject);
    }
}
