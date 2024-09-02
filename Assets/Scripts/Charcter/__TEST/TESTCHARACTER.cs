using UnityEngine;

public class TESTCHARACTER : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _direction;

    [SerializeField] private float speed;
    [SerializeField] private InputReader input;

    [SerializeField] private MoveBehaviour _moveBehaviour;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _moveBehaviour.Initialize(_rb, transform);
    }
    
    private void OnEnable()
    {
        input.MoveEvent += OnMove;

        QualitySettings.vSyncCount = 1;
    }
    private void FixedUpdate()
    {
        _moveBehaviour.Move(_direction);
    }

    private void OnMove(Vector2 direction)
    {
        _direction = direction.normalized;
    }
}
