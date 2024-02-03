using System;
using UnityEngine;

namespace Asteroids.Runtime.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        public float colliderRadius = 1f;
        public Transform cachedTransform;

    }
}