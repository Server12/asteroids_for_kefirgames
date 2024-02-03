using _Project.Runtime.Controllers.Physics.Data;
using UnityEngine;

namespace _Project.Runtime.Controllers.Physics
{
    public enum CollisionCheckType
    {
        ByRadius,
        ByLineIntersection
    }

    public struct CollisionCheck
    {
        private readonly CollisionCheckType CollisionCheckType;
        public readonly bool CollideOnce;

        public CollisionCheck(CollisionCheckType collisionCheckType,bool collideOnce = true)
        {
            CollisionCheckType = collisionCheckType;
            CurrentPosition = Vector3.positiveInfinity;
            EndPosition = Vector3.positiveInfinity;
            CollideOnce = collideOnce;
            ColliderRadius = 0;
        }
        
        public Vector3 CurrentPosition;

        public Vector3 EndPosition;

        public float ColliderRadius;

        public bool IsCollided(CollisionCheck otherCollision)
        {
            var radius = ColliderRadius + otherCollision.ColliderRadius;
            if (otherCollision.CollisionCheckType == CollisionCheckType.ByRadius &&
                CollisionCheckType == CollisionCheckType.ByRadius)
            {
                return (otherCollision.CurrentPosition - CurrentPosition).sqrMagnitude < radius * radius;
            }


            if (otherCollision.CollisionCheckType == CollisionCheckType.ByLineIntersection)
            {
                return IsIntersectLine(otherCollision.CurrentPosition, otherCollision.EndPosition, radius,
                    CurrentPosition);
            }

            return IsIntersectLine(CurrentPosition, EndPosition, radius,
                otherCollision.CurrentPosition);
        }


        private bool IsIntersectLine(Vector3 start, Vector3 end, float checkRadius, Vector3 checkPos)
        {
            Vector3 d = end - start;
            Vector3 f = start - new Vector3(checkPos.x, checkPos.y, start.z);

            var a = Vector3.Dot(d, d);
            var b = 2 * Vector3.Dot(f, d);
            var c = Vector3.Dot(f, f) - checkRadius * checkRadius;

            float discriminant = b * b - 4 * a * c;

            if (discriminant < 0) return false;
            
            return  discriminant >= 0 && discriminant * discriminant >= 4 * a * c;
            ;
        }
    }
}