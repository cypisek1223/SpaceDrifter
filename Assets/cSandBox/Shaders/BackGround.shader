Shader "Custom/BackGround"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _EdgeColor ("Edge Color", Color) = (1, 1, 1, 1) // Kolor kraw�dzi
        _GradientColor ("Gradient Color", Color) = (0, 0, 0, 1) // Kolor gradientu
        _CenterColor ("Center Color", Color) = (1, 0, 0, 1) // Kolor centralny (domy�lnie czerwony)
        _CenterWidth ("Center Width", Float) = 0.5 // Szeroko�� centralnego paska
    }
    SubShader
    {
        Tags { "Queue" = "Geometry" }
        LOD 200

        Pass
        {
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
            float4 _EdgeColor;       // Kolor kraw�dzi
            float4 _GradientColor;   // Kolor gradientu
            float4 _CenterColor;     // Kolor centralny
            float _CenterWidth;      // Elastyczna szeroko�� centralnego paska

            // Funkcja wierzcho�ka
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Konwersja pozycji wierzcho�ka
                o.uv = v.uv;                               // Zapisujemy UV
                return o;
            }

            // Funkcja fragmentu
            fixed4 frag(v2f i) : SV_Target
            {
                // Ustal wsp�rz�dne UV
                float2 uv = i.uv;

                // Normalizujemy wsp�rz�dne X do zakresu [0, 1]
                float normalizedX = uv.x;

                // Ustalmy po�ow� szeroko�ci centralnego paska
                float halfCenterWidth = _CenterWidth * 0.5;

                // Kolor docelowy
                fixed4 finalColor;

                // Gradient od lewej kraw�dzi do pocz�tku centralnego paska
                if (normalizedX < (0.5 - halfCenterWidth))
                {
                    float gradient = normalizedX / (0.5 - halfCenterWidth);
                    finalColor = lerp(_GradientColor, _EdgeColor, gradient); // Gradient od �rodka do kraw�dzi
                }
                // Gradient od prawej kraw�dzi do ko�ca centralnego paska
                else if (normalizedX > (0.5 + halfCenterWidth))
                {
                    float gradient = (1.0 - normalizedX) / (0.5 - halfCenterWidth);
                    finalColor = lerp(_GradientColor, _EdgeColor, gradient); // Gradient od �rodka do kraw�dzi
                }
                // Centralny pasek
                else
                {
                    finalColor = _CenterColor; // Kolor centralnego paska
                }

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
