using UnityEngine;

namespace MijanTools.Extensions
{
    public static class RigidbodyExt
    {
        public static void ClampVelocityMagnitude(this Rigidbody2D rigidbody, float magnitude)
        {
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, magnitude);
        }
    }
}