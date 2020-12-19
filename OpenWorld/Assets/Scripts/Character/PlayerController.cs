using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement3D movement3D;

    private void Update()
    {
        if (movement3D == null)
            movement3D = GetComponent<Movement3D>();
        if (FreeLookCam.instance == null)
            return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Quaternion.Euler(0, 0, FreeLookCam.instance.m_LookAngle);

        movement3D.MoveTo(Quaternion.Euler(0, 0, FreeLookCam.instance.m_LookAngle) * new Vector3(x, 0, z));

    }
}
