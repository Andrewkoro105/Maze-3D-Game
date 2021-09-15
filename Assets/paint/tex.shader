Shader "Custom/tex"
{
    Properties
    {
        _Background ("_Background", Color) = (0, 0, 0, 0)
        _ColorBrush ("ColorBrush", Color) = (0, 0, 0, 0)
        _MainTex ("Texture", 2D) = "white" {}
        r ("R", Float) = 0.01
        x ("X", Float) = 0.5
        y ("Y", Float) = 0.5
    }
    SubShader
    {
        Lighting Off
        Blend One Zero

        Pass
        {
        CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
            #pragma target 3.0

            
            sampler2D _MainTex;
            fixed4 _ColorBrush;
            fixed4 _Background;
            float x;
            float y;
            float r;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                fixed4 A = tex2D(_SelfTexture2D, IN.localTexcoord.xy);

                if (pow(r, 2) >= pow(IN.localTexcoord.x * 1000 - x*1000, 2) + pow(IN.localTexcoord.y*666 - y*666, 2)) A = _ColorBrush;
                if(A.a == 0) A = _Background;
                return A;
            }

        ENDCG
        }
    }
}