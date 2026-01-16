#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace SGSTools.Util
{
    public class SortingLayerAttribute : PropertyAttribute {}

    [CustomPropertyDrawer(typeof(SortingLayerAttribute))]
    public class SortingLayerPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var layers = SortingLayer.layers;
            var layerNames = new string[layers.Length];
            var currentIndex = 0;

            for (int i = 0; i < layers.Length; i++)
            {
                layerNames[i] = layers[i].name;
                if (layers[i].id == property.intValue)
                {
                    currentIndex = i;
                }
            }

            EditorGUI.BeginProperty(position, label, property);
            var newIndex = EditorGUI.Popup(position, label.text, currentIndex, layerNames);
            property.intValue = layers[newIndex].id;
            EditorGUI.EndProperty();
        }
    }
}
#endif