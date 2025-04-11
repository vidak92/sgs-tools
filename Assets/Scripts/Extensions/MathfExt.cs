using UnityEngine;

namespace MijanTools.Extensions
{
    public static class MathfExt
    {
        public static readonly float EquilateralTriangleHeight = Mathf.Sqrt(3f) / 2f;
        
        public static bool AreLinesIntersecting(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float x1 = l1p1.x;
            float y1 = l1p1.y;

            float x2 = l1p2.x;
            float y2 = l1p2.y;

            float x3 = l2p1.x;
            float y3 = l2p1.y;

            float x4 = l2p2.x;
            float y4 = l2p2.y;

            float pD = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
            float pxN = (x1 * y2 - x2 * y1) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
            float pyN = (x1 * y2 - x2 * y1) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);

            if (pD != 0f) // Not parallel nor coincident.
            {
                float px = pxN / pD;
                float py = pyN / pD;

                float minX = Mathf.Min(x1, x2);
                float maxX = Mathf.Max(x1, x2);
                float minY = Mathf.Min(y1, y2);
                float maxY = Mathf.Max(y1, y2);

                Vector2 p = new Vector2(px, py);

                float l1Dist = Vector2.Distance(l1p1, l1p2);
                float l2Dist = Vector2.Distance(l2p1, l2p2);

                float p1l1Dist = Vector2.Distance(p, l1p1);
                float p2l1Dist = Vector2.Distance(p, l1p2);

                float p1l2Dist = Vector2.Distance(p, l2p1);
                float p2l2Dist = Vector2.Distance(p, l2p2);

                float epsilon = 0.01f;
                if (
                    (p1l1Dist + p2l1Dist <= l1Dist + epsilon &&
                     p1l1Dist + p2l1Dist >= l1Dist - epsilon)
                    &&
                    (p1l2Dist + p2l2Dist <= l2Dist + epsilon &&
                     p1l2Dist + p2l2Dist >= l2Dist - epsilon)
                )
                {
                    // intersection
                    return true;
                }
            }
            return false;
        }

        public static float DirectionXYToAngle(Vector3 direction)
        {
            return Mathf.Atan2(direction.y, direction.x);
        }
    }
}