Shader "UI/DefaultGradientHighlightLuma"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        [PerRendererData] _Color ("Color", Color) = (1,1,1,1)

        _HighlightColor ("Highlight Color", Color) = (1,1,1,1)
        _HighlightWidth ("Highlight Width", Range(0,1)) = 0.25
        _HighlightSpeed ("Highlight Speed", Float) = 1
        _HighlightIntensity ("Highlight Intensity", Float) = 1
        _HighlightAngle ("Highlight Angle", Range(0,360)) = 45

        _LumaInfluence ("Luma Influence", Range(0,1)) = 1

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255
        _ColorMask ("Color Mask", Float) = 15
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        ColorMask [_ColorMask]

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color  : COLOR;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            fixed4 _Color;

            fixed4 _HighlightColor;
            float _HighlightWidth;
            float _HighlightSpeed;
            float _HighlightIntensity;
            float _HighlightAngle;
            float _LumaInfluence;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 baseCol = tex2D(_MainTex, i.uv) * i.color;

                // ---------- LUMINANCE ----------
                float luma = dot(baseCol.rgb, float3(0.299, 0.587, 0.114));
                float lumaFactor = lerp(1.0, luma, _LumaInfluence);

                // ---------- HIGHLIGHT BAND ----------
                float rad = radians(_HighlightAngle);
                float2 dir = float2(cos(rad), sin(rad));

                float t = dot(i.uv, dir);
                t += _Time.y * _HighlightSpeed;

                float band =
                    smoothstep(0.5 - _HighlightWidth, 0.5, frac(t)) *
                    smoothstep(0.5 + _HighlightWidth, 0.5, frac(t));

                float finalHighlight = band * _HighlightIntensity * lumaFactor;

                fixed4 highlightCol = _HighlightColor * finalHighlight;

                baseCol.rgb += highlightCol.rgb * highlightCol.a;
                return baseCol;
            }
            ENDCG
        }
    }
}