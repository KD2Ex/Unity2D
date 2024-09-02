using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class InteractableRadius : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;

    private IInteractableObject interactableObject;
    
    private void OnEnable()
    {
        inputReader.InteractEvent += OnInteract;
    }

    private void OnDisable()
    {
        inputReader.InteractEvent -= OnInteract;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.TryGetComponent(out interactableObject);

        if (interactableObject == null) return;
        
        interactableObject.OnApproaching();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (interactableObject == null) return;
        if (other.gameObject.name != interactableObject.Name) return;
        interactableObject.OnLeave();

        interactableObject = null;
    }

    private void OnInteract()
    {
        interactableObject.OnInteraction();
    }
}
