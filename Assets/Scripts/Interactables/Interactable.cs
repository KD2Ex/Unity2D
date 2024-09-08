using System;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour, IInteractable
{
    protected Animator animator;

    [SerializeField] protected GameObject UITip;
    //can make this a counter, if multiple interaction needed
    public bool IsInteractable { get; protected set; }
    public UnityEvent OnInteractionEvent;
    [SerializeField] private PlayerInteractableItem item;
    
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        IsInteractable = true;
    }

    private void OnEnable()
    {
        UITip.SetActive(false);
    }

    protected virtual void TriggerEnter(GameObject other) 
    {
        if (!other.CompareTag("Player")) return;

        if (!IsInteractable) return;
        
        UITip.gameObject.SetActive(true);
        item.Interactable = this;
    }

    protected virtual void TriggerExit(GameObject other)
    {
        if (!other.CompareTag("Player")) return;

        UITip.gameObject.SetActive(false);
        
        item.Interactable = null;
    }

    public virtual void OnInteraction()
    {
    }
}
