using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;

    public GameObject sword_hand;
    public GameObject sword_back;
    public GameObject sword_effect;

    private void Update()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (NowAttack() != -1)
        {
            sword_hand.SetActive(true);
            sword_back.SetActive(false);
        }
        else
        {
            sword_hand.SetActive(false);
            sword_back.SetActive(true);
        }

        if (NowAttack() >= 0)
        {
            if (NowAttack() == 0 && NowAttackTime() > 0.4f)
                sword_effect.SetActive(false);
            else if (NowAttack() == 1 && NowAttackTime() > 0.53f)
                sword_effect.SetActive(false);
            else if (NowAttack() == 2 && NowAttackTime() > 0.4f)
                sword_effect.SetActive(false);
            else if (NowAttack() == 3 && NowAttackTime() > 0.45f)
                sword_effect.SetActive(false);
            else
                sword_effect.SetActive(true);
        }
        else
            sword_effect.SetActive(false);
    }


    public int NowAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack0"))
            return 0;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
            return 1;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
            return 2;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
            return 3;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AttackStop"))
            return -2;
        return -1;
    }

    public float NowAttackTime()
    {
        if(NowAttack() >= 0)
            return animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        return -1;
    }
}
