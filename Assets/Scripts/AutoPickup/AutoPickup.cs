using Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AutoPickup : MonoBehaviour
{
    [SerializeField] private StatsParticle particle;
    [SerializeField] private float pickupDelay;
    [SerializeField] private Rigidbody2D rb;

    private float elapsedTime;
    
    private Transform target;
    private bool start; 
    
    [FormerlySerializedAs("Event")] public UnityEvent OnPickup;

    private void FixedUpdate()
    {
        if (elapsedTime < pickupDelay)
        {
            elapsedTime += Time.deltaTime;
            return;
        }
        if (!start) return;
        
        var direction = (target.position - transform.position).normalized;
        rb.MovePosition(transform.position + direction * (10f * Time.deltaTime));

        if ((target.position - transform.position).magnitude < .5f)
        {
            Consume();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        target = other.transform;
        start = true;
        
        /*var visitable = other.gameObject.GetComponent<IVisitable>(); //player
        if (visitable == null) return;
        
        visitable.Accept(particle);
        OnPickup.Invoke();
        gameObject.SetActive(false);*/
    }

    private void Consume()
    {
        var visitable = target.gameObject.GetComponent<IVisitable>(); //player
        if (visitable == null) return;
        
        visitable.Accept(particle);
        OnPickup.Invoke();
        gameObject.SetActive(false);
    }
    
}
