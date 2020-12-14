using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;   // 이동 속도
    private float gravity = -9.8f;    // 중력 계수
    private Vector3 moveDirection;    // 이동 방향

    private CharacterController characterController;

    private void Update()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();
        if (characterController.isGrounded == false)
            moveDirection.y += gravity * Time.deltaTime;
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void MoveTo(Vector3 dircetion)
    {
        moveDirection = dircetion;
    }
}
