using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutObject : MonoBehaviour
{
    [SerializeField] private Transform targetObject;
    [SerializeField] private LayerMask wallMask;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
    }
    
    void Update()
    {
        var cutoutPos = mainCamera.WorldToViewportPoint(targetObject.position);
        cutoutPos.y = (Screen.width / Screen.height);

        var offset = targetObject.position - transform.position;
        var hitObjects = Physics.RaycastAll(transform.position, offset, offset.magnitude, wallMask);
        
        for (int i = 0; i < hitObjects.Length; ++i)
        {
            var materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            for (int m = 0; i < materials.Length; ++m)
            {
                materials[m].SetVector("_CutoutPos", cutoutPos);
                materials[m].SetFloat("_CutoutSize", .1f);
                materials[m].SetFloat("_FalloffSize", .05f);
            }
            
        }
    }
}
