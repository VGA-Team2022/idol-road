using UnityEngine;

/// <summary>�e������SE�̗v�f����ۑ�����N���X </summary>
public class FlickSENames : PropertyAttribute
{
    public readonly string[] _names;
    public FlickSENames(string[] names)
    {
        _names = names;
    }
}
