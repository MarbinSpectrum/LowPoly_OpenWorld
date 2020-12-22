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
    private const string MOVE = "Move";
    private const string RUN = "Run";
    private const KeyCode JUMP_KEYCODE = KeyCode.Space;
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
        PlayerJump();
    }

    public void PlayerAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (playerAnimator.NowAttack() < 0)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 0 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 1 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 2 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
            else if (playerAnimator.NowAttack() == 3 && playerAnimator.NowAttackTime() > 0.6f)
                playerAnimator.animator.SetTrigger("Attack");
        }
    }

    public void PlayerMove()
    {
        float z = 0;
        float x = 0;

        if (playerAnimator.NowAttack() < 0)
        {
            z = Input.GetAxisRaw(VERTICAL);
            x = Input.GetAxisRaw(HORIZONTAL);
        }

        movement3D.TurnRotaion(x, z);
        movement3D.MoveTo(new Vector3(x, 0, z));

        playerAnimator.animator.SetBool(MOVE, movement3D.nowMove);
        playerAnimator.animator.SetBool(RUN, movement3D.run);
    }

    public void PlayerJump()
    {
        if (!movement3D.characterController)
            return;

        if (Input.GetKeyDown(JUMP_KEYCODE))
        {
            movement3D.Jump();
            if(movement3D.characterController.isGrounded)
                playerAnimator.animator.SetTrigger("Jump");
        }

        playerAnimator.animator.SetBool("Ground", movement3D.characterController.isGrounded);

    }

}
