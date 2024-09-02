using UnityEngine;

[CreateAssetMenu(fileName = "AddForce", menuName = "SO/Test/AddForce")]
public class AddForceBehaviour : MoveBehaviour
{
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;

    private Vector2 lastDir = Vector2.zero;
    
    public override void Move(Vector2 direction)
    {
        if (lastDir != direction)
        {
            _rb.velocity = Vector2.zero;
        }
        
        if (direction != Vector2.zero)
        {
            _rb.AddForce(direction * (acceleration), ForceMode2D.Force);

            if (_rb.velocity.magnitude > speed)
            {
                _rb.velocity = _rb.velocity.normalized * speed;
            }
        }
        else
        {
            _rb.AddForce(_rb.velocity * (-deceleration), ForceMode2D.Force);
        }

        lastDir = direction;
    }
}
