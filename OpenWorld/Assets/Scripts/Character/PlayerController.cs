using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement3D))]
public class PlayerController : MonoBehaviour
{
    private Movement3D movement3D;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private void Update()
    {
        if (movement3D == null)
            movement3D = GetComponent<Movement3D>();
        if (FreeLookCam.instance == null)
            return;

        float z = Input.GetAxisRaw(VERTICAL);
        float x = Input.GetAxisRaw(HORIZONTAL);

        Vector3 dir = FreeLookCam.instance.transform.localRotation * Vector3.forward;
        Vector3 zDir = dir * z;
        Vector3 xDir = (Quaternion.Euler(0, 90, 0) * dir) * x;

        movement3D.MoveTo(xDir + zDir);

    }
}
