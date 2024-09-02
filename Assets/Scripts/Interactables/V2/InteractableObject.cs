using System;
using Interfaces;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractableObject
{
    [SerializeField] private GameObject UITip;

    public string Name { get; set; }

    private void Awake()
    {
        Name = gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnInteraction() => Debug.Log(gameObject.GetInstanceID());

    public void OnApproaching()
    {
        UITip.gameObject.SetActive(true);
    }

    public void OnLeave()
    {
        UITip.gameObject.SetActive(false);
    }
}
