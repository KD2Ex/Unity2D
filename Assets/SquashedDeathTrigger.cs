using UnityEngine;

public class SquashedDeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        other.TryGetComponent(out Player qwer);
        qwer?.Die();
    }
}
