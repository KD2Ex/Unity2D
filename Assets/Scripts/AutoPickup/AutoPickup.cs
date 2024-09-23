using Interfaces;
using UnityEngine;
using UnityEngine.Events;

public class AutoPickup : MonoBehaviour
{
    [SerializeField] private StatsParticle particle;
    [SerializeField] private float pickupDelay;

    private bool available;
    private Rigidbody2D rb;
    private bool start;
    private Transform target;

    private float elapsedTime;
    
    public UnityEvent Event;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (elapsedTime < pickupDelay)
        {
            elapsedTime += Time.deltaTime;
            return;
        }

        available = true;
        if (!start) return;

        var direction = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * (10f * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        target = other.transform;
        start = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!available) return;
        
        var visitable = other.gameObject.GetComponent<IVisitable>(); //player
        if (visitable == null) return;
        
        visitable.Accept(particle);
        Event.Invoke();
        gameObject.SetActive(false);
    }
    
}
