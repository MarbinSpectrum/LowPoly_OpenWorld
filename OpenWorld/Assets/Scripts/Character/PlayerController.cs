using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement3D))]
[RequireComponent(typeof(PlayerAnimator))]
public class PlayerController : MonoBehaviour
{
    private Movement3D movement3D;
    private PlayerAnimator playerAnimator;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private bool canMove;

    private void Update()
    {
        if (movement3D == null)
            movement3D = GetComponent<Movement3D>();
        if (playerAnimator == null)
            playerAnimator = GetComponent<PlayerAnimator>();
        if (FreeLookCam.instance == null)
            return;

        PlayerAttack();
        PlayerMove();
    }

    public void PlayerMove()
    {
        float z = 0;
        float x = 0;

        if (playerAnimator.NowAttack() == -1)
        {
            z = Input.GetAxisRaw(VERTICAL);
            x = Input.GetAxisRaw(HORIZONTAL);
        }

        Vector3 dir = FreeLookCam.instance.transform.localRotation * Vector3.forward;
        Vector3 zDir = dir * z;
        Vector3 xDir = (Quaternion.Euler(0, 90, 0) * dir) * x;
        movement3D.TurnRotaion(x, z);
        movement3D.MoveTo(xDir + zDir);

        playerAnimator.animator.SetBool("Move", movement3D.nowMove);
        playerAnimator.animator.SetBool("Run", movement3D.run);
    }

    public void PlayerAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (playerAnimator.NowAttack() == -1)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 0 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 1 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 2 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
        }
    }
}
