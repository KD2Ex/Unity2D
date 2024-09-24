using Unity.VisualScripting;
using UnityEditorInternal;
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

    private bool hitRay;

    private float Force { get; set; }
    private float DashTime { get; set; }
    
    private Vector2 Position => transform.position;
    public FloatReference Cooldown => stats.cooldown;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        curveTime = stats.curve[stats.curve.length - 1].time;
        
        ResetValues();
    }

    private void Hit()
    {
        /*
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.origin);
        Debug.Log(ray.direction);
        */
        
        RaycastHit[] m_Results = new RaycastHit[5];
        
        var dir2d = (Vector2)transform.position + this.direction * 7f;
        var origin = new Vector3(dir2d.x, dir2d.y, -5f);
        int hits = Physics.RaycastNonAlloc(origin, Vector3.forward, m_Results, 15f);
        
        Debug.Log("Origin: " + origin);

        
        for (int i = 0; i < hits; i++)
        {
            Debug.Log("Hit " + m_Results[i].collider.gameObject.name);
        }
        if (hits == 0)
        {
            Debug.Log("Did not hit");
        }

        hitRay = false;
    }

    public void Execute(Vector2 direction)
    {
        this.direction = Vector3.Normalize(direction);
        hitRay = true;
        activated = true;
        OnDashEvent.Invoke();
    }

    public void Execute(Vector2 direction, float force, float dashTime)
    {
        Force = force;
        DashTime = dashTime;
        
        Execute(direction);
    }

    private void FixedUpdate()
    {
        if (hitRay)
        {
            Hit();
        }
        PerformDash(Force, DashTime, stats.curve);
    }

    private void PerformDash(float force, float dashTime, AnimationCurve curve)
    {
        if (!activated) return;

        var curveValue = curve.Evaluate((curveTime * time) / dashTime);
        var target = Position + (direction * (force * curveValue * Time.deltaTime));
        rb.MovePosition(target);

        time += Time.deltaTime;

        if (time > dashTime)
        {
            activated = false;
            time = 0f;
            OnStopEvent?.Invoke();
            
            ResetValues();
        }
    }

    private void ResetValues()
    {
        Force = stats.force.Value;
        DashTime = stats.dashTime.Value;
    }
}
