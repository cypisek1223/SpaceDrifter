Shader "Custom/LightGradient"
{
    Properties
    {
        _Color1 ("Start Color", Color) = (1, 0, 0, 1) // Kolor pocz¹tkowy (domyœlnie czerwony)
        _Color2 ("End Color", Color) = (0, 0, 1, 1)   // Kolor koñcowy (domyœlnie niebieski)
        _Direction ("Gradient Direction", Range(0,1)) = 0  // 0 = pionowy, 1 = poziomy
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha

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
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            fixed4 _Color1;
            fixed4 _Color2;
            float _Direction; // Kierunek gradientu

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Obliczanie wartoœci gradientu w zale¿noœci od kierunku
                float t = lerp(i.uv.y, i.uv.x, _Direction);
                return lerp(_Color1, _Color2, t); // Przejœcie pomiêdzy kolorami
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/Diffuse"
}