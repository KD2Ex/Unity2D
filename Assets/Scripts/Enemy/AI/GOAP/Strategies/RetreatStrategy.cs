using System;
using UnityEngine;

public class RetreatStrategy : IActionStrategy
{
    public RetreatStrategy(
        Rigidbody2D rb, 
        Sensor sensor, 
        RaycastDetection detection, 
        float dashTime,
        float cooldown,
        float force,
        Action onStop
        )
    {
        _rb = rb;
        _sensor = sensor;
        _cooldown = cooldown;
        _force = force;
        _detection = detection;
        OnStop = onStop;
        
        _timer = new CountDownTimer(dashTime);
        _timer.OnTimerStart += () => Complete = false;
        _timer.OnTimerStop += () => Complete = true;
    }

    public bool CanPerform => true;
    public bool Complete { get; private set; }

    private Action OnStop;
    private Rigidbody2D _rb;
    private Sensor _sensor;
    private float _cooldown;
    private float _force;
    private CountDownTimer _timer;
    private RaycastDetection _detection;
    
    private Vector2 _direction;
    
    private Transform danger => _sensor.TargetTransform;
    
    public void Start()
    {
        _timer.Start();

        _direction = _detection.GetRetreatDirection();
        
        //_dash.TriggerAbility(_direction, );
        _rb.velocity = _direction.normalized * _force;
        /*var dir = danger.position;
        _rb.AddForce(dir * _force, ForceMode2D.Impulse);*/
    }

    public void Update(float deltaTime)
    {
        _timer.Tick(deltaTime);
        //_rb.MovePosition(_rb.position + _direction * (_force * deltaTime));
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
        OnStop();
    }
}