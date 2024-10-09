using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Dash : MonoBehaviour
{
    [SerializeField] private DashStats stats;

    [SerializeField] private Collider2D collider;
    //[SerializeField] private ScriptableEvent OnStart;

    [Header("game event")] [SerializeField]
    public UnityEvent OnDashEvent;
    public UnityEvent OnStopEvent;
    
    private Rigidbody2D rb;
    private Vector2 direction;
    private bool activated;
    private float time;
    private float dashedDistance = 0f;
    
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

    private RaycastHit[] Hit(Vector2 originRayPoint, out int hitsAmount)
    {
        /*
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.Log(ray.origin);
        Debug.Log(ray.direction);
        */

        RaycastHit[] m_Results = new RaycastHit[5];
        
        var origin = new Vector3(originRayPoint.x, originRayPoint.y, -5f);
        //hitsAmount = Physics.SphereCastNonAlloc(origin, .25f, Vector3.forward, m_Results, 15f);
        hitsAmount = Physics.RaycastNonAlloc(origin, Vector3.forward, m_Results, 15f);
        
        Debug.Log("Origin: " + origin);
        return m_Results;
    }

    private void ActivateFlags()
    {
        hitRay = true;
        activated = true;
        OnDashEvent.Invoke();
    }
    
    public void Execute(Vector2 direction)
    {
        this.direction = Vector3.Normalize(direction);

        Force = stats.force.Value;
        ActivateFlags();
    }

    public void Execute(Vector2 direction, float force, float dashTime)
    {
        this.direction = Vector3.Normalize(direction);
        Force = force;
        DashTime = dashTime;
        
        
        ActivateFlags();
    }

    private float RecalculateDashForce()
    {
        for (int i = 0; i < Mathf.FloorToInt(Force * DashTime); i++)
        {
            var point = (Vector2)collider.bounds.center + direction * (Force * DashTime - i);
            var res = Hit(point, out int hitsAmount);

            var isNotGround = false;
            
            for (int j = 0; j < hitsAmount; j++)
            {
                var hitObject = res[j].collider;
                Debug.Log(i);
                Debug.Log(hitObject.tag);

                if (!hitObject.CompareTag("Ground"))
                {
                    isNotGround = true;
                }
            }
            
            if (isNotGround) continue;

            Vector2 hitPoint = res[0].point;

            if (i != 0)
            {
                var x = Mathf.Round(hitPoint.x) - .1f;
                var y = Mathf.Round(hitPoint.y) - 1f;

                Debug.Log(new Vector2(x, y));
        
                hitPoint = new Vector2(
                    Mathf.Floor(x) + .5f,
                    Mathf.Floor(y) + .5f
                );
            }
            
            var newDestination
                = hitPoint - (Vector2)collider.bounds.center;
            var newForce = newDestination.magnitude / DashTime;
            var distance = (newDestination).magnitude;
            Debug.Log("new destination: " + newDestination);
            Debug.Log("distance: " + distance);
            return distance + .1f;
        }

        return 0f; // we need to disable layer change then'
    }

    private float Distance;
    
    private void FixedUpdate()
    {
        
        if (hitRay)
        {
            Debug.Log("FixedUpdate");
            Distance = RecalculateDashForce();
            Debug.Log(Force);
            hitRay = false;
        }
        PerformDash(Force, DashTime, stats.curve, Distance);
    }

    private void PerformDash(float force, float dashTime, AnimationCurve curve, float distance)
    {
        if (!activated) return;
        
        var curveValue = curve.Evaluate((curveTime * time) / dashTime);
        var target = Position + (direction * (force * curveValue * Time.deltaTime));

        var distanceDelta = (target - Position).magnitude;
        
        if (dashedDistance + distanceDelta > distance)
        {
            var mag = (distance - dashedDistance);
            
            rb.MovePosition(Position + direction * mag);
            dashedDistance += mag + .1f;
        }
        else
        {
            dashedDistance += distanceDelta;
            rb.MovePosition(target);
        }
        
        if (dashedDistance > distance)
        {
            activated = false;
            dashedDistance = 0f;
            OnStopEvent?.Invoke();
            
            ResetValues();
        }

        return;
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
