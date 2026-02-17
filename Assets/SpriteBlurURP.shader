Shader "Custom/SimpleBlurSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurSize ("Blur Size", Range(0,0.02)) = 0.005
        _Alpha ("Alpha", Range(0,1)) = 0.7
    }

    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "RenderPipeline"="UniversalPipeline" }

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes { float4 positionOS : POSITION; float2 uv : TEXCOORD0; };
            struct Varyings { float4 positionHCS : SV_POSITION; float2 uv : TEXCOORD0; };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            float _BlurSize;
            float _Alpha;

            Varyings vert(Attributes v)
            {
                Varyings o;
                o.positionHCS = TransformObjectToHClip(v.positionOS.xyz);
                o.uv = v.uv;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                float2 uv = i.uv;
                half4 col = 0;

                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv);
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(_BlurSize,0));
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv - float2(_BlurSize,0));
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv + float2(0,_BlurSize));
                col += SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, uv - float2(0,_BlurSize));

                col /= 5;
                col.a = _Alpha;

                return col;
            }
            ENDHLSL
        }
    }
}
