Shader "SGS/QuadShape"
{
    Properties
    {
        
    }
    SubShader
    {
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            // shape data
            // x: type (0 - circle, 1 - rect)
            // y: radius (for circle) or width (for rect)
            // z: height (for rect)
            
            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 shapeData: TEXCOORD1;
                float4 color : TEXCOORD2;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 shapeData: TEXCOORD1;
                float4 color : TEXCOORD2;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.shapeData = v.shapeData;
                o.color = v.color;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                const float shapeType = i.shapeData.x;
                if (shapeType == 0)
                {
                    // circle
                    float dis = length(i.uv - 0.5);
                    float width = i.shapeData.y;
                    float mask = dis > (0.5 - width) && dis < 0.5;
                    
                    float clipEpsilon = 0.1;
                    clip(mask - clipEpsilon);

                    float4 col = mask * i.color;
                    return col;
                }
                else if (shapeType == 1)
                {
                    // rect
                    float2 dist = i.uv;
                    float2 width = i.shapeData.yz;
                    float mask = dist.x < width.x || dist.x > 1 - width.x || dist.y < width.y || dist.y > 1 - width.y;

                    float clipEpsilon = 0.1;
                    clip(mask - clipEpsilon);

                    float4 col = mask * i.color;
                    return col;
                }
                else
                {
                    // line
                    return i.color;
                }
            }
            ENDCG
        }
    }
}
