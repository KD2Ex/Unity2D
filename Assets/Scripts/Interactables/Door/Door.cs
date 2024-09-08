using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private KeyItemRI requiredItem;
    [SerializeField] private bool freeEntry;
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerEnter(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TriggerExit(other.gameObject);
    }

    public override void OnInteraction()
    {
        base.OnInteraction();

        OnInteractionEvent?.Invoke();
        
        if (freeEntry)
        {
            gameObject.SetActive(false);
            return;
        }
        
        if (requiredItem.set.Items.Contains(requiredItem))
        {
            requiredItem.set.Remove(requiredItem);
            gameObject.SetActive(false);
        }
        
    }
}
