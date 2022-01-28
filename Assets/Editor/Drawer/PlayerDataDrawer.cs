using Data;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawer
{
    [CustomPropertyDrawer(typeof(PlayerData))]
    public class PlayerDataDrawer : PropertyDrawer
    {
        private const int lineHeight = 22;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var name = property.FindPropertyRelative("Name");
            var scriptCount = property.FindPropertyRelative("ScriptCount");
            var scriptItemCount = property.FindPropertyRelative("ScriptItemCount");
            var spriteFrameCount = property.FindPropertyRelative("SpriteFrameCount");
            var soundCount = property.FindPropertyRelative("SoundCount");
            var originalBytes = property.FindPropertyRelative("OriginalBytes");

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PrefixLabel(position, new GUIContent("玩家信息"));
            
            EditorGUI.BeginDisabledGroup(true);

            int x = 30;
            int y = (int)(lineHeight + position.y);
            
            EditorGUI.PropertyField(getFieldRect(x, y, position.width), name);
            y += lineHeight;
            EditorGUI.PropertyField(getFieldRect(x, y, position.width), scriptCount);
            y += lineHeight;
            EditorGUI.PropertyField(getFieldRect(x, y, position.width), scriptItemCount);
            y += lineHeight;
            EditorGUI.PropertyField(getFieldRect(x, y, position.width), spriteFrameCount);
            y += lineHeight;
            EditorGUI.PropertyField(getFieldRect(x, y, position.width), soundCount);
            y += lineHeight;
            EditorGUI.PropertyField(getFieldRect(x, y, position.width), originalBytes);
            
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

        private Rect getFieldRect(float x, float y, float totalWidth, int height = 18)
        {
            return new Rect(x, y, totalWidth - x, height);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return lineHeight * 10;
        }
    }
}