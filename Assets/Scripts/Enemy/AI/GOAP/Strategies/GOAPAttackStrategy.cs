using TMPro;
using UnityEngine;

public class GOAPAttackStrategy : IActionStrategy
{
    public bool CanPerform => true;
    public bool Complete { get; private set; }

    private CountDownTimer _timer;
    private AnimationController _animation;
    private DirectionalWeapon _weapon;
    private Sensor _attackSensor;
    private Rigidbody2D _rb;
    private int animationClip;

    public GOAPAttackStrategy(AnimationController animation, int clip, DirectionalWeapon weapon, Sensor attackSensor, Rigidbody2D rb)
    {
        animationClip = clip;
        _rb = rb;
        _animation = animation;
        _weapon = weapon;
        _attackSensor = attackSensor;

        var time = _animation.GetAnimationLength(_animation.attackRightClip);
        Debug.Log(time);
        _timer = new CountDownTimer(time);
        _timer.OnTimerStart += () => Complete = false;
        _timer.OnTimerStop += () =>
        {
            Complete = true;
        };
    }
    
    public void Start()
    {
        _timer.Start();
        
        _weapon.gameObject.SetActive(true);
        _weapon.Attack(((Vector2)_attackSensor.TargetTransform.position - _rb.position).normalized);
    }

    public void Update(float deltaTime)
    {
        _timer.Tick(deltaTime);
    }

}