using System;
using UnityEngine;

namespace _Project.Runtime.Views
{
    public abstract class BaseView : MonoBehaviour
    {
        public float colliderRadius = 1f;
        public Transform cachedTransform;

    }
}