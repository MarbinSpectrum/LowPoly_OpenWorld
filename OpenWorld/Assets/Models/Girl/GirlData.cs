using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
public class GirlData : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    [Title("눈")]
    [LabelText("종류")]
    [ValueDropdown("EyeType")]
    public int eye_Num;

    [Title("입")]
    [LabelText("종류")]
    [ValueDropdown("MouthType")]
    public int mouth_Num;

    [Title("머리")]
    [LabelText("색상")]
    [ColorPalette("Hair")]
    public Color hair_Color;

    [Title("피부")]
    [LabelText("색상")]
    [ColorPalette("Skin")]
    public Color skin_Color;

    [Title("옷")]
    [LabelText("색상")]
    [ColorPalette("Cloth")]
    public Color cloth_Color;

    [Space(60)]
    [Title("")]

    public SkinnedMeshRenderer meshRenderer;
    public List<Texture2D> eye_Tex = new List<Texture2D>();
    private static IEnumerable EyeType = new ValueDropdownList<int>()
    {
        {"고양이 눈동자" ,0},
        {"분노의 눈동자" ,1},
        {"겸허한 눈동자" ,2},
        {"총망한 눈동자" ,3},
        {"개구쟁이 눈동자" ,4},
        {"진부한 눈동자" ,5},
        {"매력적인 눈동자" ,6}
    };
    public List<Texture2D> mouth_Tex = new List<Texture2D>();
    private static IEnumerable MouthType = new ValueDropdownList<int>()
    {
        {"고양이 입" ,0},
        {"분노의 입" ,1},
        {"겸허한 입" ,2},
        {"총망한 입" ,3},
        {"개구쟁이 입" ,4},
        {"진부한 입" ,5},
        {"매력적인 입" ,6}
    };

    private void Update()
    {
        UpdateOutline();
    }
    private void UpdateOutline()
    {
        if (meshRenderer == null)
            return;

        if (mpb == null)
            mpb = new MaterialPropertyBlock();

        //머리 색상
        meshRenderer.GetPropertyBlock(mpb, 0);
        mpb.SetColor("_Color", hair_Color);
        mpb.SetColor("_ColorDim", hair_Color*1.5f);
        mpb.SetColor("_ColorDimExtra", hair_Color);
        mpb.SetColor("_ColorDimExtra", hair_Color);
        meshRenderer.SetPropertyBlock(mpb, 0);

        //피부 색상
        meshRenderer.GetPropertyBlock(mpb, 1);
        mpb.SetColor("_Color", skin_Color);
        mpb.SetColor("_ColorDim", skin_Color * skin_Color);
        mpb.SetColor("_ColorDimExtra", skin_Color * skin_Color * skin_Color);
        meshRenderer.SetPropertyBlock(mpb, 1);
        meshRenderer.GetPropertyBlock(mpb, 2);
        mpb.SetColor("_Color", skin_Color * 1.2f);
        mpb.SetColor("_ColorDim", skin_Color * skin_Color * 1.2f);
        mpb.SetColor("_ColorDimExtra", skin_Color * skin_Color * skin_Color * 1.2f);
        meshRenderer.SetPropertyBlock(mpb, 2);
        meshRenderer.GetPropertyBlock(mpb, 3);
        mpb.SetColor("_Color", skin_Color * 1.2f);
        mpb.SetColor("_ColorDim", skin_Color * skin_Color * 1.2f);
        mpb.SetColor("_ColorDimExtra", skin_Color * skin_Color * skin_Color * 1.2f);
        meshRenderer.SetPropertyBlock(mpb, 3);

        //옷 색상
        meshRenderer.GetPropertyBlock(mpb, 4);
        mpb.SetColor("_Color", cloth_Color);
        mpb.SetColor("_ColorDim", cloth_Color * cloth_Color);
        mpb.SetColor("_ColorDimExtra", cloth_Color * cloth_Color * cloth_Color);
        meshRenderer.SetPropertyBlock(mpb, 4);
        meshRenderer.GetPropertyBlock(mpb, 7);
        mpb.SetColor("_Color", cloth_Color * 1.5f);
        mpb.SetColor("_ColorDim", cloth_Color * cloth_Color * 1.5f);
        mpb.SetColor("_ColorDimExtra", cloth_Color * cloth_Color * cloth_Color * 1.5f);
        meshRenderer.SetPropertyBlock(mpb, 7);

        //눈
        meshRenderer.GetPropertyBlock(mpb, 3);
        mpb.SetTexture("_MainTex", eye_Tex[eye_Num]);
        meshRenderer.SetPropertyBlock(mpb, 3);

        //입
        meshRenderer.GetPropertyBlock(mpb, 2);
        mpb.SetTexture("_MainTex", mouth_Tex[mouth_Num]);
        meshRenderer.SetPropertyBlock(mpb, 2);

    }
}
