using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemParameter : ScriptableObject
{
    [SerializeField, Header("チェックを入れればランダムでアイテムが出る")]
    bool _randomGenerator = false;

    [Tooltip("値が高ければ高いほど出現します")]
    [SerializeField, Header("ランダムの場合の各アイテムの出現確率"), ElementNames(new string[] { "ぬいぐるみ", "花束", "プレゼント", "お金" }), Range(0f, 10f)]
    float[] _itemWeights = default;

    [SerializeField, Header("生成するアイテム")]
    List<ItemInfo> _items = default;

    /// <summary>trueならランダムでアイテムが出る</summary>
    public bool RandomGenerator => _randomGenerator;

    /// <summary>各アイテムの出現確率</summary>
    public float[] ItemWeights => _itemWeights;

    /// <summary>生成順番</summary>
    public List<ItemInfo> GeneratorItems => _items;
}

/// <summary>生成するアイテムの情報を保持するクラス</summary>
[Serializable]
public class ItemInfo
{
    [SerializeField, Header("アイテムを生成するまでの時間"), Range(1f, 10f)]
    float _generatorInterval;

    [SerializeField, Header("生成するアイテム")]
    ItemType _item = default;

    [SerializeField, Header("左右どちらに生成するか")]
    GenerateDirection _generateDirection = default;
}

/// <summary>アイテムの種類 </summary>
public enum ItemType
{
    /// <summary>ぬいぐるみ</summary>
    Toy = 0,
    /// <summary>花束</summary>
    Bouquet = 1,
    /// <summary>プレゼント</summary>
    Present,
    /// <summary>お金</summary>
    Money
}

/// <summary>左右どちらに生成するか</summary>
public enum GenerateDirection
{
    /// <summary>右に生成 </summary>
    Right = 0,
    /// <summary>左に生成 </summary>
    Left = 1,
    /// <summary>左右ランダムに生成 </summary>
    Random = 2
}
