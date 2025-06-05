using UnityEngine;

namespace MijanTools.Extensions
{
    public static class RigidbodyExt
    {
        public static void ClampVelocityMagnitude(this Rigidbody2D rigidbody, float magnitude)
        {
            #if UNITY_6000_0_OR_NEWER
            rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, magnitude);
            #else
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, magnitude);
            #endif
        }

        public static void ApplyQuadraticDrag(this Rigidbody2D rigidbody, float drag, float clampMagnitudeMultiplier = 2f)
        {
            #if UNITY_6000_0_OR_NEWER
            var velocity = rigidbody.linearVelocity;
            #else
            var velocity = rigidbody.velocity;
            #endif
            var dragForce = -drag * velocity.normalized * velocity.sqrMagnitude;
            dragForce = Vector2.ClampMagnitude(dragForce, velocity.magnitude * clampMagnitudeMultiplier);
            rigidbody.AddForce(dragForce, ForceMode2D.Force);
        }
    }
}