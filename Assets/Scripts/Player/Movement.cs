using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    protected Vector2 _movementDirection;
    private Vector2 _lastMoveDirection;
    protected Rigidbody2D _rb;
    private Animator _animator;

    #region Animations Hashes

    private int h_IdleTop;
    private int h_IdleBottom;
    private int h_IdleRight;
    private int h_IdleLeft;

    private int h_MoveTop;
    private int h_MoveBottom;
    private int h_MoveRight;
    private int h_MoveLeft;

    private Dictionary<int, int> moveXAnimations;
    private Dictionary<int, int> moveYAnimations;

    #endregion
    
    [SerializeField] private float speed;

    public Vector2 LastMovementDirection => _lastMoveDirection;

    private Dictionary<string, float> speedEffects = new();

    private bool _blockMovement;
    
    public bool BlockMovement
    {
        get => _blockMovement;
        set
        {
            if (value) _rb.velocity = Vector2.zero;
            _blockMovement = value;
        }
    }
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        

        h_IdleTop = Animator.StringToHash("IdleTop");
        h_IdleBottom = Animator.StringToHash("IdleBottom");
        h_IdleRight = Animator.StringToHash("IdleRight");
        h_IdleLeft = Animator.StringToHash("IdleLeft");
        
        h_MoveTop = Animator.StringToHash("MoveTop");
        h_MoveBottom = Animator.StringToHash("MoveBottom");
        h_MoveRight = Animator.StringToHash("MoveRight");
        h_MoveLeft = Animator.StringToHash("MoveLeft");

        moveXAnimations = new Dictionary<int, int>()
        {
            {1, h_IdleRight},
            {-1, h_IdleLeft},
        };
        moveYAnimations = new Dictionary<int, int>()
        {
            {1, h_IdleTop},
            {-1, h_IdleBottom},
        };
        
        /*
        moveAnimations.Add(new []{1, 0}, h_IdleRight);
        moveAnimations.Add(new []{-1, 0}, h_IdleLeft);
        moveAnimations.Add(new []{0, 1}, h_IdleTop);
        moveAnimations.Add(new []{0, -1}, h_IdleBottom);*/
        
    }


    private void FixedUpdate()
    {
        
    }

    private void Update()
    {
        if (BlockMovement) return;
        
        Move();
    }

    protected virtual void Move()
    {
        var pos = (Vector2)transform.position + _movementDirection.normalized * (speed * Time.deltaTime);
        
        var velocity = speedEffects.Aggregate(1f, (current, effect) => current * effect.Value);

        _rb.velocity = _movementDirection.normalized * (velocity * speed);
        //_rb.MovePosition(pos);

        if (_movementDirection.sqrMagnitude > .1f)
        {
            _lastMoveDirection = _movementDirection;
        }
        
        _animator.SetFloat("X", _movementDirection.x);
        _animator.SetFloat("Y", _movementDirection.y);
        _animator.SetFloat("LastX", _lastMoveDirection.x);
        _animator.SetFloat("LastY", _lastMoveDirection.y);
    }
    
    public virtual void OnMove(Vector2 direction)
    {
        _movementDirection = direction;
    }

    public void ApplyEffect(string key, float value)
    {
        speedEffects.Add(key, value);
    }

    public void RemoveEffect(string key)
    {
        speedEffects.Remove(key);
    } 
}
