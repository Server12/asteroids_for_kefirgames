using UnityEngine;

namespace _Project.Runtime.Views
{
    public class LaserView : BaseView
    {
        public LineRenderer LineRenderer;
        public float positionOffset;
        public Vector3[] positions;
    }
}