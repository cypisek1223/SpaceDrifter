Shader "Custom/LineGradient"
{
    Properties
    {
        _Color1 ("Start Color", Color) = (1, 0, 0, 1)   // Pocz�tkowy kolor (czerwony)
        _Color2 ("End Color", Color) = (0, 0, 1, 1)     // Ko�cowy kolor (niebieski)
        _Direction ("Gradient Direction", Range(0,1)) = 0  // 0 = pionowy, 1 = poziomy
        _GradientScale ("Gradient Scale", Float) = 1.0  // Skala gradientu (kontrola d�ugo�ci)
        _GradientWidth ("Gradient Width", Float) = 1.0  // Szeroko�� gradientu (kontrola szeroko�ci)
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
            float _GradientScale;    // Skala gradientu (kontrola d�ugo�ci)
            float _GradientWidth;    // Szeroko�� gradientu

            v2f vert(appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Obliczanie warto�ci gradientu w zale�no�ci od kierunku
                float t = lerp(i.uv.y, i.uv.x, _Direction);

                // Skala gradientu: zmniejszenie warto�ci _GradientWidth sprawi, �e gradient b�dzie szerszy
                t = t * _GradientScale; // Skala pozostaje bez zmian

                // Wprowadzenie ograniczenia szeroko�ci gradientu
                t /= _GradientWidth;  // Dzielimy przez szeroko��, aby uzyska� po��dane efekty

                return lerp(_Color1, _Color2, saturate(t)); // Przej�cie pomi�dzy kolorami
            }
            ENDCG
        }
    }
    FallBack "Transparent/Cutout/Diffuse"
}