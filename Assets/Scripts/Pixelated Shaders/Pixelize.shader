  Shader "Hidden/Pixelize"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white"
    }
    SubShader
{
Tags
            {
               "RenderType"="Opaque" "RenderPipeline" = "UniversalPipline"
            }
            
            HLSLINCLUDE
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipeline.universal/ShaderLibary/Core.hlsl"


struct Attributes
{
    float4 positionOS : POSITION;
    float2 uv : TEXCOORD0;
};

struct Varyings
{
    float4 positionHCS : SV_POSITION;
    float2 uv : TEXTCOORD0;
};

            
            TEXTURE2D(_MainTex);
            float4 _MainTex_TexelSize;
            float4 _MainTex_ST;
          
             //SAMPLER(sampler_MainTex);
            //Texture2D _MainTex;
            //samplerState sampler
            
            SamplerState sampler_point_clamp;
            
            uniform float2 _BlockCount;
            uniform float2 _BLockSize;
            uniform float2 _HalfBlockSize;

}


            

Varyings vert (Attributes IN)
{
    Varyings OUT;
    OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
    return OUT;
}

}
