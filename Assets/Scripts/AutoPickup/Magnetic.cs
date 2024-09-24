using UnityEngine;

public class Magnetic : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D rb;

    
    private Transform target;
    private bool start; 

    private void FixedUpdate()
    {
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
}
