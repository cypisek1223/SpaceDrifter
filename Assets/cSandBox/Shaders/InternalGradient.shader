Shader "Custom/InternalGradient"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Base Color", Color) = (1,1,1,1)
        _GradientColor ("Gradient Color", Color) = (0,0,0,1)
        _GradientStrength ("Gradient Strength", Range(0.1, 5)) = 1.0 // Nowy parametr si³y gradientu
    }
    SubShader
    {
        Tags { "Queue"="Geometry" }
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
                float2 worldPos : TEXCOORD1; // Wspó³rzêdne obiektu w œwiecie
            };

            sampler2D _MainTex;
            float4 _Color;
            float4 _GradientColor;
            float _GradientStrength; // Nowy parametr dla intensywnoœci gradientu
            float4 _MainTex_ST;

            // Funkcja wierzcho³ka
            v2f vert(appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Konwersja pozycji wierzcho³ka
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);      // Transformacja UV
                o.worldPos = v.vertex.xy;                 // Rzeczywiste wspó³rzêdne w lokalnej przestrzeni obiektu

                return o;
            }

            // Funkcja fragmentu
            fixed4 frag(v2f i) : SV_Target
            {
                // Œrodek sprite'a w przestrzeni œwiata
                float2 center = float2(0, 0);  // Lokalny œrodek sprite'a (zak³adamy 0,0)

                // Obliczanie odleg³oœci od œrodka obiektu
                float distance = length(i.worldPos - center);

                // Obliczenie maksymalnej odleg³oœci (zak³adamy rozmiar sprite'a)
                float maxDistance = length(float2(_MainTex_ST.x, _MainTex_ST.y));

                // Normalizacja odleg³oœci z uwzglêdnieniem si³y gradientu (_GradientStrength)
                float gradient = saturate(distance / (maxDistance * _GradientStrength));

                // Pobranie koloru tekstury
                fixed4 baseColor = tex2D(_MainTex, i.uv) * _Color;

                // Mieszanie kolorów na podstawie odleg³oœci
                fixed4 finalColor = lerp(baseColor, _GradientColor, gradient);

                return finalColor;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}