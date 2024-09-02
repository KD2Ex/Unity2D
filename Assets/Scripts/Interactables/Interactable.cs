using Interfaces;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    protected Animator animator;

    [SerializeField] private PlayerInteractableItem item;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    protected virtual void TriggerEnter(GameObject other) 
    {
        Debug.Log("Enter");
        
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
        Debug.Log(name);
    }
}
