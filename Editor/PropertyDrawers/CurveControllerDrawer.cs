using Tactile.Core.Editor.Utility;
using Tactile.Core.Editor.Utility.PropertyShelves;
using Tactile.Core.Extensions;
using Tactile.Core.Utility;
using UnityEditor;
using UnityEngine;

namespace Tactile.Animations.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(CurveController))]
    public class CurveControllerDrawer : ShelfPropertyDrawer
    {
        private static readonly string TPropertyFieldName = "t";
        private static readonly string CurveFieldName = "curve";
        private static readonly string EventFieldName = "onNewValue";
        private readonly FoldoutShelf _foldoutShelf;

        public CurveControllerDrawer()
        {
            _foldoutShelf = new FoldoutShelf(new CurveControllerShelf(this), new PropertyShelf(EventFieldName));
        }

        private class CurveControllerShelf : IShelf
        {
            private readonly CurveControllerDrawer _drawer;
            public CurveControllerShelf(CurveControllerDrawer drawer)
            {
                _drawer = drawer;
            }
            
            public void Render(Rect rect, SerializedProperty property, GUIContent label)
            {
                var target = (CurveController)property.GetTargetObjectOfProperty();
            
                var tProperty = property.FindPropertyRelative(TPropertyFieldName);
                var curveProperty = property.FindPropertyRelative(CurveFieldName);
                var rects = rect.HorizontalLayout(
                    RectLayout.Flex(),
                    RectLayout.Flex(4f));

                var curveRect = rects[0];
                var sliderRect = rects[1];

                EditorGUI.BeginChangeCheck();
                var newCurve = EditorGUI.CurveField(curveRect, curveProperty.animationCurveValue);
                var newT = EditorGUI.Slider(sliderRect, tProperty.floatValue, 0f, 1f);
                
                // Update properties
                if (!EditorGUI.EndChangeCheck()) return;
                curveProperty.animationCurveValue = newCurve;
                tProperty.floatValue = newT;
                property.serializedObject.ApplyModifiedProperties();
                target.OnNewValue.Invoke(target.T);
            }

            public float GetHeight(SerializedProperty property, GUIContent label)
            {
                return EditorGUIUtility.singleLineHeight;
            }
        }

        protected override IShelf Shelf => _foldoutShelf;
    }
}