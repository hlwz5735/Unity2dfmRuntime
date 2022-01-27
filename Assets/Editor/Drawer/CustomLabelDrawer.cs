using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawer
{
    [CustomPropertyDrawer(typeof(CustomLabelAttribute))]
    public class CustomLabelDrawer : PropertyDrawer
    {
        private GUIContent label = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent lbl)
        {
            if (label == null)
            {
                string name = (attribute as CustomLabelAttribute).Name;
                label = new GUIContent(name);
            }

            EditorGUI.PropertyField(position, property, label);
        }
    }
}
