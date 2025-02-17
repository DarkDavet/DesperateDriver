Shader "Unlit/TransparentShader"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Frequency ("Frequency", Range(0, 10)) = 1.0
        _Amplitude ("Amplitude", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags {"RenderType" = "Transparent"}
        LOD 200

        Pass 
        {
            Blend SrcAlpha OneMinusSrcAlpha
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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _Color;
            float _Frequency;
            float _Amplitude;

            v2f vert (appdata_t v)
            {
                v2f output;
                output.pos = UnityObjectToClipPos(v.vertex);
                output.uv = v.uv;
                return output;
            }

            half4 frag (v2f i) : SV_Target
            {
                float alpha = _Color.a * (0.5 + 0.5 * sin(_Time.y * _Frequency) * _Amplitude);
                return float4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}

