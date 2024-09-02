using UnityEngine;
using UnityEngine.Events;

public class Dash : MonoBehaviour
{
    [SerializeField] private DashStats stats;
    //[SerializeField] private ScriptableEvent OnStart;

    [Header("game event")] [SerializeField]
    public UnityEvent OnDashEvent;
    public UnityEvent OnStopEvent;
    
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool activated;
    private float time;

    private float curveTime;
    
    private Vector2 Position => transform.position;

    public FloatReference Cooldown => stats.cooldown;
    public FloatReference DashTime => stats.dashTime;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curveTime = stats.curve[stats.curve.length - 1].time;
    }

    public void Execute(Vector2 direction)
    {
        this.direction = Vector3.Normalize(direction);
        activated = true;
        OnDashEvent.Invoke();
    }

    private void FixedUpdate()
    {
        PerformDash();
    }

    private void PerformDash()
    {
        if (!activated) return;

        var curveValue = stats.curve.Evaluate((curveTime * time) / stats.dashTime.Value);
        var target = Position + (direction * (stats.force.Value * curveValue * Time.deltaTime));
        rb.MovePosition(target);

        time += Time.deltaTime;

        if (time > stats.dashTime.Value)
        {
            activated = false;
            time = 0f;
            OnStopEvent?.Invoke();
        }
    }
}
