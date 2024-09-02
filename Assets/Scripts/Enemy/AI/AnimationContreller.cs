using UnityEngine;

public abstract class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private float _length;

    [HideInInspector] public int attackClip = Animator.StringToHash("Attack");
    [HideInInspector] public int attackRightClip = Animator.StringToHash("SlowAttackRight");
    [HideInInspector] public int shootingClip = Animator.StringToHash("Idle");
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetSpeed(float value, float maxVelocity) => _animator.SetFloat("Speed", value / maxVelocity);

    public void SetDirection(Vector2 direction)
    {
        _animator.SetFloat("X", direction.x);
        _animator.SetFloat("Y", direction.y);
        
    }

    public void SetLastDirection(Vector2 direction)
    {
        _animator.SetFloat("LastX", direction.x);
        _animator.SetFloat("LastY", direction.y);
    }

    public void SetTrigger(int hash)
    {
        _animator.SetTrigger(hash);
    }

    public float GetAnimationLength(int hash)
    {
        if (_length > 0f) return _length;

        foreach (var clip in _animator.runtimeAnimatorController.animationClips)
        {
            if (Animator.StringToHash(clip.name) == hash)
            {
                _length = clip.length;
                return clip.length;
            }
        }

        return -1f;
    }

    public bool IsAnimationPlaying(int hash)
    {
        
        bool res = _animator.GetCurrentAnimatorStateInfo(0).tagHash == hash;

//        Debug.Log(res);
        return res;
    }

    protected abstract void SetAttackClip();
}
