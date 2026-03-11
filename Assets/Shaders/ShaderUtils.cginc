#ifndef SGS_SHADER_UTILS
#define SGS_SHADER_UTILS

#define TAU 6.283185
#define PI TAU / 2

float DirToAngle(float2 dir)
{
    float angle = atan2(dir.y, dir.x);
    if (angle < 0)
    {
        angle += TAU;
    }
    return angle;
}

float SDF_Circle(float2 uv, float2 center, float radius)
{
    float dist = distance(uv, center) - radius;
    return dist;
}

float SDF_Capsule(float2 uv, float2 rectSize, float radius)
{
    float minX = rectSize.y / 2;
    float maxX = rectSize.x - rectSize.y / 2;
    float2 pointOnLineSeg = float2(clamp(uv.x, minX, maxX), rectSize.y / 2);
    float dist = distance(uv, pointOnLineSeg) - radius;
    return dist;
}

float SDF_Rect(float2 uv, float2 origin, float2 size)
{
    float2 halfSize = size / 2;
    float2 center = origin + halfSize;
    float2 p = abs(uv - center); // normalize to top-right quadrant
    float2 d = max(p - halfSize, 0);
    float dist = length(d);
    return dist;
}

float SDF_RoundRect(float2 uv, float2 origin, float2 size, float4 r)
{
    float2 b = size / 2;
    float2 center = origin + b;
    float2 p = uv - center; // normalize to top-right quadrant
    r.xy = p.x > 0 ? r.xy : r.zw;
    r.x  = p.y > 0 ? r.x : r.y;
    float2 q = abs(p) - b + r.x;
    return min(max(q.x, q.y), 0) + length(max(q, 0)) - r.x;
}

float SDF_Smooth(float distance)
{
    float fw = fwidth(distance);
    float smooth = smoothstep(fw, -fw, distance);
    return smooth;
}

float CircleFill(float2 uv, float2 center, float radius)
{
    float dist = SDF_Circle(uv, center, radius);
    float mask = SDF_Smooth(dist);
    return mask;
}

float CircleLine(float2 uv, float2 center, float inRadius, float outRadius)
{
    float outCircleMask = CircleFill(uv, center, outRadius);
    float inCircleMask = CircleFill(uv, center, inRadius);
    float circleMask = outCircleMask - inCircleMask;
    return circleMask;
}

float CapsuleFill(float2 uv, float2 rectSize, float radius)
{
    float dist = SDF_Capsule(uv, rectSize, radius);
    float mask = SDF_Smooth(dist);
    return mask;
}

float CapsuleLine(float2 uv, float2 rectSize, float inRadius, float outRadius)
{
    float outMask = CapsuleFill(uv, rectSize, outRadius);
    float inMask = CapsuleFill(uv, rectSize, inRadius);
    float mask = outMask - inMask;
    return mask;
}

float RectFill(float2 uv, float2 origin, float2 size)
{
    float dist = SDF_Rect(uv, origin, size);
    float mask = 1 - SDF_Smooth(-dist);
    return mask;
}

float RoundRectFill(float2 uv, float2 origin, float2 size, float4 r)
{
    float dist = SDF_RoundRect(uv, origin, size, r);
    float mask = SDF_Smooth(dist);
    return mask;
}

float CheckeredDotPattern(float2 uv, float cellSize, float radius)
{
    float2 origin = cellSize;

    float2 patternUV1 = uv % (cellSize * 2);
    float patternMask1 = CircleFill(patternUV1, origin, radius);

    float2 patternUV2 = (uv + cellSize) % (cellSize * 2);
    float patternMask2 = CircleFill(patternUV2, origin, radius);

    float patternMask = patternMask1 + patternMask2;
    return patternMask;
}

#endif