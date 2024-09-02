using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupAnimationController : MonoBehaviour
{
    public Vector2 position;

    private Vector3 Origin;
    
    // Start is called before the first frame update
    void Start()
    {
        Origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Origin.x + position.x, Origin.y + position.y);
    }
}
