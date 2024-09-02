using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    [SerializeField] private GameObject parent;
    private Vector3 ParentPos => parent.transform.position;
    private Camera camera => Camera.main;

    [SerializeField] private float xBound = 17f;
    [SerializeField] private float yBound = 10f;

    private void LateUpdate()
    {
        var mousePos = Input.mousePosition;
        var pos =  camera.ScreenToWorldPoint(mousePos);
        pos.z = 0f;

        //if (Mathf.Abs(distance.x) > 17f || Mathf.Abs(distance.y) > 10f) return;
        //Debug.Log(mousePos.sqrMagnitude);

        /*var xClamp = Mathf.Clamp(pos.x, ParentPos.x - 17f, ParentPos.x + 17f);
        var yClamp = Mathf.Clamp(pos.y, ParentPos.y - 10f, ParentPos.y + 10f);
        
        pos = new Vector3(xClamp, yClamp, 0f);*/
        var distance = pos - parent.transform.position;
        

        var angle = Mathf.Atan2(distance.y, distance.x);
        var x = xBound * Mathf.Cos(angle);
        var y = yBound * Mathf.Sin(angle);

        var xClamp = 0f;
        var yClamp = 0f;
        
        if (Mathf.Sign(x) > 0.99f)
        {
            xClamp = Mathf.Clamp(pos.x, ParentPos.x - x, ParentPos.x + x);
        }
        else
        {
            xClamp = Mathf.Clamp(pos.x, ParentPos.x + x, ParentPos.x - x);
        }

        if (Mathf.Sign(y) > 0.99f)
        {
            yClamp = Mathf.Clamp(pos.y, ParentPos.y - y, ParentPos.y + y);
        }
        else
        {
            yClamp = Mathf.Clamp(pos.y, ParentPos.y + y, ParentPos.y - y);
        }
        
        pos = new Vector3(xClamp, yClamp, 0f);
        
        transform.position = pos;
    }
}
