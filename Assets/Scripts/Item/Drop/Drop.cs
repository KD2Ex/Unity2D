using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class Drop : MonoBehaviour
{
    [SerializeField] private List<DropItem> list;
    [SerializeField] private float speed;
    [SerializeField] private float time;

    private Vector2 direction = new Vector2(3f, 0f);
    
    public void Execute()
    {
        foreach (var dropItem in list)
        {
            var angle = Random.Range(0, 360);
            
            for (int i = 0; i < dropItem.amount; i++)
            {
                var go = Instantiate(dropItem.item, transform.position, Quaternion.identity);
                go.TryGetComponent<Rigidbody2D>(out var rb);

                if (rb)
                {
                    direction = Quaternion.AngleAxis(angle, Vector3.forward) * direction;
                    StartCoroutine(MoveToPoint.Execute(rb, direction, speed, time));
                }
            }
            
        }
    }
    
    
}
