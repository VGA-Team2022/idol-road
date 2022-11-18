using UnityEngine;

/// <summary>各方向のSEの要素名を保存するクラス </summary>
public class FlickSENames : PropertyAttribute
{
    public readonly string[] _names;
    public FlickSENames(string[] names)
    {
        _names = names;
    }
}
