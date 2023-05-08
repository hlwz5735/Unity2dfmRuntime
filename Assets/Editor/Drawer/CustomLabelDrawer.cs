using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawer
{
    [CustomPropertyDrawer(typeof(CustomLabelAttribute))]
    public class CustomLabelDrawer : PropertyDrawer
    {
        private GUIContent _label = null;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent lbl)
        {
            if (_label == null)
            {
                string name = (attribute as CustomLabelAttribute)?.name;
                _label = new GUIContent(name);
            }

            EditorGUI.PropertyField(position, property, _label);
        }
    }
}
