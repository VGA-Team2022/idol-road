using UnityEngine;
using UnityEditor;

// <summary>インスペクターで表示される配列やリストの要素名を変更する為クラス </summary>
[CustomPropertyDrawer(typeof(ElementNames))]
public class ElementNamesDrawer : PropertyDrawer
{
    //描画処理
    public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
    {
        try
        {
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(rect, property, new GUIContent(((ElementNames)attribute)._names[pos]));

        }
        catch
        {
            EditorGUI.PropertyField(rect, property, label);
        }
    }
}