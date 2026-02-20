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

float SDF_Circle(float2 uv, float2 origin, float radius)
{
    float dist = distance(uv, origin) - radius;
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

float SDF_Smooth(float distance)
{
    float fw = fwidth(distance);
    float smooth = smoothstep(fw, -fw, distance);
    return smooth;
}

float RoundRectFill(float2 uv, float2 origin, float2 size, float cornerRadius)
{
    // TODO use actual SDF
    float left = origin.x;
    float right = origin.x + size.x;
    float bottom = origin.y;
    float top = origin.y + size.y;
    bool isInBounds = uv.x > left && uv.x < right && uv.y > bottom && uv.y < top;

    float inLeft = left + cornerRadius;
    float inRight = right - cornerRadius;
    float inBottom = bottom + cornerRadius;
    float inTop = top - cornerRadius;

    float2 inTL = float2(inLeft, inTop);
    float2 inTR = float2(inRight, inTop);
    float2 inBL = float2(inLeft, inBottom);
    float2 inBR = float2(inRight, inBottom);

    float distTL = distance(uv, inTL);
    float distTR = distance(uv, inTR);
    float distBL = distance(uv, inBL);
    float distBR = distance(uv, inBR);

    bool isCornerTL = distTL > cornerRadius && uv.x < inLeft && uv.y > inTop;
    bool isCornerTR = distTR > cornerRadius && uv.x > inRight && uv.y > inTop;
    bool isCornerBL = distBL > cornerRadius && uv.x < inLeft && uv.y < inBottom;
    bool isCornerBR = distBR > cornerRadius && uv.x > inRight && uv.y < inBottom;

    bool isCorner = isCornerTL || isCornerTR || isCornerBL || isCornerBR;
    return isInBounds && !isCorner;
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