using AYellowpaper.SerializedCollections.Editor;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(Multiplier), true)]
public class MultiplierDrawer : PropertyDrawer
{
    public const float PADDING = 4f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        if (!property.propertyPath.Contains("Array.data["))
        {
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
        }

        SerializedProperty stat = property.FindPropertyRelative("stat");
        SerializedProperty value = property.FindPropertyRelative("value");

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float width = position.width;

        float stat_xpos = position.x;
        float stat_width = width * 0.65f - 2 * PADDING;

        Rect statRect = new Rect(stat_xpos, position.y, stat_width, position.height);

        float value_xpos = position.x + width * 0.65f;
        float value_width = width * 0.35f - 2 * PADDING;

        Rect valueRect = new Rect(value_xpos, position.y, value_width, position.height);

        EditorGUI.PropertyField(statRect, stat, GUIContent.none);
        EditorGUI.PropertyField(valueRect, value, GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
#endif