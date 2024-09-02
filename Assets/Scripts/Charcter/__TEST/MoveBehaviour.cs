using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MoveBehaviour : ScriptableObject
{
    protected Rigidbody2D _rb;
    protected Transform _character;

    [SerializeField] protected float speed;

    public virtual void Initialize(Rigidbody2D rb, Transform character)
    {
        _rb = rb;
        _character = character;
    }
    public abstract void Move(Vector2 direction);
}
