Shader "Custom/URP_Pearlescent4ColorStereo"
{
    Properties
    {
        _Color1 ("Color 1 (Facing)", Color) = (1, 0, 0, 1)
        _Color2 ("Color 2", Color) = (0, 1, 0, 1)
        _Color3 ("Color 3", Color) = (0, 0, 1, 1)
        _Color4 ("Color 4 (Edge)", Color) = (1, 1, 0, 1)

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Iterations ("Iridescence Power", Range(1,20)) = 5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "RenderPipeline"="UniversalPipeline" }
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing
            #pragma multi_compile _ STEREO_INSTANCING_ON
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS   : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float3 normalWS    : TEXCOORD0;
                float3 viewDirWS   : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            float4 _Color1;
            float4 _Color2;
            float4 _Color3;
            float4 _Color4;

            float _Glossiness;
            float _Metallic;
            float _Iterations;

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                UNITY_SETUP_INSTANCE_ID(IN);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

                float3 positionWS = TransformObjectToWorld(IN.positionOS).xyz;
                OUT.positionHCS = TransformWorldToHClip(positionWS);
                OUT.normalWS = TransformObjectToWorldNormal(IN.normalOS);
                OUT.viewDirWS = normalize(_WorldSpaceCameraPos - positionWS);
                return OUT;
            }

            float iridFunc(float x)
            {
                x = saturate(x);
                return pow(x, _Iterations);
            }

            float4 frag (Varyings IN) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

                float NdotV = dot(normalize(IN.normalWS), normalize(IN.viewDirWS));
                float t = iridFunc(NdotV);

                float4 c1 = _Color1;
                float4 c2 = _Color2;
                float4 c3 = _Color3;
                float4 c4 = _Color4;

                // Optional: Debug-Farben je Auge
                /*
                if (unity_StereoEyeIndex == 0)
                    return float4(1, 0, 0, 1); // rot - linkes Auge
                else
                    return float4(0, 0, 1, 1); // blau - rechtes Auge
                */

                float4 col;
                if (t < 0.33)
                {
                    float localT = saturate(t / 0.33);
                    col = lerp(c4, c3, localT);
                }
                else if (t < 0.66)
                {
                    float localT = saturate((t - 0.33) / 0.33);
                    col = lerp(c3, c2, localT);
                }
                else
                {
                    float localT = saturate((t - 0.66) / 0.34);
                    col = lerp(c2, c1, localT);
                }

                return col;
            }
            ENDHLSL
        }
    }
}
