using UnityEngine;

public class RangedStrategy : IActionStrategy
{
    public RangedStrategy(DirectionalWeapon weapon, Sensor attackSensor, Rigidbody2D rb)
    {
        _weapon = weapon;
        _attackSensor = attackSensor;
        _rb = rb;
        
        _timer = new CountDownTimer(.1f);
        _timer.OnTimerStart += () => Complete = false;
        _timer.OnTimerStop += () => Complete = true;
    }
    
    public bool CanPerform => true;
    public bool Complete { get; private set; }

    private CountDownTimer _timer;
    private DirectionalWeapon _weapon;
    private Sensor _attackSensor;
    private Rigidbody2D _rb;

    public void Start()
    {
        _timer.Start();
        _weapon.gameObject.SetActive(true);

        var dir = ((Vector2) _attackSensor.TargetTransform.position - _rb.position).normalized;
        
        _weapon.Attack(dir);
    }
    
    public void Update(float deltaTime)
    {
        _timer.Tick(deltaTime);
    }

    public void Stop()
    {
        Debug.Log("stop");
    }
}