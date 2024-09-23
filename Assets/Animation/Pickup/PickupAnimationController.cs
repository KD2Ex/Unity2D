using System;
using System.Collections;
using MEC;
using UnityEngine;

public class PickupAnimationController : MonoBehaviour
{
    public Vector2 offset;
    private Rigidbody2D rb;

    private Move move;

    private float elapsed = 0f;
    private float time = .2f;

    private Coroutine coroutine;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        move = GetComponent<Move>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Loop());
    }

    private void OnEnable()
    {
        Debug.Log("Translate");
    }

    private void Update()
    {
        
    }

    private IEnumerator Loop()
    {
        while (gameObject.activeInHierarchy)
        {
            move.BySeconds(Vector2.up, 5f, time);
            yield return new WaitUntil(() => !move.IsRunning);
            move.BySeconds(Vector2.down, 5f, time);
            yield return new WaitUntil(() => !move.IsRunning); 
        }
    }
}
