  Shader"Hidden/Pixelize"
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


    }
}
