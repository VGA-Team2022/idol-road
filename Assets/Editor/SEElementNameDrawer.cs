using UnityEngine;
using UnityEditor;

/// <summary>SEの配列の各要素名を変更する為クラス </summary>
[CustomPropertyDrawer(typeof(FlickSENames))]
public class SEElementNameDrawer : PropertyDrawer
{
    /// <summary>要素名を変更 </summary>
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