using UnityEngine;

public class VelocityMovement : Movement
{
    private Vector2 direction;
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    protected override void Move()
    {
        _rb.velocity += direction * (5f * Time.deltaTime);
    }

    public override void OnMove(Vector2 direction)
    {
        Debug.Log(direction);
        this.direction = direction;
    }
}
