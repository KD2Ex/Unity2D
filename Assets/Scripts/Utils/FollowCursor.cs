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
    [SerializeField] private float minBoundRadius = 3f;

    
    private void LateUpdate()
    {
        if (GameController.connected) return;
        
        var mousePos = Input.mousePosition;
        var pos =  camera.ScreenToWorldPoint(mousePos);
        pos.z = 0f;

        var distance = pos - ParentPos;
        
        var angle = Mathf.Atan2(distance.y, distance.x);
        var x = xBound * Mathf.Cos(angle);
        var y = yBound * Mathf.Sin(angle);
        
        var xClamp = 0f;
        var yClamp = 0f;
        
        var minX = ParentPos.x;
        var maxX = ParentPos.x;

        if (distance.magnitude < 3f)
        {
            var point = MathUtils.GetPointOnCircle(ParentPos, minBoundRadius, angle * Mathf.Rad2Deg);
            xClamp = point.x;
            yClamp = point.y;
        }
        else
        {
            if (Mathf.Sign(x) > 0.99f)
            {
            
                xClamp = Mathf.Clamp(pos.x, minX, ParentPos.x + x);
            }
            else
            {
                xClamp = Mathf.Clamp(pos.x, ParentPos.x + x, maxX);
            }

            if (Mathf.Sign(y) > 0.99f)
            {
                yClamp = Mathf.Clamp(pos.y, ParentPos.y - y, ParentPos.y + y);
            }
            else
            {
                yClamp = Mathf.Clamp(pos.y, ParentPos.y + y, ParentPos.y - y);
            }
        }
        
        pos = new Vector3(xClamp, yClamp, 0f);
        transform.position = pos;
    }

}
