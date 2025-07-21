Shader "SGS/VignetteShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistanceMultiplier ("Distance Multiplier", Range(0, 5)) = 1.5
        _DistancePower ("Distance Power", Range(1, 5)) = 3
        _VignettePower ("VignettePower", Range(0.0, 10)) = 0.5
        _Color ("Color", Color) = (0, 0, 0, 1)
        _IsAdditive ("Is Additive", Range(0, 1)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always
        
//        Blend One One

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float _DistanceMultiplier;
            float _DistancePower;
            float _VignettePower;
            float4 _Color;
            float _IsAdditive;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float4 renderTex = tex2D(_MainTex, i.uv);
                float2 dist = (i.uv - 0.5) * _DistanceMultiplier;
                for (int j = 1; j <= _DistancePower; j++)
                {
                    dist *= dist;
                }
                dist.x = dot(dist, dist) * _VignettePower;

                float isAdditive = step(0.5, _IsAdditive);
                if (isAdditive)
                {
                    renderTex += dist.x * _Color;
                }
                else
                {
                    renderTex *= 1 - dist.x;
                }
                return renderTex;
            }
            ENDCG
        }
    }
}
