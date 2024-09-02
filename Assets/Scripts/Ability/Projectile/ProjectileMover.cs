using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 velocity)
    {
        rb.AddForce(velocity, ForceMode2D.Impulse);
    }
}
