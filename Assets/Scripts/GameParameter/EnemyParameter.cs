using UnityEngine;

/// <summary>エネミーのパラメーターを設定するクラス </summary>
[CreateAssetMenu]
public class EnemyParameter : ScriptableObject
{
    [SerializeField, Header("----------------------------通常時-------------------------------"),Header("与えるダメージ")]
    int _addDamageValue = 0;

    [SerializeField, Header("向かってくる速度")]
    float _moveSpeed = 0f;

    [SerializeField, Header("透明になるまでの速度")]
    float _fadeSpeed = 0f;

    [SerializeField, Header("獲得出来るアイドルパワー量")]
    int _addIdolPowerValue = 0;

    [SerializeField, Header("リズム判定の秒数"), ElementNames(new string[] {"合計時間", "Bad", "Good", "Perfect", "Out"})]
    float[] _rhythmTimes = new float[5];
    
    [SerializeField, Header("----------------------------ボス戦の時----------------------------"), Header("与えるダメージ")]
    int _addDamageValueToBossBattle = 0;

    [SerializeField, Header("向かってくる速度")]
    float _moveSpeedToBossBattle = 0f;

    [SerializeField, Header("透明になるまでの速度")]
    float _fadeSpeedToBossBattle = 0f;

    [SerializeField, Header("獲得出来るアイドルパワー量")]
    int _addIdolPowerValueToBossBattle = 0;

    [SerializeField, Header("リズム判定の秒数"), ElementNames(new string[] { "合計時間", "Bad", "Good", "Perfect", "Out" })]
    float[] _rhythmTimesToBossBattle = new float[5];
    
    /// <summary>向かってくる速度</summary>
    public float MoveSpped => _moveSpeed;

    /// <summary>ボス戦のときの向かってくる速度</summary>
    public float MoveSpeedToBossBattle => _moveSpeedToBossBattle;

    /// <summary>透明になるまでの速度</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>ボス戦のときの透明になるまでの速度</summary>
    public float FadeSpeedToBossBattle => _fadeSpeedToBossBattle;

    /// <summary>リズム判定の秒数 0=合計時間 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;

    /// <summary>ボス戦時のリズム判定の秒数 0=合計時間 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimesToBossBattle => _rhythmTimesToBossBattle;

    /// <summary>与えるダメージ </summary>
    public int AddDamageValue => _addDamageValue;

    /// <summary>ボス戦のときの与えるダメージ</summary>
    public int AddDamageValueToBossBattle => _addDamageValueToBossBattle;
    /// <summary>獲得できるアイドルパワー量</summary>
    public int AddIdolPowerValue => _addIdolPowerValue;
    /// <summary>ボス戦のときの獲得出来るアイドルパワー量 </summary>
    public int AddIdolPowerValueToBossBattle => _addIdolPowerValueToBossBattle;
}
