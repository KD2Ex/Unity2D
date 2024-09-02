using UnityEngine;

[CreateAssetMenu(fileName = "Velocity", menuName = "SO/Test/Velocity")]
public class VelocityMovementBehaviour : MoveBehaviour
{
    public override void Initialize(Rigidbody2D rb, Transform character)
    {
        base.Initialize(rb, character);
    }
    public override void Move(Vector2 direction)
    {
        _rb.velocity = direction * speed;
    }
}
