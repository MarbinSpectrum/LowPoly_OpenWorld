using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(CharacterController))]
public class Movement3D : MonoBehaviour
{
    [LabelText("이동 속도")]
    [MinValue(0)]
    public float moveSpeed = 5.0f;   // 이동 속도

    [LabelText("중력 계수")]
    [ReadOnly]
    [SerializeField]
    private float gravity = -9.8f;    // 중력 계수
    private Vector3 moveDirection;    // 이동 방향

    [HideInInspector]
    public bool nowMove;

    private CharacterController characterController;
    private float turnTime = 0;
    private void Update()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //중력처리
        if (characterController.isGrounded == false)
            moveDirection.y += gravity * Time.deltaTime;

        //이동여부
        nowMove = (moveDirection.x != 0 || moveDirection.z != 0);

        //이동
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        //회전
        TurnRotaion();
    }

    public void MoveTo(Vector3 dircetion)
    {
        moveDirection = dircetion.normalized;
    }

    public void TurnRotaion()
    {
        if (nowMove)
        {
            turnTime += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, FreeLookCam.instance.transform.localRotation, Mathf.Min(1, turnTime));
        }
        else
            turnTime = 0;
    }
}
