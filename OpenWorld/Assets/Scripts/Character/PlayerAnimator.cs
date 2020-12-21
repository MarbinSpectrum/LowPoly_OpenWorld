using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;

    private void Update()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    public int NowAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack0"))
            return 0;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            return 1;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            return 2;
        return -1;
    }

    public float NowAttackTime()
    {
        if(NowAttack() >= 0)
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return -1;
    }
}
