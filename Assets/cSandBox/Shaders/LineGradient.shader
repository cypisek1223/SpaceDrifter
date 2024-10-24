Shader "Custom/LineGradient"
{
    Properties
    {
        _Color1 ("Start Color", Color) = (1, 0, 0, 1)   // Pocz¹tkowy kolor (czerwony)
        _Color2 ("End Color", Color) = (0, 0, 1, 1)     // Koñcowy kolor (niebieski)
        _Direction ("Gradient Direction", Range(0,1)) = 0  // 0 = pionowy, 1 = poziomy
        _GradientScale ("Gradient Scale", Float) = 1.0  // Skala gradientu (kontrola d³ugoœci)
        _GradientWidth ("Gradient Width", Float) = 1.0  // Szerokoœæ gradientu (kontrola szerokoœci)
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
            float _Direction;        // Kierunek gradientu
            float _GradientScale;    // Skala gradientu (kontrola d³ugoœci)
            float _GradientWidth;    // Szerokoœæ gradientu

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

                // Skala gradientu: zmniejszenie wartoœci _GradientWidth sprawi, ¿e gradient bêdzie szerszy
                t = t * _GradientScale; // Skala pozostaje bez zmian

                // Wprowadzenie ograniczenia szerokoœci gradientu
                t /= _GradientWidth;  // Dzielimy przez szerokoœæ, aby uzyskaæ po¿¹dane efekty

                return lerp(_Color1, _Color2, saturate(t)); // Przejœcie pomiêdzy kolorami
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/Diffuse"
}