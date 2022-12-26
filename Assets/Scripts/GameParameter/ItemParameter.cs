using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemParameter : ScriptableObject
{
    [SerializeField, Header("�`�F�b�N������΃����_���ŃA�C�e�����o��")]
    bool _randomGenerator = false;

    [Tooltip("�l��������΍����قǏo�����܂�")]
    [SerializeField, Header("�����_���̏ꍇ�̊e�A�C�e���̏o���m��"), ElementNames(new string[] { "�ʂ������", "�ԑ�", "�v���[���g", "����" }), Range(0f, 10f)]
    float[] _itemWeights = default;

    [SerializeField, Header("��������A�C�e��")]
    List<ItemInfo> _items = default;

    /// <summary>true�Ȃ烉���_���ŃA�C�e�����o��</summary>
    public bool RandomGenerator => _randomGenerator;

    /// <summary>�e�A�C�e���̏o���m��</summary>
    public float[] ItemWeights => _itemWeights;

    /// <summary>��������</summary>
    public List<ItemInfo> GeneratorItems => _items;
}

/// <summary>��������A�C�e���̏���ێ�����N���X</summary>
[Serializable]
public class ItemInfo
{
    [SerializeField, Header("�A�C�e���𐶐�����܂ł̎���"), Range(1f, 10f)]
    float _generatorInterval;

    [SerializeField, Header("��������A�C�e��")]
    ItemType _item = default;

    [SerializeField, Header("���E�ǂ���ɐ������邩")]
    GenerateDirection _generateDirection = default;
}

/// <summary>�A�C�e���̎�� </summary>
public enum ItemType
{
    /// <summary>�ʂ������</summary>
    Toy = 0,
    /// <summary>�ԑ�</summary>
    Bouquet = 1,
    /// <summary>�v���[���g</summary>
    Present,
    /// <summary>����</summary>
    Money
}

/// <summary>���E�ǂ���ɐ������邩</summary>
public enum GenerateDirection
{
    /// <summary>�E�ɐ��� </summary>
    Right = 0,
    /// <summary>���ɐ��� </summary>
    Left = 1,
    /// <summary>���E�����_���ɐ��� </summary>
    Random = 2
}
