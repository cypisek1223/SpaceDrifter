Shader "Sprite-Mask-CustomStencil"
{
    Properties
    {
        [IntRange] _StencilRef ("Stencil Value", Range(0,255)) = 0
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Cutoff ("Mask alpha cutoff", Range(0.0, 1.0)) = 0.0
    }

    HLSLINCLUDE
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    ENDHLSL

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
            "RenderPipeline" = "UniversalPipeline"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend Off
        ColorMask 0

        Stencil
        {
            Ref [_StencilRef]
            Comp Always
            Pass Replace
        }

        Pass
        {
            Tags{ "LightMode" = "Universal2D" }
            HLSLPROGRAM
            #pragma fragment MaskRenderingFragment
            #pragma vertex MaskRenderingVertex

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SpriteMaskShared.hlsl"
            ENDHLSL
        }
        Pass
        {
            Tags{ "LightMode" = "NormalsRendering" }
            HLSLPROGRAM
            #pragma fragment MaskRenderingFragment
            #pragma vertex MaskRenderingVertex

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SpriteMaskShared.hlsl"
            ENDHLSL
        }
        Pass
        {
            Tags{ "LightMode" = "UniversalForward" }
            HLSLPROGRAM
            #pragma fragment MaskRenderingFragment
            #pragma vertex MaskRenderingVertex

            #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SpriteMaskShared.hlsl"
            ENDHLSL
        }

    }
}
