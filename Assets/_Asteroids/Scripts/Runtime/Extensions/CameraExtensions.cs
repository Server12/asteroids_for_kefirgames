using UnityEngine;

namespace Asteroids.Runtime.Extensions
{
    public static class CameraExtensions
    {
        public static void RestrictViewPort2DMovement(this Camera camera, Transform transform)
        {
            var viewPortPoint = camera.WorldToViewportPoint(transform.position);
            if (viewPortPoint.y >= 1)
            {
                var bottomPos = camera.ViewportToWorldPoint(new Vector3(viewPortPoint.x, 0, 0));
                transform.position = new Vector3(bottomPos.x, bottomPos.y);
            }
            else if (viewPortPoint.y <= 0)
            {
                var topPos = camera.ViewportToWorldPoint(new Vector3(viewPortPoint.x, 1, 0));
                transform.position = new Vector3(topPos.x, topPos.y);
            }
            else if (viewPortPoint.x <= 0)
            {
                var leftPos = camera.ViewportToWorldPoint(new Vector3(1, viewPortPoint.y, 0));
                transform.position = new Vector3(leftPos.x, leftPos.y);
            }
            else if (viewPortPoint.x >= 1)
            {
                var rightPos = camera.ViewportToWorldPoint(new Vector3(0, viewPortPoint.y, 0));
                transform.position = new Vector3(rightPos.x, rightPos.y);
            }
        }

        public static bool IsOutOfViewPortView(this Camera camera, Transform transform)
        {
            var viewPortPoint = camera.WorldToViewportPoint(transform.position);
            return viewPortPoint.x >= 1 || viewPortPoint.x <= 0 || viewPortPoint.y >= 1 || viewPortPoint.y <= 0;
        }
    }
}