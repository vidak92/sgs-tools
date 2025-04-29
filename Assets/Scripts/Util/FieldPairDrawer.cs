#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace MijanTools.Util
{
    public class FieldPairDrawer : PropertyDrawer
    {
        protected virtual string Field1Name { get; }
        protected virtual string Field2Name { get; }
        
        protected virtual float FieldLabelWidth { get; }
        protected virtual string Field1Label { get; }
        protected virtual string Field2Label { get; }
        
        private string Field1DisplayName => string.IsNullOrEmpty(Field1Label) ? Field1Name : Field1Label;
        private string Field2DisplayName => string.IsNullOrEmpty(Field2Label) ? Field2Name : Field2Label;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var labelWidth = EditorGUIUtility.labelWidth;
            var labelRect = new Rect(position.x, position.y, labelWidth, position.height);
            
            var fieldWidth = (position.width - labelWidth) / 2f;
            var field1Rect = new Rect(position.x + labelWidth, position.y, fieldWidth, position.height);
            var field2Rect = new Rect(position.x + labelWidth + fieldWidth, position.y, fieldWidth, position.height);

            var minFieldProperty = property.FindPropertyRelative(Field1Name/*.ToBackingFieldName()*/);
            var maxFieldProperty = property.FindPropertyRelative(Field2Name/*.ToBackingFieldName()*/);

            EditorGUI.LabelField(labelRect, label);
            
            EditorGUIUtility.labelWidth = FieldLabelWidth;
            EditorGUI.PropertyField(field1Rect, minFieldProperty, new GUIContent(Field1DisplayName));
            EditorGUI.PropertyField(field2Rect, maxFieldProperty, new GUIContent(Field2DisplayName));
            EditorGUIUtility.labelWidth = labelWidth;
            
            EditorGUI.EndProperty();
        }
    }
}
#endif