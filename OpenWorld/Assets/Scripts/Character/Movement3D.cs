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

    [LabelText("점프력")]
    [MinValue(0)]
    public float jumpPower = 3.0f;   // 이동 속도

    [LabelText("최대 스테미너")]
    [MinValue(0)]
    public float maxStamina = 5.0f;   // 최대 스테미너

    [LabelText("중력 계수")]
    [ReadOnly]
    [SerializeField]
    private float gravity = -9.8f;    // 중력 계수
    private Vector3 moveDirection;    // 이동 방향

    [HideInInspector]
    public bool nowMove;
    [HideInInspector]
    public bool run;
    float stamina = 5;
    bool staminaFlag;

    [HideInInspector]
    public CharacterController characterController;
    private float turnTime = 0;
    private float turnDic;
    private float nowturnDic;

    private void Update()
    {
        if (characterController == null)
            characterController = GetComponent<CharacterController>();

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //중력처리
        if (characterController.isGrounded == false)
            moveDirection.y += gravity * Time.deltaTime;

        //이동 관리
        MoveMng();

        //스테미너 관리
        StaminaMng();

        //회전 관리
        TurnMng();
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void MoveMng()
    {
        //이동여부
        nowMove = (moveDirection.x != 0 || moveDirection.z != 0);

        //이동
        float nowSpeed = moveSpeed;
        if (Input.GetKeyDown(KeyCode.LeftShift) && 
            nowMove &&  stamina > 0)
            run = true;
        if (!nowMove)
            run = false;
        if (run)
            nowSpeed *= 1.5f;
        Vector3 move = moveDirection * moveSpeed * Time.deltaTime;

        characterController.Move(move);
    }

    public void MoveTo(Vector3 dircetion)
    {
        Vector3 movedis = FreeLookCam.instance.transform.localRotation * dircetion;
        moveDirection = new Vector3(movedis.x, moveDirection.y, movedis.z);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    public void Jump()
    {
        if (characterController.isGrounded)
            moveDirection.y = jumpPower;
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void StaminaMng()
    {
        if (run)
            stamina -= Time.deltaTime;
        else
            stamina += Time.deltaTime;
        if (stamina < 0)
            run = false;
        if (stamina <= 0 && !staminaFlag)
        {
            staminaFlag = true;
            stamina = -2;
        }
        else if (staminaFlag)
        {
            if (stamina > 0)
                staminaFlag = false;
        }
        stamina = Mathf.Min(maxStamina, stamina);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void TurnMng()
    {
        if (nowturnDic != turnDic)
        {
            turnTime = 0;
            nowturnDic = turnDic;
        }

        if (nowMove)
        {
            turnTime += Time.deltaTime;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, nowturnDic, 0) * FreeLookCam.instance.transform.localRotation, Mathf.Min(1, turnTime));
        }
        else
            turnTime = 0;
    }

    public void TurnRotaion(float x, float z)
    {
        if (x > 0 && z == 0)
            turnDic = 90;
        else if (x > 0 && z > 0)
            turnDic = 45;
        else if (x > 0 && z < 0)
            turnDic = 135;
        else if (x < 0 && z == 0)
            turnDic = 270;
        else if (x < 0 && z < 0)
            turnDic = 225;
        else if (x < 0 && z > 0)
            turnDic = 315;
        else if (x == 0 && z > 0)
            turnDic = 0;
        else if (x == 0 && z < 0)
            turnDic = 180;
    }
}
