using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ファンの生成順番を設定するクラス</summary>
[CreateAssetMenu]
public class EnemyOrderParameter : ScriptableObject
{
    [SerializeField, Header("生成順番")]
    List<EnemyInfo> _enemyOrder = new List<EnemyInfo>();

    /// <summary>生成順番</summary>
    public List<EnemyInfo> EnemyOrder { get => _enemyOrder; }
}

/// <summary>生成する敵の情報を保持するクラス</summary>
[Serializable]
public class EnemyInfo
{
    [SerializeField, Header("敵の種類")]
    public EnemyType _enemyType = EnemyType.Nomal;
    [SerializeField, Header("生成間隔"), Tooltip("1=7.68秒, 2=5.76秒, 3=3.84秒, 4=1.92秒")]
    public GenerateInterval _generateInterval = GenerateInterval.Interval1;
    [SerializeField, Header("ファンサ要求")]
    public List<RequestType> requestTypes = new List<RequestType>();
}

/// <summary>ファンの種類 </summary>
public enum EnemyType
{
    /// <summary>通常 </summary>
    Nomal = 0,
    /// <summary>壁ファン ファンサ数 2 </summary>
    Wall2 = 1,
    /// <summary>壁ファン ファンサ数 3 </summary>
    Wall3 = 2,
    /// <summary>ボス </summary>
    Boss = 3,
    /// <summary>敵なし</summary>
    Wait = 4,
}

/// <summary>要求の種類 </summary>
public enum RequestType
{
    Random = 0,

    Pose = 1,
    Kiss = 2,
    Wink = 3,
    Sign = 4,
}

/// <summary>生成間隔</summary>
public enum GenerateInterval
{
    /// <summary>7.68f </summary>
    Interval1 = 0,
    /// <summary>5.76f </summary>
    Interval2 = 1,
    /// <summary>3.84f </summary>
    Interval3 = 2,
    /// <summary>1.92f </summary>
    Interval4 = 3,
}


