Shader "Custom/BackGround"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeColor ("Edge Color", Color) = (1, 1, 1, 1)
        _GradientColor ("Gradient Color", Color) = (0, 0, 0, 1)
        _CenterColor ("Center Color", Color) = (1, 0, 0, 1)
        _CenterWidth ("Center Width", Float) = 0.5
    }
    SubShader
    {
        Tags { "Queue" = "Background" "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            ZWrite Off            // Wy³¹cza zapis do bufora g³êbokoœci
            ZTest Off             // Wy³¹cza test g³êbokoœci
            Blend SrcAlpha OneMinusSrcAlpha // Domyœlny tryb blendowania

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _EdgeColor;
            float4 _GradientColor;
            float4 _CenterColor;
            float _CenterWidth;

            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float normalizedX = uv.x;
                float halfCenterWidth = _CenterWidth * 0.5;

                fixed4 finalColor;

                if (normalizedX < (0.5 - halfCenterWidth))
                {
                    float gradient = normalizedX / (0.5 - halfCenterWidth);
                    finalColor = lerp(_GradientColor, _EdgeColor, gradient);
                }
                else if (normalizedX > (0.5 + halfCenterWidth))
                {
                    float gradient = (1.0 - normalizedX) / (0.5 - halfCenterWidth);
                    finalColor = lerp(_GradientColor, _EdgeColor, gradient);
                }
                else
                {
                    finalColor = _CenterColor;
                }

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
