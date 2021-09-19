Shader "Unlit/tex"
{
    Properties
    {
        _Background ("background", Color) = (1, 1, 1, 1)
        _Brush ("Brush", Color) = (0, 0, 0, 0)

        _Brfore ("Brfore", 2D) = "white" {}

        X ("X", float) = 0.5
        Y ("Y", float) = 0.5
        R ("R", float) = 0.2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _Brfore;
            float4 _Brfore_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _Brfore);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 _Background;
            float4 _Brush;
            float X;
            float Y;
            float R;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_Brfore, i.uv);

                if (pow(R, 2) >= pow(i.uv.x * 1000 - X * 1000, 2) + pow(i.uv.y * 666 - Y * 666, 2)) col = _Brush;
                if (col.a == 0) col = _Background;
                
                return col;
            }
            ENDCG
        }
    }
}
