using UnityEngine;

namespace MijanTools.Extensions
{
    public static class RigidbodyExt
    {
        public static void ClampVelocityMagnitude(this Rigidbody2D rigidbody, float magnitude)
        {
            rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, magnitude);
        }

        public static void ApplyQuadraticDrag(this Rigidbody2D rigidbody, float drag, float clampMagnitudeMultiplier = 2f)
        {
            var velocity = rigidbody.velocity;
            var dragForce = -drag * velocity.normalized * velocity.sqrMagnitude;
            dragForce = Vector2.ClampMagnitude(dragForce, velocity.magnitude * clampMagnitudeMultiplier);
            rigidbody.AddForce(dragForce, ForceMode2D.Force);
        }
    }
}