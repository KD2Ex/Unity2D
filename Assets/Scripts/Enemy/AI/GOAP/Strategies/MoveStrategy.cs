using System;
using UnityEngine;

public class MoveStrategy : IActionStrategy
{
    private Rigidbody2D _rb;
    private Func<Vector2> _destination;
    private float _speed;

    private Action OnStop;
    
    public bool CanPerform => !Complete;
    public bool Complete => Vector2.Distance(_rb.transform.position, _destination()) <= .2f;

    public MoveStrategy(Rigidbody2D rb, Func<Vector2> destination, float speed, Action onStop = null)
    {
        _rb = rb;
        _destination = destination;
        _speed = speed;
        OnStop = onStop;
    }
    
    public void Start()
    {
        OnStop();
    }

    public void Update(float deltaTime)
    {
        var direction = (_destination()).normalized;
        _rb.velocity = direction * _speed;
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
    }
}
