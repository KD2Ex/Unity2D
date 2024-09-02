using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyAnimationController : AnimationController
{
    protected override void SetAttackClip()
    {
        attackClip = Animator.StringToHash("Attack");
    }
}
