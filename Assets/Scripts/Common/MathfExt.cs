using UnityEngine;

namespace SGSTools.Common
{
    public static class MathfExt
    {
        public const float TAU = 6.283185307179586f;
        
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
        
        /// <returns>angle in radians</returns>
        public static float VectorXYToAngle(Vector3 direction)
        {
            return Mathf.Atan2(direction.y, direction.x);
        }
        
        /// <returns>angle in radians</returns>
        public static float VectorXYToAngle(float x, float y)
        {
            return Mathf.Atan2(y, x);
        }

        /// <param name="angle">in radians</param>
        public static Vector3 AngleToVectorXY(float angle, float radius) // TODO rename to AngleToVectorXY
        {
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            return new Vector3(x, y, 0f);
        }

        /// <returns>wrapped `value` such that it's always between 0 and max (exclusive)</returns>
        public static int GetWrappedValue(int value, int maxExclusive)
        {
            if (maxExclusive < 0)
            {
                // TODO assert?
            }
            
            if (value >= maxExclusive)
            {
                value %= maxExclusive;
            }
            else
            {
                // TODO find a way to do this without a loop
                while (value < 0)
                {
                    value += maxExclusive;    
                }
            }
            return value;
        }
    }
}