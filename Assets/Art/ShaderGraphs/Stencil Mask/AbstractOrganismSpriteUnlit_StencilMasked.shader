Shader "AbstractOrganismSpriteUnlit_StencilMasked"
{
    Properties
    {
        [IntRange] _StencilRef ("Stencil Reference Value", Range(0,255)) = 0
        [NoScaleOffset]_MainTex("_MainTex", 2D) = "white" {}
        _AbstractAmount("AbstractAmount", Range(0, 1)) = 0
        _Distortion("Distortion", Vector) = (0.1, 0.1,0 ,0)
        _NoiseSpeed("NoiseSpeed", Vector) = (0, 0.05, 0, 0)
        _Edges("Edges", Vector) = (0, 1, 0, 0)
        _PrimColor("PrimColor", Color) = (0.1881374, 1, 0, 0)
        _SecndColor("SecndColor", Color) = (0, 0, 0, 0)
        _PatternTilling("PatternTilling", Vector) = (1, 1, 0, 0)
        _NoiseTilling("NoiseTilling", Vector) = (0.7, 0.7, 0, 0)
        [NoScaleOffset]_Pattern("Pattern", 2D) = "white" {}
        [NoScaleOffset]_Noise("Noise", 2D) = "white" {}
        _PatternOffsetSpeed("PatternOffsetSpeed", Vector) = (0, 0, 0, 0)
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"=""
        }

        Stencil
        {
            Ref [_StencilRef]
            Comp Equal
        }

        Pass
        {
            Name "Sprite Unlit"
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
            #define SHADERPASS SHADERPASS_SPRITEUNLIT
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
             float4 uv0;
             float3 TimeParameters;
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
        float2 _Distortion;
        float2 _Edges;
        float4 _PrimColor;
        float4 _SecndColor;
        float2 _PatternTilling;
        float2 _NoiseTilling;
        float4 _Pattern_TexelSize;
        float4 _Noise_TexelSize;
        float2 _NoiseSpeed;
        float2 _PatternOffsetSpeed;
        float4 _MainTex_TexelSize;
        float _AbstractAmount;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_Pattern);
        SAMPLER(sampler_Pattern);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        
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
            
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Negate_float2(float2 In, out float2 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Lerp_float2(float2 A, float2 B, float2 T, out float2 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_InvertColors_float(float In, float InvertColors, out float Out)
        {
            Out = abs(InvertColors - In);
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
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
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ec2ac6b934f94c74a096020d7fd43c92_Out_0 = _PrimColor;
            UnityTexture2D _Property_9dd76704f9f246d488fb07086073d3a8_Out_0 = UnityBuildTexture2DStructNoScale(_Pattern);
            float4 _UV_c75c1f5c043c4e198acc32f3437f449c_Out_0 = IN.uv0;
            UnityTexture2D _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            float2 _Property_db31dd61303942708f99d545026f742d_Out_0 = _NoiseTilling;
            float2 _Multiply_9ae04b4471df43edb53a25efcab572de_Out_2;
            Unity_Multiply_float2_float2(_Property_db31dd61303942708f99d545026f742d_Out_0, float2(0.5, 0.5), _Multiply_9ae04b4471df43edb53a25efcab572de_Out_2);
            float2 _Property_dbb17ac177e347ea9a447b43984576b0_Out_0 = _NoiseSpeed;
            float2 _Multiply_5134478f79404c57893fb4ca54400c11_Out_2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_dbb17ac177e347ea9a447b43984576b0_Out_0, _Multiply_5134478f79404c57893fb4ca54400c11_Out_2);
            float2 _TilingAndOffset_a5a4a28df0cc453eab5823097b23ceba_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Multiply_9ae04b4471df43edb53a25efcab572de_Out_2, _Multiply_5134478f79404c57893fb4ca54400c11_Out_2, _TilingAndOffset_a5a4a28df0cc453eab5823097b23ceba_Out_3);
            float4 _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.tex, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.samplerstate, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.GetTransformedUV(_TilingAndOffset_a5a4a28df0cc453eab5823097b23ceba_Out_3));
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_R_4 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.r;
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_G_5 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.g;
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_B_6 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.b;
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_A_7 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.a;
            float2 _Property_9da17c475f144718a036e17a79f13db2_Out_0 = _NoiseTilling;
            float2 _Negate_da309cf39ab3496d91057f58ef208641_Out_1;
            Unity_Negate_float2(_Property_dbb17ac177e347ea9a447b43984576b0_Out_0, _Negate_da309cf39ab3496d91057f58ef208641_Out_1);
            float2 _Multiply_f1aafdb8805e4f569d3f2f50ccd3c702_Out_2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Negate_da309cf39ab3496d91057f58ef208641_Out_1, _Multiply_f1aafdb8805e4f569d3f2f50ccd3c702_Out_2);
            float2 _TilingAndOffset_bcad2af07836400d8be697bf7f8671d7_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_9da17c475f144718a036e17a79f13db2_Out_0, _Multiply_f1aafdb8805e4f569d3f2f50ccd3c702_Out_2, _TilingAndOffset_bcad2af07836400d8be697bf7f8671d7_Out_3);
            float4 _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.tex, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.samplerstate, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.GetTransformedUV(_TilingAndOffset_bcad2af07836400d8be697bf7f8671d7_Out_3));
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_R_4 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.r;
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_G_5 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.g;
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_B_6 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.b;
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_A_7 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.a;
            float _Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_R_4, _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_R_4, _Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2);
            float2 _Property_6c2829529ea346ce92da6bd6b87dd044_Out_0 = _Distortion;
            float2 _Lerp_e27cd666eedd459bada2dd36c2ed76f8_Out_3;
            Unity_Lerp_float2((_UV_c75c1f5c043c4e198acc32f3437f449c_Out_0.xy), (_Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2.xx), _Property_6c2829529ea346ce92da6bd6b87dd044_Out_0, _Lerp_e27cd666eedd459bada2dd36c2ed76f8_Out_3);
            float2 _Property_9b9404ab780844de93ee0d7d1aadac81_Out_0 = _PatternTilling;
            float2 _Property_c98edf3193fa43b694bda81bb73fffa5_Out_0 = _PatternOffsetSpeed;
            float2 _Multiply_d6426128825244d19b2869d410266963_Out_2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_c98edf3193fa43b694bda81bb73fffa5_Out_0, _Multiply_d6426128825244d19b2869d410266963_Out_2);
            float2 _TilingAndOffset_4cb05b0c3cac4d4b9b37a5c95c35350a_Out_3;
            Unity_TilingAndOffset_float(_Lerp_e27cd666eedd459bada2dd36c2ed76f8_Out_3, _Property_9b9404ab780844de93ee0d7d1aadac81_Out_0, _Multiply_d6426128825244d19b2869d410266963_Out_2, _TilingAndOffset_4cb05b0c3cac4d4b9b37a5c95c35350a_Out_3);
            float4 _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0 = SAMPLE_TEXTURE2D(_Property_9dd76704f9f246d488fb07086073d3a8_Out_0.tex, _Property_9dd76704f9f246d488fb07086073d3a8_Out_0.samplerstate, _Property_9dd76704f9f246d488fb07086073d3a8_Out_0.GetTransformedUV(_TilingAndOffset_4cb05b0c3cac4d4b9b37a5c95c35350a_Out_3));
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_R_4 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.r;
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_G_5 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.g;
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_B_6 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.b;
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_A_7 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.a;
            float2 _Property_f90a2d481503476f8ac7e867addd0993_Out_0 = _Edges;
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_R_1 = _Property_f90a2d481503476f8ac7e867addd0993_Out_0[0];
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_G_2 = _Property_f90a2d481503476f8ac7e867addd0993_Out_0[1];
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_B_3 = 0;
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_A_4 = 0;
            float _Subtract_5d8010b96dc74d2380367d0a20744c54_Out_2;
            Unity_Subtract_float(_Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2, 0.1, _Subtract_5d8010b96dc74d2380367d0a20744c54_Out_2);
            float _Multiply_ac7e4200f0ac4d888cab0909c33db40f_Out_2;
            Unity_Multiply_float_float(_Subtract_5d8010b96dc74d2380367d0a20744c54_Out_2, 10, _Multiply_ac7e4200f0ac4d888cab0909c33db40f_Out_2);
            float _Smoothstep_03887835e23b48378347274a770a9623_Out_3;
            Unity_Smoothstep_float(_Split_59b039bedbdf41cd97073eb29dfabc6b_R_1, _Split_59b039bedbdf41cd97073eb29dfabc6b_G_2, _Multiply_ac7e4200f0ac4d888cab0909c33db40f_Out_2, _Smoothstep_03887835e23b48378347274a770a9623_Out_3);
            float _Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_R_4, _Smoothstep_03887835e23b48378347274a770a9623_Out_3, _Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2);
            float4 _Multiply_54f1c4c817db4fd69b6c833eaa3f3547_Out_2;
            Unity_Multiply_float4_float4(_Property_ec2ac6b934f94c74a096020d7fd43c92_Out_0, (_Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2.xxxx), _Multiply_54f1c4c817db4fd69b6c833eaa3f3547_Out_2);
            float _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_Out_1;
            float _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_InvertColors = float (1);
            Unity_InvertColors_float(_Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2, _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_InvertColors, _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_Out_1);
            float4 _Property_88670ba85c6e4def91ea0156eb2484f1_Out_0 = _SecndColor;
            float4 _Multiply_ba7b42697e9143649625b34b8b0eba9e_Out_2;
            Unity_Multiply_float4_float4((_InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_Out_1.xxxx), _Property_88670ba85c6e4def91ea0156eb2484f1_Out_0, _Multiply_ba7b42697e9143649625b34b8b0eba9e_Out_2);
            float4 _Add_7e06f9e417d04a68b1faa840960b7cd4_Out_2;
            Unity_Add_float4(_Multiply_54f1c4c817db4fd69b6c833eaa3f3547_Out_2, _Multiply_ba7b42697e9143649625b34b8b0eba9e_Out_2, _Add_7e06f9e417d04a68b1faa840960b7cd4_Out_2);
            UnityTexture2D _Property_d4397eed575b422c8b64bcc34ac157da_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d4397eed575b422c8b64bcc34ac157da_Out_0.tex, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.samplerstate, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.GetTransformedUV(IN.uv0.xy));
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_R_4 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.r;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_G_5 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.g;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_B_6 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.b;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.a;
            float _Property_af6aa5de5b9f44d285fc96cebfa11614_Out_0 = _AbstractAmount;
            float4 _Lerp_475d09499c884f278af066b370c2885f_Out_3;
            Unity_Lerp_float4(_Add_7e06f9e417d04a68b1faa840960b7cd4_Out_2, _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0, (_Property_af6aa5de5b9f44d285fc96cebfa11614_Out_0.xxxx), _Lerp_475d09499c884f278af066b370c2885f_Out_3);
            surface.BaseColor = (_Lerp_475d09499c884f278af066b370c2885f_Out_3.xyz);
            surface.Alpha = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7;
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
            output.TimeParameters =                             _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
        #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteUnlitPass.hlsl"
        
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
        float2 _Distortion;
        float2 _Edges;
        float4 _PrimColor;
        float4 _SecndColor;
        float2 _PatternTilling;
        float2 _NoiseTilling;
        float4 _Pattern_TexelSize;
        float4 _Noise_TexelSize;
        float2 _NoiseSpeed;
        float2 _PatternOffsetSpeed;
        float4 _MainTex_TexelSize;
        float _AbstractAmount;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_Pattern);
        SAMPLER(sampler_Pattern);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        
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
            float3 Position;
            float3 Normal;
            float3 Tangent;
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
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_d4397eed575b422c8b64bcc34ac157da_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d4397eed575b422c8b64bcc34ac157da_Out_0.tex, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.samplerstate, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.GetTransformedUV(IN.uv0.xy));
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_R_4 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.r;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_G_5 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.g;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_B_6 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.b;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.a;
            surface.Alpha = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7;
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
        float2 _Distortion;
        float2 _Edges;
        float4 _PrimColor;
        float4 _SecndColor;
        float2 _PatternTilling;
        float2 _NoiseTilling;
        float4 _Pattern_TexelSize;
        float4 _Noise_TexelSize;
        float2 _NoiseSpeed;
        float2 _PatternOffsetSpeed;
        float4 _MainTex_TexelSize;
        float _AbstractAmount;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_Pattern);
        SAMPLER(sampler_Pattern);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        
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
            float3 Position;
            float3 Normal;
            float3 Tangent;
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
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_d4397eed575b422c8b64bcc34ac157da_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d4397eed575b422c8b64bcc34ac157da_Out_0.tex, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.samplerstate, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.GetTransformedUV(IN.uv0.xy));
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_R_4 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.r;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_G_5 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.g;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_B_6 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.b;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.a;
            surface.Alpha = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7;
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
            Name "Sprite Unlit"
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
             float4 uv0;
             float3 TimeParameters;
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
        float2 _Distortion;
        float2 _Edges;
        float4 _PrimColor;
        float4 _SecndColor;
        float2 _PatternTilling;
        float2 _NoiseTilling;
        float4 _Pattern_TexelSize;
        float4 _Noise_TexelSize;
        float2 _NoiseSpeed;
        float2 _PatternOffsetSpeed;
        float4 _MainTex_TexelSize;
        float _AbstractAmount;
        CBUFFER_END
        
        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_Pattern);
        SAMPLER(sampler_Pattern);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);
        TEXTURE2D(_MainTex);
        SAMPLER(sampler_MainTex);
        
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
            
        void Unity_Multiply_float2_float2(float2 A, float2 B, out float2 Out)
        {
            Out = A * B;
        }
        
        void Unity_TilingAndOffset_float(float2 UV, float2 Tiling, float2 Offset, out float2 Out)
        {
            Out = UV * Tiling + Offset;
        }
        
        void Unity_Negate_float2(float2 In, out float2 Out)
        {
            Out = -1 * In;
        }
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Lerp_float2(float2 A, float2 B, float2 T, out float2 Out)
        {
            Out = lerp(A, B, T);
        }
        
        void Unity_Subtract_float(float A, float B, out float Out)
        {
            Out = A - B;
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        void Unity_Multiply_float4_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A * B;
        }
        
        void Unity_InvertColors_float(float In, float InvertColors, out float Out)
        {
            Out = abs(InvertColors - In);
        }
        
        void Unity_Add_float4(float4 A, float4 B, out float4 Out)
        {
            Out = A + B;
        }
        
        void Unity_Lerp_float4(float4 A, float4 B, float4 T, out float4 Out)
        {
            Out = lerp(A, B, T);
        }
        
            /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
            // Graph Vertex
            struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
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
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_ec2ac6b934f94c74a096020d7fd43c92_Out_0 = _PrimColor;
            UnityTexture2D _Property_9dd76704f9f246d488fb07086073d3a8_Out_0 = UnityBuildTexture2DStructNoScale(_Pattern);
            float4 _UV_c75c1f5c043c4e198acc32f3437f449c_Out_0 = IN.uv0;
            UnityTexture2D _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            float2 _Property_db31dd61303942708f99d545026f742d_Out_0 = _NoiseTilling;
            float2 _Multiply_9ae04b4471df43edb53a25efcab572de_Out_2;
            Unity_Multiply_float2_float2(_Property_db31dd61303942708f99d545026f742d_Out_0, float2(0.5, 0.5), _Multiply_9ae04b4471df43edb53a25efcab572de_Out_2);
            float2 _Property_dbb17ac177e347ea9a447b43984576b0_Out_0 = _NoiseSpeed;
            float2 _Multiply_5134478f79404c57893fb4ca54400c11_Out_2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_dbb17ac177e347ea9a447b43984576b0_Out_0, _Multiply_5134478f79404c57893fb4ca54400c11_Out_2);
            float2 _TilingAndOffset_a5a4a28df0cc453eab5823097b23ceba_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Multiply_9ae04b4471df43edb53a25efcab572de_Out_2, _Multiply_5134478f79404c57893fb4ca54400c11_Out_2, _TilingAndOffset_a5a4a28df0cc453eab5823097b23ceba_Out_3);
            float4 _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.tex, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.samplerstate, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.GetTransformedUV(_TilingAndOffset_a5a4a28df0cc453eab5823097b23ceba_Out_3));
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_R_4 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.r;
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_G_5 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.g;
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_B_6 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.b;
            float _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_A_7 = _SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_RGBA_0.a;
            float2 _Property_9da17c475f144718a036e17a79f13db2_Out_0 = _NoiseTilling;
            float2 _Negate_da309cf39ab3496d91057f58ef208641_Out_1;
            Unity_Negate_float2(_Property_dbb17ac177e347ea9a447b43984576b0_Out_0, _Negate_da309cf39ab3496d91057f58ef208641_Out_1);
            float2 _Multiply_f1aafdb8805e4f569d3f2f50ccd3c702_Out_2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Negate_da309cf39ab3496d91057f58ef208641_Out_1, _Multiply_f1aafdb8805e4f569d3f2f50ccd3c702_Out_2);
            float2 _TilingAndOffset_bcad2af07836400d8be697bf7f8671d7_Out_3;
            Unity_TilingAndOffset_float(IN.uv0.xy, _Property_9da17c475f144718a036e17a79f13db2_Out_0, _Multiply_f1aafdb8805e4f569d3f2f50ccd3c702_Out_2, _TilingAndOffset_bcad2af07836400d8be697bf7f8671d7_Out_3);
            float4 _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.tex, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.samplerstate, _Property_d0ce27f2dd014e4d899c026f1833b6bb_Out_0.GetTransformedUV(_TilingAndOffset_bcad2af07836400d8be697bf7f8671d7_Out_3));
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_R_4 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.r;
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_G_5 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.g;
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_B_6 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.b;
            float _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_A_7 = _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_RGBA_0.a;
            float _Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_fd190303af6c4fedb9da2e945e96a3f5_R_4, _SampleTexture2D_bc611791682a44a58b1998aa7a753d67_R_4, _Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2);
            float2 _Property_6c2829529ea346ce92da6bd6b87dd044_Out_0 = _Distortion;
            float2 _Lerp_e27cd666eedd459bada2dd36c2ed76f8_Out_3;
            Unity_Lerp_float2((_UV_c75c1f5c043c4e198acc32f3437f449c_Out_0.xy), (_Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2.xx), _Property_6c2829529ea346ce92da6bd6b87dd044_Out_0, _Lerp_e27cd666eedd459bada2dd36c2ed76f8_Out_3);
            float2 _Property_9b9404ab780844de93ee0d7d1aadac81_Out_0 = _PatternTilling;
            float2 _Property_c98edf3193fa43b694bda81bb73fffa5_Out_0 = _PatternOffsetSpeed;
            float2 _Multiply_d6426128825244d19b2869d410266963_Out_2;
            Unity_Multiply_float2_float2((IN.TimeParameters.x.xx), _Property_c98edf3193fa43b694bda81bb73fffa5_Out_0, _Multiply_d6426128825244d19b2869d410266963_Out_2);
            float2 _TilingAndOffset_4cb05b0c3cac4d4b9b37a5c95c35350a_Out_3;
            Unity_TilingAndOffset_float(_Lerp_e27cd666eedd459bada2dd36c2ed76f8_Out_3, _Property_9b9404ab780844de93ee0d7d1aadac81_Out_0, _Multiply_d6426128825244d19b2869d410266963_Out_2, _TilingAndOffset_4cb05b0c3cac4d4b9b37a5c95c35350a_Out_3);
            float4 _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0 = SAMPLE_TEXTURE2D(_Property_9dd76704f9f246d488fb07086073d3a8_Out_0.tex, _Property_9dd76704f9f246d488fb07086073d3a8_Out_0.samplerstate, _Property_9dd76704f9f246d488fb07086073d3a8_Out_0.GetTransformedUV(_TilingAndOffset_4cb05b0c3cac4d4b9b37a5c95c35350a_Out_3));
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_R_4 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.r;
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_G_5 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.g;
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_B_6 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.b;
            float _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_A_7 = _SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_RGBA_0.a;
            float2 _Property_f90a2d481503476f8ac7e867addd0993_Out_0 = _Edges;
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_R_1 = _Property_f90a2d481503476f8ac7e867addd0993_Out_0[0];
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_G_2 = _Property_f90a2d481503476f8ac7e867addd0993_Out_0[1];
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_B_3 = 0;
            float _Split_59b039bedbdf41cd97073eb29dfabc6b_A_4 = 0;
            float _Subtract_5d8010b96dc74d2380367d0a20744c54_Out_2;
            Unity_Subtract_float(_Multiply_1a95513c485546c6a00dc5cdb8d07cda_Out_2, 0.1, _Subtract_5d8010b96dc74d2380367d0a20744c54_Out_2);
            float _Multiply_ac7e4200f0ac4d888cab0909c33db40f_Out_2;
            Unity_Multiply_float_float(_Subtract_5d8010b96dc74d2380367d0a20744c54_Out_2, 10, _Multiply_ac7e4200f0ac4d888cab0909c33db40f_Out_2);
            float _Smoothstep_03887835e23b48378347274a770a9623_Out_3;
            Unity_Smoothstep_float(_Split_59b039bedbdf41cd97073eb29dfabc6b_R_1, _Split_59b039bedbdf41cd97073eb29dfabc6b_G_2, _Multiply_ac7e4200f0ac4d888cab0909c33db40f_Out_2, _Smoothstep_03887835e23b48378347274a770a9623_Out_3);
            float _Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2;
            Unity_Multiply_float_float(_SampleTexture2D_3334a95081444ed6a74fc437a8ccb8fa_R_4, _Smoothstep_03887835e23b48378347274a770a9623_Out_3, _Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2);
            float4 _Multiply_54f1c4c817db4fd69b6c833eaa3f3547_Out_2;
            Unity_Multiply_float4_float4(_Property_ec2ac6b934f94c74a096020d7fd43c92_Out_0, (_Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2.xxxx), _Multiply_54f1c4c817db4fd69b6c833eaa3f3547_Out_2);
            float _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_Out_1;
            float _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_InvertColors = float (1);
            Unity_InvertColors_float(_Multiply_bcd9c4e32a87456fa67c1b0de1af78b6_Out_2, _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_InvertColors, _InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_Out_1);
            float4 _Property_88670ba85c6e4def91ea0156eb2484f1_Out_0 = _SecndColor;
            float4 _Multiply_ba7b42697e9143649625b34b8b0eba9e_Out_2;
            Unity_Multiply_float4_float4((_InvertColors_8b12bd173e2a4a6ba9bb51a9613e3aed_Out_1.xxxx), _Property_88670ba85c6e4def91ea0156eb2484f1_Out_0, _Multiply_ba7b42697e9143649625b34b8b0eba9e_Out_2);
            float4 _Add_7e06f9e417d04a68b1faa840960b7cd4_Out_2;
            Unity_Add_float4(_Multiply_54f1c4c817db4fd69b6c833eaa3f3547_Out_2, _Multiply_ba7b42697e9143649625b34b8b0eba9e_Out_2, _Add_7e06f9e417d04a68b1faa840960b7cd4_Out_2);
            UnityTexture2D _Property_d4397eed575b422c8b64bcc34ac157da_Out_0 = UnityBuildTexture2DStructNoScale(_MainTex);
            float4 _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d4397eed575b422c8b64bcc34ac157da_Out_0.tex, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.samplerstate, _Property_d4397eed575b422c8b64bcc34ac157da_Out_0.GetTransformedUV(IN.uv0.xy));
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_R_4 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.r;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_G_5 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.g;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_B_6 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.b;
            float _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7 = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0.a;
            float _Property_af6aa5de5b9f44d285fc96cebfa11614_Out_0 = _AbstractAmount;
            float4 _Lerp_475d09499c884f278af066b370c2885f_Out_3;
            Unity_Lerp_float4(_Add_7e06f9e417d04a68b1faa840960b7cd4_Out_2, _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_RGBA_0, (_Property_af6aa5de5b9f44d285fc96cebfa11614_Out_0.xxxx), _Lerp_475d09499c884f278af066b370c2885f_Out_3);
            surface.BaseColor = (_Lerp_475d09499c884f278af066b370c2885f_Out_3.xyz);
            surface.Alpha = _SampleTexture2D_24e878a4ec874f4782e82c4694df5c53_A_7;
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
            output.TimeParameters =                             _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
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
        #include "Packages/com.unity.render-pipelines.universal/Editor/2D/ShaderGraph/Includes/SpriteUnlitPass.hlsl"
        
            ENDHLSL
        }
    }
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}