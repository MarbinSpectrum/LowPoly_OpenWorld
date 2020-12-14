using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MurphyData : MonoBehaviour
{
    private MaterialPropertyBlock mpb;

    [ColorUsage(false)] public Color base_Color1;
    [ColorUsage(false)] public Color base_Color2;
    [ColorUsage(false)] public Color cloth_Color;
    public int face_Num;
    public int mouth_Num;
    public int cloth_Num;

    [Space(20)]
    [Header("-------------------------------------------")]

    public SkinnedMeshRenderer meshRenderer;
    public List<Texture2D> face_Tex = new List<Texture2D>();
    public List<Texture2D> mouth_Tex = new List<Texture2D>();
    public List<Texture2D> cloth_Tex = new List<Texture2D>();

    private void Update()
    {
        UpdateOutline();
    }
    private void UpdateOutline()
    {
        if (meshRenderer == null) 
            return;

        if(mpb == null)
            mpb = new MaterialPropertyBlock();

        //옷색깔
        meshRenderer.GetPropertyBlock(mpb, 0);
        mpb.SetColor("_BaseColor", cloth_Color);
        mpb.SetColor("_1st_ShadeColor", cloth_Color);
        mpb.SetColor("_2st_ShadeColor", cloth_Color);
        mpb.SetColor("_Outline_Color", cloth_Color);
        meshRenderer.SetPropertyBlock(mpb, 0);

        //1번색깔
        meshRenderer.GetPropertyBlock(mpb, 1);
        mpb.SetColor("_BaseColor", base_Color1);
        mpb.SetColor("_1st_ShadeColor", base_Color1);
        mpb.SetColor("_2st_ShadeColor", base_Color1);
        mpb.SetColor("_Outline_Color", base_Color1);
        meshRenderer.SetPropertyBlock(mpb, 1);
        meshRenderer.GetPropertyBlock(mpb, 5);
        mpb.SetColor("_BaseColor", base_Color1);
        mpb.SetColor("_1st_ShadeColor", base_Color1);
        mpb.SetColor("_2st_ShadeColor", base_Color1);
        mpb.SetColor("_Outline_Color", base_Color1);
        meshRenderer.SetPropertyBlock(mpb, 5);

        //2번색깔
        meshRenderer.GetPropertyBlock(mpb, 2);
        mpb.SetColor("_BaseColor", base_Color2);
        mpb.SetColor("_1st_ShadeColor", base_Color2);
        mpb.SetColor("_2st_ShadeColor", base_Color2);
        mpb.SetColor("_Outline_Color", base_Color2);
        meshRenderer.SetPropertyBlock(mpb, 2);
        meshRenderer.GetPropertyBlock(mpb, 6);
        mpb.SetColor("_BaseColor", base_Color2);
        mpb.SetColor("_1st_ShadeColor", base_Color2);
        mpb.SetColor("_2st_ShadeColor", base_Color2);
        mpb.SetColor("_Outline_Color", base_Color2);
        meshRenderer.SetPropertyBlock(mpb, 6);

        //얼굴
        face_Num = Mathf.Min(face_Num, face_Tex.Count - 1);
        face_Num = Mathf.Max(face_Num, 0);
        if (face_Num >= 0)
        {
            meshRenderer.GetPropertyBlock(mpb, 1);
            mpb.SetTexture("_MainTex", face_Tex[face_Num]);
            mpb.SetTexture("_1st_ShadeMap", face_Tex[face_Num]);
            mpb.SetTexture("_2st_ShadeMap", face_Tex[face_Num]);
            meshRenderer.SetPropertyBlock(mpb, 1);
            meshRenderer.GetPropertyBlock(mpb, 2);
            mpb.SetTexture("_MainTex", face_Tex[face_Num]);
            mpb.SetTexture("_1st_ShadeMap", face_Tex[face_Num]);
            mpb.SetTexture("_2st_ShadeMap", face_Tex[face_Num]);
            meshRenderer.SetPropertyBlock(mpb, 2);
        }

        //입
        mouth_Num = Mathf.Min(mouth_Num, mouth_Tex.Count - 1);
        mouth_Num = Mathf.Max(mouth_Num, 0);
        if (mouth_Num >= 0)
        {
            meshRenderer.GetPropertyBlock(mpb, 8);
            mpb.SetTexture("_MainTex", mouth_Tex[mouth_Num]);
            mpb.SetTexture("_1st_ShadeMap", mouth_Tex[mouth_Num]);
            mpb.SetTexture("_2st_ShadeMap", mouth_Tex[mouth_Num]);
            meshRenderer.SetPropertyBlock(mpb, 8);
        }

        //옷
        cloth_Num = Mathf.Min(cloth_Num, cloth_Tex.Count - 1);
        cloth_Num = Mathf.Max(cloth_Num, 0);
        if (cloth_Num >= 0)
        {
            meshRenderer.GetPropertyBlock(mpb, 0);
            mpb.SetTexture("_MainTex", cloth_Tex[cloth_Num]);
            mpb.SetTexture("_1st_ShadeMap", cloth_Tex[cloth_Num]);
            mpb.SetTexture("_2st_ShadeMap", cloth_Tex[cloth_Num]);
            meshRenderer.SetPropertyBlock(mpb, 0);
        }
    }
}
