using Interfaces;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    protected Animator animator;
    
    //can make this a counter, if multiple interaction needed
    public bool IsInteractable { get; protected set; }

    [SerializeField] private PlayerInteractableItem item;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        IsInteractable = true;
    }
    
    protected virtual void TriggerEnter(GameObject other) 
    {
        if (!other.CompareTag("Player")) return;

        item.Interactable = this;
    }

    protected virtual void TriggerExit(GameObject other)
    {
        if (!other.CompareTag("Player")) return;

        item.Interactable = null;
    }

    public virtual void OnInteraction()
    {
    }
}
