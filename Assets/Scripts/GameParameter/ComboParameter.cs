using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ComboParameter : ScriptableObject
{
    [SerializeField　, Header("コンボ数によって表示するイラスト")]
    List<ComboInfo> _comboInfos = default;

    public List<ComboInfo> ComboInfos => _comboInfos;
}

/// <summary>コンボ数によって表示するイラスト</summary>
[Serializable]
public class ComboInfo
{
    [SerializeField, Header("イラストを変更するコンボ数")]
    public int _nextCombo;

    [SerializeField, Header("表示するイラスト")]
    public Sprite _comboSprites = default;
}