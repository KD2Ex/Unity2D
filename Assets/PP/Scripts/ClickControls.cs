using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class ClickControls : MonoBehaviour
{
    private Camera cameraMain => Camera.main;
    
    private Vector3 mousePos;
    private Vector2 offset;

    private bool clicked;
    private bool pressed;

    private bool hited;

    private GameObject attached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pressed = Input.GetMouseButton(0);
        clicked = Input.GetMouseButtonDown(0);
        mousePos = cameraMain.ScreenToWorldPoint(Input.mousePosition);

        if (!pressed) hited = false;
        if (!pressed && attached) attached = null;

    }

    void FixedUpdate()
    {
        if (pressed && !hited)
        {
            hited = true;
            
            Ray ray = cameraMain.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                Debug.DrawLine(ray.origin, hit.point);
                attached = hit.collider.gameObject;
                offset = mousePos - attached.transform.position;
            }
        }
        
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log(hit.collider.gameObject.name);
        }*/
    }

    private void LateUpdate()
    {
        if (!attached) return;
        mousePos.z = 0f;
        attached.transform.position = (Vector2)mousePos - offset;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(mousePos, Vector3.forward * 100f);
    }
}
