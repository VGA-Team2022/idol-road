using UnityEngine;

/// <summary>インスペクターで表示する配列やリストの要素名を保持するクラス </summary>
public class ElementNames : PropertyAttribute
{
    /// <summary>表示したい名前の配列 </summary>
    public readonly string[] _names;
    public ElementNames(string[] names) { _names = names; }
}