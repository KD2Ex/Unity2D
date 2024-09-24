using System;
using UnityEngine;

public class ChangeFocusTrigger : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    [SerializeField] private bool removeFocus;
  
    private void Start()
    {
        if (removeFocus)
        {
            CameraFocusManager.instance.RemoveFocus();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;


        if (removeFocus)
        {
            RemoveFocus();
        }
        else
        {
            Focus();
        }
    }

    private void Focus()
    {
        CameraFocusManager.instance.SetFocus(followTarget);
    }

    private void RemoveFocus()
    {
        CameraFocusManager.instance.RemoveFocus();

    }
}
