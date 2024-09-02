using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovePosition", menuName = "SO/Test/MovePosition")]
public class MovePositionMovement : MoveBehaviour
{
    public override void Initialize(Rigidbody2D rb, Transform character)
    {
        base.Initialize(rb, character);
    }

    public override void Move(Vector2 direction)
    {
        var dir = (Vector2)_character.position + direction * (speed * Time.deltaTime);
        _rb.MovePosition(dir);
    }
}
