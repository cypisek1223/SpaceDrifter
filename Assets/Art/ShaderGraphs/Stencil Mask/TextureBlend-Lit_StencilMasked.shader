Shader "TextureBlend-Lit_StencilMasked"
{
    Properties
    {
                [IntRange] _StencilRef ("Stencil Value", Range(0,255)) = 0
        [NoScaleOffset]_MainTex("_MainTex", 2D) = "white" {}
        [NoScaleOffset]_WhitePattern("WhitePattern", 2D) = "white" {}
        [NoScaleOffset]_BlackPattern("BlackPattern", 2D) = "white" {}
        _WhiteUvs("WhiteUvs", Vector) = (1, 1, 0, 0)
        _BlackUvs("BlackUvs", Vector) = (1, 1, 0, 0)
        _WhiteColor("WhiteColor", Color) = (1, 1, 1, 0)
        _BlackColor("BlackColor", Color) = (0.245283, 0.1747063, 0.1747063, 0)
        _BlendOffset("BlendOffset", Range(-1, 1)) = 0
        [NoScaleOffset]_NoiseOverlay("NoiseOverlay", 2D) = "black" {}
        _NoiseTiling("NoiseTiling", Vector) = (1, 1, 0, 0)
        _NoiseStrength("NoiseStrength", Range(-1, 5)) = 1
        _NoiseOffset("NoiseOffset", Float) = 0
        _Power("Power", Float) = 0
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
         
        Stencil
        {
            Ref [_StencilRef]
            Comp Equal
        }

        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Lit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"=""
        }
        Pass
        {
            Name "Sprite Lit"
            Tags
            {
                "LightMode" = "Universal2D"
            }
        
            // Render State
            Cull Off
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 2.0
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag
        
            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>
        
            // Keywords
            #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_0
        #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_1
        #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_2
        #pragma multi_compile _ USE_SHAPE_LIGHT_TYPE_3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
            // GraphKeywords: <None>
        
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define VARYINGS_NEED_SCREENPOSITION
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SPRITELIT
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
            // Includes
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
            // --------------------------------------------------
            // Structs and Packing
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
            struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float4 texCoord0;
             float4 color;
             float4 screenPosition;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float4 interp3 : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyzw =  input.texCoord0;
            output.interp2.xyzw =  input.color;
            output.interp3.xyzw =  input.screenPosition;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            output.color = input.interp2.xyzw;
            output.screenPosition = input.interp3.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float4 _BlackPattern_TexelSize;
        half2 _WhiteUvs;
        half2 _BlackUvs;
        float4 _WhitePattern_TexelSize;
        half4 _BlackColor;
        half4 _WhiteColor;
        half _BlendOffset;
        float4 _NoiseOverlay_TexelSize;
        half2 _NoiseTiling;
        half _NoiseStrength;
        half _NoiseOffset;
        half _Power;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_BlackPattern);
        SAMPLER(sampler_BlackPattern);
        TEXTURE2D(_WhitePattern);
        SAMPLER(sampler_WhitePattern);
        TEXTURE2D(_NoiseOverlay);
        SAMPLER(sampler_NoiseOverlay);
        
            // Graph Includes
            // GraphIncludes: <None>
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Graph Functions
            
        void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A - B;
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_half4(half4 A, half4 B, out half4 Out)
        {
            Out = A + B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Subtract_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A - B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            half Alpha;
            half4 SpriteMask;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0 = UnityBuildTexture2DStructNoScale(_BlackPattern);
            float3 _Subtract_2611599512eb4160ae95748584d3e3eb_Out_2;
            Unity_Subtract_float3(IN.WorldSpacePosition, SHADERGRAPH_OBJECT_POSITION, _Subtract_2611599512eb4160ae95748584d3e3eb_Out_2);
            half2 _Property_5e2ca93f020f426ba36d9bfe9f6116ad_Out_0 = _BlackUvs;
            float2 _Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2;
            Unity_Multiply_float2_float2((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_5e2ca93f020f426ba36d9bfe9f6116ad_Out_0, _Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2);
            float4 _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0 = SAMPLE_TEXTURE2D(_Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.tex, _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.samplerstate, _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.GetTransformedUV(_Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2));
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_R_4 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.r;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_G_5 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.g;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_B_6 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.b;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_A_7 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.a;
            half4 _Property_bbb3876183cd4b3ab3533dcee97760b3_Out_0 = _BlackColor;
            float4 _Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2;
            Unity_Multiply_float4_float4((_SampleTexture2D_37ad7634563149debaa7adec8d280677_R_4.xxxx), _Property_bbb3876183cd4b3ab3533dcee97760b3_Out_0, _Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2);
            half _Property_b2cded26bcef426692fb0bd56700e838_Out_0 = _Power;
            UnityTexture2D _Property_3d32a97b7ba1453086089eea0de504dc_Out_0 = UnityBuildTexture2DStructNoScale(_WhitePattern);
            half2 _Property_8c713f0da4f149b196eee1adb45a205a_Out_0 = _WhiteUvs;
            float2 _Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2;
            Unity_Multiply_float2_float2((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_8c713f0da4f149b196eee1adb45a205a_Out_0, _Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2);
            float4 _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0 = SAMPLE_TEXTURE2D(_Property_3d32a97b7ba1453086089eea0de504dc_Out_0.tex, _Property_3d32a97b7ba1453086089eea0de504dc_Out_0.samplerstate, _Property_3d32a97b7ba1453086089eea0de504dc_Out_0.GetTransformedUV(_Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2));
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_R_4 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.r;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_G_5 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.g;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_B_6 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.b;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_A_7 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.a;
            half4 _Property_69f39d042e324215960957c9552d249e_Out_0 = _WhiteColor;
            float4 _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2;
            Unity_Multiply_float4_float4((_SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_R_4.xxxx), _Property_69f39d042e324215960957c9552d249e_Out_0, _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2);
            float4 _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2;
            Unity_Multiply_float4_float4((_Property_b2cded26bcef426692fb0bd56700e838_Out_0.xxxx), _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2, _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2);
            UnityTexture2D _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            half4 _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0 = SAMPLE_TEXTURE2D(_Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.tex, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.samplerstate, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.GetTransformedUV(IN.uv0.xy));
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_R_4 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.r;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_G_5 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.g;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_B_6 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.b;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.a;
            half _Property_09403be1effb4969aadd0253b8275131_Out_0 = _BlendOffset;
            half4 _Add_d16de15f3584456f9d5ad00f52ccddad_Out_2;
            Unity_Add_half4(_SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0, (_Property_09403be1effb4969aadd0253b8275131_Out_0.xxxx), _Add_d16de15f3584456f9d5ad00f52ccddad_Out_2);
            UnityTexture2D _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0 = UnityBuildTexture2DStructNoScale(_NoiseOverlay);
            half2 _Property_7f72c5aa47d740cd81ae4c01582e613a_Out_0 = _NoiseTiling;
            half _Property_c3a1c3696eb24532937bcc75c247209c_Out_0 = _NoiseOffset;
            float2 _TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3;
            Unity_TilingAndOffset_float((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_7f72c5aa47d740cd81ae4c01582e613a_Out_0, (_Property_c3a1c3696eb24532937bcc75c247209c_Out_0.xx), _TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3);
            float4 _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.tex, _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.samplerstate, _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.GetTransformedUV(_TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3));
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_R_4 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.r;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_G_5 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.g;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_B_6 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.b;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_A_7 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.a;
            half _Property_7edae69dffce42b982787386e591baa0_Out_0 = _NoiseStrength;
            float _Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_R_4, _Property_7edae69dffce42b982787386e591baa0_Out_0, _Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2);
            float4 _Subtract_d911dc3580f240208967a10d14db51a0_Out_2;
            Unity_Subtract_float4(_Add_d16de15f3584456f9d5ad00f52ccddad_Out_2, (_Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2.xxxx), _Subtract_d911dc3580f240208967a10d14db51a0_Out_2);
            float4 _Clamp_232882b14617487892fc67d5789a9222_Out_3;
            Unity_Clamp_float4(_Subtract_d911dc3580f240208967a10d14db51a0_Out_2, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_232882b14617487892fc67d5789a9222_Out_3);
            float4 _Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3;
            Unity_Lerp_float4(_Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2, _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2, _Clamp_232882b14617487892fc67d5789a9222_Out_3, _Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3);
            surface.BaseColor = (_Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3.xyz);
            surface.Alpha = _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7;
            surface.SpriteMask = IsGammaSpace() ? half4(1, 1, 1, 1) : half4 (SRGBToLinear(half3(1, 1, 1)), 1);
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
            output.WorldSpacePosition =                         input.positionWS;
            output.uv0 =                                        input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
            return output;
        }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteLitPass.hlsl"
        
            ENDHLSL
        }
        Pass
        {
            Name "Sprite Normal"
            Tags
            {
                "LightMode" = "NormalsRendering"
            }
        
            // Render State
            Cull Off
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 2.0
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag
        
            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>
        
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
        
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SPRITENORMAL
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
            // Includes
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
            // --------------------------------------------------
            // Structs and Packing
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
            struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 TangentSpaceNormal;
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float3 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float4 interp3 : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float4 _BlackPattern_TexelSize;
        half2 _WhiteUvs;
        half2 _BlackUvs;
        float4 _WhitePattern_TexelSize;
        half4 _BlackColor;
        half4 _WhiteColor;
        half _BlendOffset;
        float4 _NoiseOverlay_TexelSize;
        half2 _NoiseTiling;
        half _NoiseStrength;
        half _NoiseOffset;
        half _Power;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_BlackPattern);
        SAMPLER(sampler_BlackPattern);
        TEXTURE2D(_WhitePattern);
        SAMPLER(sampler_WhitePattern);
        TEXTURE2D(_NoiseOverlay);
        SAMPLER(sampler_NoiseOverlay);
        
            // Graph Includes
            // GraphIncludes: <None>
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Graph Functions
            
        void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A - B;
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_half4(half4 A, half4 B, out half4 Out)
        {
            Out = A + B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Subtract_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A - B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            half Alpha;
            half3 NormalTS;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0 = UnityBuildTexture2DStructNoScale(_BlackPattern);
            float3 _Subtract_2611599512eb4160ae95748584d3e3eb_Out_2;
            Unity_Subtract_float3(IN.WorldSpacePosition, SHADERGRAPH_OBJECT_POSITION, _Subtract_2611599512eb4160ae95748584d3e3eb_Out_2);
            half2 _Property_5e2ca93f020f426ba36d9bfe9f6116ad_Out_0 = _BlackUvs;
            float2 _Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2;
            Unity_Multiply_float2_float2((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_5e2ca93f020f426ba36d9bfe9f6116ad_Out_0, _Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2);
            float4 _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0 = SAMPLE_TEXTURE2D(_Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.tex, _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.samplerstate, _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.GetTransformedUV(_Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2));
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_R_4 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.r;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_G_5 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.g;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_B_6 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.b;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_A_7 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.a;
            half4 _Property_bbb3876183cd4b3ab3533dcee97760b3_Out_0 = _BlackColor;
            float4 _Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2;
            Unity_Multiply_float4_float4((_SampleTexture2D_37ad7634563149debaa7adec8d280677_R_4.xxxx), _Property_bbb3876183cd4b3ab3533dcee97760b3_Out_0, _Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2);
            half _Property_b2cded26bcef426692fb0bd56700e838_Out_0 = _Power;
            UnityTexture2D _Property_3d32a97b7ba1453086089eea0de504dc_Out_0 = UnityBuildTexture2DStructNoScale(_WhitePattern);
            half2 _Property_8c713f0da4f149b196eee1adb45a205a_Out_0 = _WhiteUvs;
            float2 _Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2;
            Unity_Multiply_float2_float2((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_8c713f0da4f149b196eee1adb45a205a_Out_0, _Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2);
            float4 _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0 = SAMPLE_TEXTURE2D(_Property_3d32a97b7ba1453086089eea0de504dc_Out_0.tex, _Property_3d32a97b7ba1453086089eea0de504dc_Out_0.samplerstate, _Property_3d32a97b7ba1453086089eea0de504dc_Out_0.GetTransformedUV(_Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2));
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_R_4 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.r;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_G_5 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.g;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_B_6 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.b;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_A_7 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.a;
            half4 _Property_69f39d042e324215960957c9552d249e_Out_0 = _WhiteColor;
            float4 _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2;
            Unity_Multiply_float4_float4((_SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_R_4.xxxx), _Property_69f39d042e324215960957c9552d249e_Out_0, _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2);
            float4 _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2;
            Unity_Multiply_float4_float4((_Property_b2cded26bcef426692fb0bd56700e838_Out_0.xxxx), _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2, _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2);
            UnityTexture2D _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            half4 _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0 = SAMPLE_TEXTURE2D(_Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.tex, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.samplerstate, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.GetTransformedUV(IN.uv0.xy));
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_R_4 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.r;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_G_5 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.g;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_B_6 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.b;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.a;
            half _Property_09403be1effb4969aadd0253b8275131_Out_0 = _BlendOffset;
            half4 _Add_d16de15f3584456f9d5ad00f52ccddad_Out_2;
            Unity_Add_half4(_SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0, (_Property_09403be1effb4969aadd0253b8275131_Out_0.xxxx), _Add_d16de15f3584456f9d5ad00f52ccddad_Out_2);
            UnityTexture2D _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0 = UnityBuildTexture2DStructNoScale(_NoiseOverlay);
            half2 _Property_7f72c5aa47d740cd81ae4c01582e613a_Out_0 = _NoiseTiling;
            half _Property_c3a1c3696eb24532937bcc75c247209c_Out_0 = _NoiseOffset;
            float2 _TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3;
            Unity_TilingAndOffset_float((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_7f72c5aa47d740cd81ae4c01582e613a_Out_0, (_Property_c3a1c3696eb24532937bcc75c247209c_Out_0.xx), _TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3);
            float4 _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.tex, _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.samplerstate, _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.GetTransformedUV(_TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3));
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_R_4 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.r;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_G_5 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.g;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_B_6 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.b;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_A_7 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.a;
            half _Property_7edae69dffce42b982787386e591baa0_Out_0 = _NoiseStrength;
            float _Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_R_4, _Property_7edae69dffce42b982787386e591baa0_Out_0, _Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2);
            float4 _Subtract_d911dc3580f240208967a10d14db51a0_Out_2;
            Unity_Subtract_float4(_Add_d16de15f3584456f9d5ad00f52ccddad_Out_2, (_Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2.xxxx), _Subtract_d911dc3580f240208967a10d14db51a0_Out_2);
            float4 _Clamp_232882b14617487892fc67d5789a9222_Out_3;
            Unity_Clamp_float4(_Subtract_d911dc3580f240208967a10d14db51a0_Out_2, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_232882b14617487892fc67d5789a9222_Out_3);
            float4 _Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3;
            Unity_Lerp_float4(_Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2, _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2, _Clamp_232882b14617487892fc67d5789a9222_Out_3, _Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3);
            surface.BaseColor = (_Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3.xyz);
            surface.Alpha = _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7;
            surface.NormalTS = IN.TangentSpaceNormal;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
            output.TangentSpaceNormal =                         float3(0.0f, 0.0f, 1.0f);
        
        
            output.WorldSpacePosition =                         input.positionWS;
            output.uv0 =                                        input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
            return output;
        }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteNormalPass.hlsl"
        
            ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
            // Render State
            Cull Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 2.0
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag
        
            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>
        
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
        
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD0
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENESELECTIONPASS 1
        
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
            // Includes
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
            // --------------------------------------------------
            // Structs and Packing
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
            struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 interp0 : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.interp0.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float4 _BlackPattern_TexelSize;
        half2 _WhiteUvs;
        half2 _BlackUvs;
        float4 _WhitePattern_TexelSize;
        half4 _BlackColor;
        half4 _WhiteColor;
        half _BlendOffset;
        float4 _NoiseOverlay_TexelSize;
        half2 _NoiseTiling;
        half _NoiseStrength;
        half _NoiseOffset;
        half _Power;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_BlackPattern);
        SAMPLER(sampler_BlackPattern);
        TEXTURE2D(_WhitePattern);
        SAMPLER(sampler_WhitePattern);
        TEXTURE2D(_NoiseOverlay);
        SAMPLER(sampler_NoiseOverlay);
        
            // Graph Includes
            // GraphIncludes: <None>
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Graph Functions
            // GraphFunctions: <None>
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
            // Graph Pixel
            struct SurfaceDescription
        {
            half Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            half4 _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0 = SAMPLE_TEXTURE2D(_Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.tex, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.samplerstate, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.GetTransformedUV(IN.uv0.xy));
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_R_4 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.r;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_G_5 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.g;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_B_6 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.b;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.a;
            surface.Alpha = _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
            output.uv0 =                                        input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
            return output;
        }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
            ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
            // Render State
            Cull Back
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 2.0
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag
        
            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>
        
            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>
        
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD0
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENEPICKINGPASS 1
        
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
            // Includes
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
            // --------------------------------------------------
            // Structs and Packing
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
            struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 interp0 : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.interp0.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float4 _BlackPattern_TexelSize;
        half2 _WhiteUvs;
        half2 _BlackUvs;
        float4 _WhitePattern_TexelSize;
        half4 _BlackColor;
        half4 _WhiteColor;
        half _BlendOffset;
        float4 _NoiseOverlay_TexelSize;
        half2 _NoiseTiling;
        half _NoiseStrength;
        half _NoiseOffset;
        half _Power;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_BlackPattern);
        SAMPLER(sampler_BlackPattern);
        TEXTURE2D(_WhitePattern);
        SAMPLER(sampler_WhitePattern);
        TEXTURE2D(_NoiseOverlay);
        SAMPLER(sampler_NoiseOverlay);
        
            // Graph Includes
            // GraphIncludes: <None>
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Graph Functions
            // GraphFunctions: <None>
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
            // Graph Pixel
            struct SurfaceDescription
        {
            half Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            half4 _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0 = SAMPLE_TEXTURE2D(_Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.tex, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.samplerstate, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.GetTransformedUV(IN.uv0.xy));
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_R_4 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.r;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_G_5 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.g;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_B_6 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.b;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.a;
            surface.Alpha = _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
        
        
            output.uv0 =                                        input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
            return output;
        }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
            ENDHLSL
        }
        Pass
        {
            Name "Sprite Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }
        
            // Render State
            Cull Off
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
            // Debug
            // <None>
        
            // --------------------------------------------------
            // Pass
        
            HLSLPROGRAM
        
            // Pragmas
            #pragma target 2.0
        #pragma exclude_renderers d3d11_9x
        #pragma vertex vert
        #pragma fragment frag
        
            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>
        
            // Keywords
            #pragma multi_compile_fragment _ DEBUG_DISPLAY
            // GraphKeywords: <None>
        
            // Defines
            #define _SURFACE_TYPE_TRANSPARENT 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SPRITEFORWARD
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
            // Includes
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreInclude' */
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
            // --------------------------------------------------
            // Structs and Packing
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
            struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float4 texCoord0;
             float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float3 TangentSpaceNormal;
             float3 WorldSpacePosition;
             float4 uv0;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
             float4 interp2 : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyzw =  input.texCoord0;
            output.interp2.xyzw =  input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            output.color = input.interp2.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
            // --------------------------------------------------
            // Graph
        
            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        float4 _MainTex_TexelSize;
        float4 _BlackPattern_TexelSize;
        half2 _WhiteUvs;
        half2 _BlackUvs;
        float4 _WhitePattern_TexelSize;
        half4 _BlackColor;
        half4 _WhiteColor;
        half _BlendOffset;
        float4 _NoiseOverlay_TexelSize;
        half2 _NoiseTiling;
        half _NoiseStrength;
        half _NoiseOffset;
        half _Power;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        TEXTURE2D(_BlackPattern);
        SAMPLER(sampler_BlackPattern);
        TEXTURE2D(_WhitePattern);
        SAMPLER(sampler_WhitePattern);
        TEXTURE2D(_NoiseOverlay);
        SAMPLER(sampler_NoiseOverlay);
        
            // Graph Includes
            // GraphIncludes: <None>
        
            // -- Property used by ScenePickingPass
            #ifdef SCENEPICKINGPASS
            float4 _SelectionID;
            #endif
        
            // -- Properties used by SceneSelectionPass
            #ifdef SCENESELECTIONPASS
            int _ObjectId;
            int _PassValue;
            #endif
        
            // Graph Functions
            
        void Unity_Subtract_float3(float3 A, float3 B, out float3 Out)
        {
            Out = A - B;
        }
        
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_Add_half4(half4 A, half4 B, out half4 Out)
        {
            Out = A + B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Subtract_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A - B;
        }
        
        void Unity_Clamp_float4(float4 In, float4 Min, float4 Max, out float4 Out)
        {
            Out = clamp(In, Min, Max);
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
            #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
            // Graph Pixel
            struct SurfaceDescription
        {
            float3 BaseColor;
            half Alpha;
            half3 NormalTS;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0 = UnityBuildTexture2DStructNoScale(_BlackPattern);
            float3 _Subtract_2611599512eb4160ae95748584d3e3eb_Out_2;
            Unity_Subtract_float3(IN.WorldSpacePosition, SHADERGRAPH_OBJECT_POSITION, _Subtract_2611599512eb4160ae95748584d3e3eb_Out_2);
            half2 _Property_5e2ca93f020f426ba36d9bfe9f6116ad_Out_0 = _BlackUvs;
            float2 _Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2;
            Unity_Multiply_float2_float2((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_5e2ca93f020f426ba36d9bfe9f6116ad_Out_0, _Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2);
            float4 _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0 = SAMPLE_TEXTURE2D(_Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.tex, _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.samplerstate, _Property_ffa28175efdf4ca6808d5c90ac88b54f_Out_0.GetTransformedUV(_Multiply_95cc7f4762fa417db9f6a50872fa2c29_Out_2));
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_R_4 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.r;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_G_5 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.g;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_B_6 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.b;
            float _SampleTexture2D_37ad7634563149debaa7adec8d280677_A_7 = _SampleTexture2D_37ad7634563149debaa7adec8d280677_RGBA_0.a;
            half4 _Property_bbb3876183cd4b3ab3533dcee97760b3_Out_0 = _BlackColor;
            float4 _Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2;
            Unity_Multiply_float4_float4((_SampleTexture2D_37ad7634563149debaa7adec8d280677_R_4.xxxx), _Property_bbb3876183cd4b3ab3533dcee97760b3_Out_0, _Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2);
            half _Property_b2cded26bcef426692fb0bd56700e838_Out_0 = _Power;
            UnityTexture2D _Property_3d32a97b7ba1453086089eea0de504dc_Out_0 = UnityBuildTexture2DStructNoScale(_WhitePattern);
            half2 _Property_8c713f0da4f149b196eee1adb45a205a_Out_0 = _WhiteUvs;
            float2 _Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2;
            Unity_Multiply_float2_float2((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_8c713f0da4f149b196eee1adb45a205a_Out_0, _Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2);
            float4 _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0 = SAMPLE_TEXTURE2D(_Property_3d32a97b7ba1453086089eea0de504dc_Out_0.tex, _Property_3d32a97b7ba1453086089eea0de504dc_Out_0.samplerstate, _Property_3d32a97b7ba1453086089eea0de504dc_Out_0.GetTransformedUV(_Multiply_b6ed6cd76abc4dd395b88a79a23680dd_Out_2));
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_R_4 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.r;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_G_5 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.g;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_B_6 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.b;
            float _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_A_7 = _SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_RGBA_0.a;
            half4 _Property_69f39d042e324215960957c9552d249e_Out_0 = _WhiteColor;
            float4 _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2;
            Unity_Multiply_float4_float4((_SampleTexture2D_040e9d7559d84b179acaf7375bc27b83_R_4.xxxx), _Property_69f39d042e324215960957c9552d249e_Out_0, _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2);
            float4 _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2;
            Unity_Multiply_float4_float4((_Property_b2cded26bcef426692fb0bd56700e838_Out_0.xxxx), _Multiply_d4d4c5c6af114d91ba666ea87368cb59_Out_2, _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2);
            UnityTexture2D _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            half4 _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0 = SAMPLE_TEXTURE2D(_Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.tex, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.samplerstate, _Property_58be09d875644be0a52bd1db9fb59d0b_Out_0.GetTransformedUV(IN.uv0.xy));
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_R_4 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.r;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_G_5 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.g;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_B_6 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.b;
            half _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7 = _SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0.a;
            half _Property_09403be1effb4969aadd0253b8275131_Out_0 = _BlendOffset;
            half4 _Add_d16de15f3584456f9d5ad00f52ccddad_Out_2;
            Unity_Add_half4(_SampleTexture2D_777bbfc67719494889148207d7e03648_RGBA_0, (_Property_09403be1effb4969aadd0253b8275131_Out_0.xxxx), _Add_d16de15f3584456f9d5ad00f52ccddad_Out_2);
            UnityTexture2D _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0 = UnityBuildTexture2DStructNoScale(_NoiseOverlay);
            half2 _Property_7f72c5aa47d740cd81ae4c01582e613a_Out_0 = _NoiseTiling;
            half _Property_c3a1c3696eb24532937bcc75c247209c_Out_0 = _NoiseOffset;
            float2 _TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3;
            Unity_TilingAndOffset_float((_Subtract_2611599512eb4160ae95748584d3e3eb_Out_2.xy), _Property_7f72c5aa47d740cd81ae4c01582e613a_Out_0, (_Property_c3a1c3696eb24532937bcc75c247209c_Out_0.xx), _TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3);
            float4 _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.tex, _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.samplerstate, _Property_a8f31ab7540b422eb7c2a4314631d27a_Out_0.GetTransformedUV(_TilingAndOffset_042224a3f7524000b6e36ad8d14f8afd_Out_3));
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_R_4 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.r;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_G_5 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.g;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_B_6 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.b;
            float _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_A_7 = _SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_RGBA_0.a;
            half _Property_7edae69dffce42b982787386e591baa0_Out_0 = _NoiseStrength;
            float _Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_2732123b9fee47b7b8a3fef71373c1cf_R_4, _Property_7edae69dffce42b982787386e591baa0_Out_0, _Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2);
            float4 _Subtract_d911dc3580f240208967a10d14db51a0_Out_2;
            Unity_Subtract_float4(_Add_d16de15f3584456f9d5ad00f52ccddad_Out_2, (_Multiply_8f885ce66f4541bc8a9f604c1a9961b3_Out_2.xxxx), _Subtract_d911dc3580f240208967a10d14db51a0_Out_2);
            float4 _Clamp_232882b14617487892fc67d5789a9222_Out_3;
            Unity_Clamp_float4(_Subtract_d911dc3580f240208967a10d14db51a0_Out_2, float4(0, 0, 0, 0), float4(1, 1, 1, 1), _Clamp_232882b14617487892fc67d5789a9222_Out_3);
            float4 _Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3;
            Unity_Lerp_float4(_Multiply_58e476b4540a4af2a2425d865ef5f8b3_Out_2, _Multiply_eb1077f43d8444b4b4d1739caede63d7_Out_2, _Clamp_232882b14617487892fc67d5789a9222_Out_3, _Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3);
            surface.BaseColor = (_Lerp_8a7495e62d234a7591353ab6864b11c0_Out_3.xyz);
            surface.Alpha = _SampleTexture2D_777bbfc67719494889148207d7e03648_A_7;
            surface.NormalTS = IN.TangentSpaceNormal;
            return surface;
        }
        
            // --------------------------------------------------
            // Build Graph Inputs
        
            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
            
        
        
        
            output.TangentSpaceNormal =                         float3(0.0f, 0.0f, 1.0f);
        
        
            output.WorldSpacePosition =                         input.positionWS;
            output.uv0 =                                        input.texCoord0;
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN                output.FaceSign =                                   IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
            return output;
        }
        
            // --------------------------------------------------
            // Main
        
            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteForwardPass.hlsl"
        
            ENDHLSL
        }
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}