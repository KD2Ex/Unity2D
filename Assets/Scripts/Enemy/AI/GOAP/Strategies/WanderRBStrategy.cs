using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderRBStrategy : IActionStrategy
{
    private Rigidbody2D _rb;
    private float _radius;
    private float _speed;

    private Vector2 _targetPosition;
    
    // Start is called before the first frame update
    public bool CanPerform => !Complete;
    public bool Complete => Vector2.Distance(_rb.transform.position, _targetPosition) <= 2f;

    public WanderRBStrategy(Rigidbody2D rb, float radius, float speed)
    {
        _rb = rb;
        _radius = radius;
        _speed = speed;
    }
    
    public void Start()
    {
        _targetPosition = (Vector2)_rb.position + Random.insideUnitCircle * _radius;
    }

    public void Update(float deltaTime)
    {
        var direction = (_targetPosition - _rb.position).normalized;
            
        //_rb.MovePosition(_rb.position + direction * (_speed * deltaTime));

        _rb.velocity = direction * _speed;
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
    }
    
    IEnumerator MoveTo()
    {
        while (!Complete)
        {
            
            yield return null;
        }
    }
    
}
