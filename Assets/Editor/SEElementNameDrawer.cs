using UnityEngine;
using UnityEditor;

/// <summary>SE�̔z��̊e�v�f����ύX����׃N���X </summary>
[CustomPropertyDrawer(typeof(FlickSENames))]
public class SEElementNameDrawer : PropertyDrawer
{
    /// <summary>�v�f����ύX </summary>
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((FlickSENames)attribute)._names[pos]));

        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label);
        }
    }
}