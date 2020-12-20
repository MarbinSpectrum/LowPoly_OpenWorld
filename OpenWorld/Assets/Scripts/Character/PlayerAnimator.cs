using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement3D))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private Movement3D movement3D;

    private void Update()
    {
        if (movement3D == null)
            movement3D = GetComponent<Movement3D>();
        if (animator == null)
            animator = GetComponent<Animator>();

        animator.SetBool("Run", movement3D.nowMove);

    }
}
