using UnityEngine;

public class SlowEffector : MonoBehaviour
{
    [Range(0, 1)] [SerializeField] private float speed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent<Movement>(out var movement);
        movement?.ApplyEffect(gameObject.name, speed);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.TryGetComponent<Movement>(out var movement);
        movement?.RemoveEffect(gameObject.name);
    }
}
