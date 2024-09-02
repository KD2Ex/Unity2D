using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        var hit = Physics2D.Raycast(transform.position, transform.right, maxDistance, layerMask);
        if (hit)
        {
            DrawRay(transform.position, hit.point);
        }
        else
        {
            DrawRay(transform.position, transform.right * maxDistance);
        }
    }

    private void DrawRay(Vector2 start, Vector2 end)
    {
        _line.SetPosition(0, start);
        _line.SetPosition(1, end);
    }
}
