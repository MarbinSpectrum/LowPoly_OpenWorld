// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Roystan/Toon Complete Y Cut effect"
{
    Properties
    {
        _MainTex("MainTex", 2D) = "white" {}
        _YCut("Y Cut", Range(-3,3)) = 0
        _EffectSize("EffectSize", Range(0,1)) = 0
        [HDR]_EffectColor("EffectColor",Color) = (1,1,1,1)

        _EffectSize2("EffectSize2", Range(0,1)) = 0
        [HDR]_EffectColor2("EffectColor2",Color) = (1,1,1,1)
        _Effect2Power("Effect2Power", float) = 1
    }
        SubShader
            {
                Lighting Off
                Fog { Mode Off }
                ZWrite Off
                Blend SrcAlpha OneMinusSrcAlpha


                Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
                Pass
                {
                    CGPROGRAM

                    #pragma vertex vert
                    #pragma fragment frag

                    #include "UnityCG.cginc"

                    sampler2D _MainTex;
                    float4 _MainTex_ST;
                    float _YCut;
                    float _EffectSize;
                    float4 _EffectColor;

                    float _EffectSize2;
                    float4 _EffectColor2;
                    float _Effect2Power;

                    struct appdata
                    {
                        float4 vertex : POSITION;
                        float2 uv : TEXCOORD0;
                        float3 lpos : TEXCOORD2;
                    };

                    struct v2f
                    {
                        float4 pos : SV_POSITION;
                        float2 uv : TEXCOORD0;
                        float3 lpos : TEXCOORD2;
                    };


                    v2f vert(appdata v)
                    {
                        v2f o;

                        fixed4 pos = v.vertex;
                        o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                        o.lpos = v.vertex;
                        o.pos = UnityObjectToClipPos(pos);

                        return o;
                    }

                    fixed4 frag(v2f i) : SV_Target
                    {
                        fixed4 output = fixed4(0,0,0,0);
                        output.rgb = i.lpos;
                        float4 sample = tex2D(_MainTex, i.uv);
                        if (output.g > _YCut)
                            sample.a = 0;
                        else if (output.g > _YCut - _EffectSize)
                        {
                         /*   sample.rgb = lerp(sample.rgb, _EffectColor.rgb, _EffectLerp);*/
                            sample.rgb = _EffectColor.rgb;
                        }
                        else if (output.g > _YCut - _EffectSize - _EffectSize2)
                        {
                            float v = output.g - (_YCut - _EffectSize - _EffectSize2);
                            v /= _EffectSize2;
                            sample.rgb *= lerp(1, _EffectColor2.rgb, v);
                        }
                        return sample;
                    }

                    ENDCG
                }
            }
}