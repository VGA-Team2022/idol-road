using UnityEngine;

/// <summary>エネミーのパラメーターを設定するクラス </summary>
[CreateAssetMenu]
public class EnemyParameter : ScriptableObject
{
    [SerializeField, Header("与えるダメージ")]
    int _addDamageValue = 0;

    [SerializeField, Header("向かってくる速度")]
    float _moveSpeed = 0f;

    [SerializeField, Header("透明になるまでの速度")]
    float _fadeSpeed = 0f;

    [SerializeField, Header("リズム判定の秒数"), ElementNames(new string[] {"合計時間", "Bad", "Good", "Perfect", "Out"})]
    float[] _rhythmTimes = new float[5];

    /// <summary>向かってくる速度</summary>
    public float MoveSpped => _moveSpeed;

    /// <summary>透明になるまでの速度</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>リズム判定の秒数 0=合計時間 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;

    /// <summary>与えるダメージ </summary>
    public int AddDamageValue => _addDamageValue;
}
