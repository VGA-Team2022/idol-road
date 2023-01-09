using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ComboParameter : ScriptableObject
{
    [SerializeField�@, Header("�R���{���ɂ���ĕ\������C���X�g")]
    List<ComboInfo> _comboInfos = default;

    public List<ComboInfo> ComboInfos => _comboInfos;
}

/// <summary>�R���{���ɂ���ĕ\������C���X�g</summary>
[Serializable]
public class ComboInfo
{
    [SerializeField, Header("�C���X�g��ύX����R���{��")]
    public int _nextCombo;

    [SerializeField, Header("�\������C���X�g")]
    public Sprite _comboSprites = default;
}