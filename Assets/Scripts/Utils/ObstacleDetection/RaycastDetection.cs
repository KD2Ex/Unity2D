using System.Collections.Generic;
using UnityEngine;

public class RaycastDetection : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private float angle;
    [SerializeField] private float dashDistance;
    [SerializeField] private LayerMask mask;

    [SerializeField] private Transform target;
    
    private RaycastHit2D _preferablePath;
    private List<RaycastHit2D> _hits = new();

    private Dictionary<RaycastHit2D, float> _hitsDict = new();

    private Vector3 draw = Vector3.zero;
    public float Angle => angle;

    
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    public Vector2 GetRetreatDirection()
    {
        _hits.Clear();
        _hitsDict.Clear();
        _preferablePath = new RaycastHit2D();

        var result = Vector2.zero;

        var back = (Vector2)(transform.position - target.position).normalized;
        var origin = (Vector2) transform.position;
        
        for (int i = 0; i <= 90; i += 10)
        {
            result = Cast(i, back, origin);
            if (result != Vector2.zero)
            {
                break;
            }  
            
            result = Cast(i * -1, back, origin);
            if (result != Vector2.zero)
            {
                break;
            }
            
            /*var direction = Quaternion.AngleAxis(i, Vector3.back) * back.normalized;
            var hit = Physics2D.Raycast(transform.position, direction, distance, mask);
            /*if (hit)
            {
                _hits.Add(hit);
                _hitsDict.Add(hit, Vector2.Distance(hit.point, target.position));
            }#1#
            if (!hit)
            {
                Debug.DrawRay(transform.position, direction);
                draw = direction;
                break;
            }

            if (i == 0) continue;

            direction = Quaternion.AngleAxis(i * -1, Vector3.back) * back.normalized;
            hit = Physics2D.Raycast(transform.position, direction, distance, mask);

            if (!hit)
            {
                Debug.DrawRay(transform.position, direction);
                draw = direction;
                break;
            }*/

        }

        return result;

    }
    
    public Vector2 Cast(float angle, Vector2 back, Vector2 origin)
    {
        var direction = Quaternion.AngleAxis(angle, Vector3.back) * back.normalized;
        var hit = Physics2D.Raycast(origin, direction, distance, mask);
        
        if (!hit)
        {
            Debug.DrawRay(transform.position, direction);
            draw = direction;
            return direction;
        }
        
        _hits.Add(hit);
        return Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        foreach (var hit in _hits)
        {
            Debug.DrawLine(transform.position, hit.point);
        }

        if (_preferablePath)
        {
            Debug.DrawLine(transform.position, _preferablePath.point, Color.magenta);
        }

        if (draw != Vector3.zero)
        {
            var color = new Color(1, .5f, 0);
            Debug.DrawRay(transform.position, draw, color);
        }
    }
}
