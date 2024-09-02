using System.Collections;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(CircleCollider2D))]
public class Sensor : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float timerInterval = 1f;

    private CircleCollider2D _detectionRange;
    
    public event UnityAction<Transform> OnTargetChanged = delegate {  };

    public Transform TargetTransform => _target ? _target.transform : null;
    public bool IsTargetInRange => TargetTransform != null;
    
    private GameObject _target;
    private Vector3 _lastKnowPosition;

    private void Awake()
    {
        _detectionRange = GetComponent<CircleCollider2D>();
        _detectionRange.isTrigger = true;
        _detectionRange.radius = detectionRadius;
    }

    private void Start()
    {
        StartCoroutine(Timer(timerInterval));
    }

    private IEnumerator Timer(float seconds)
    {
        while (enabled)
        {
            yield return new WaitForSeconds(seconds);
//            Debug.Log("Update target pos");
            UpdateTargetPosition(_target);
        }
    }

    void UpdateTargetPosition(GameObject target = null)
    {
        _target = target;
        if (IsTargetInRange && (_lastKnowPosition != TargetTransform.position))
        {
            _lastKnowPosition = TargetTransform.position;
            OnTargetChanged?.Invoke(TargetTransform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        UpdateTargetPosition(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        UpdateTargetPosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = IsTargetInRange ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
