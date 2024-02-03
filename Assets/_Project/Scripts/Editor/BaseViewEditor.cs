using System;
using _Project.Runtime.Views;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.Scripts.Editor
{
    [UnityEditor.CustomEditor(typeof(BaseView), true)]
    public class BaseViewEditor : UnityEditor.Editor
    {
        private LaserView _laserView;
        private BaseView _view;

        private void OnEnable()
        {
            _view = target as BaseView;
            _laserView = _view as LaserView;
        }

        public override UnityEngine.UIElements.VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            InspectorElement.FillDefaultInspector(root, serializedObject, this);

            // if (_laserView != null)
            // {
            //     var btn = new Button(() =>
            //     {
            //         var start = _laserView.transform.TransformPoint(_laserView.LineRenderer.GetPosition(0));
            //         var endPos = _laserView.transform.TransformPoint(_laserView.LineRenderer.GetPosition(1));
            //
            //         var diameter = _laserView.colliderRadius * 2;
            //         var dist = Vector3.Distance(start, endPos);
            //         var totalPoints = Mathf.RoundToInt(dist / (diameter+_laserView.positionOffset * Vector3.Distance(start,endPos)));
            //         var dir = (endPos - start).normalized;
            //
            //
            //         _laserView.positions = new Vector3[totalPoints];
            //         for (int i = 0; i < totalPoints; i++)
            //         {
            //             var pos = _laserView.transform.TransformPoint(start + dir * (i * diameter));
            //             _laserView.positions[i] = start + dir * (i * (diameter+_laserView.positionOffset));
            //         }
            //     });
            //     btn.text = "generate points";
            //     root.Add(btn);
            // }

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