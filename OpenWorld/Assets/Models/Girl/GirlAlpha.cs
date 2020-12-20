using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlAlpha : MonoBehaviour
{
    public GirlData girlData;

    private void Update()
    {
        if (Mathf.Abs(Camera.main.transform.localPosition.z) < 0.55f)
            girlData.alpha = true;
        else
            girlData.alpha = false;
    }
}
