using Asteroids.Runtime.Views;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Asteroids.Editor
{
    [CustomEditor(typeof(BaseView), true)]
    public class BaseViewEditor : UnityEditor.Editor
    {
        private BaseView _view;

        private void OnEnable()
        {
            _view = target as BaseView; }

        public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);
            return root;
        }


        private void OnSceneGUI()
        {
            if (_view == null) return;
            Handles.color = Color.green;
            Handles.DrawWireDisc(_view.transform.position, Vector3.forward, _view.colliderRadius);
        }
    }
}