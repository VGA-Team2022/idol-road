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
    int _addDamageValueBoss = 0;

    [SerializeField, Header("向かってくる速度")]
    float _moveSpeedBoss = 0f;

    [SerializeField, Header("透明になるまでの速度")]
    float _fadeSpeedBoss = 0f;

    [SerializeField, Header("獲得出来るアイドルパワー量")]
    int _addIdolPowerValueBoss = 0;

    [SerializeField, Header("リズム判定の秒数"), ElementNames(new string[] { "合計時間", "Bad", "Good", "Perfect", "Out" })]
    float[] _rhythmTimesBoss = new float[5];

    /// <summary>向かってくる速度</summary>
    public float MoveSpeed => _moveSpeed;

    /// <summary>透明になるまでの速度</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>リズム判定の秒数 0=合計時間 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;
   
    /// <summary>与えるダメージ </summary>
    public int AddDamageValue => _addDamageValue;
   
    /// <summary>獲得できるアイドルパワー量</summary>
    public int AddIdolPowerValue => _addIdolPowerValue;


    //========== 以下 ボスステージ用パラメータープロパティ ===========

    /// <summary>ダメージ量 </summary>
    public int AddDamageValueBoss => _addDamageValueBoss;

    /// <summary>移動速度 </summary>
    public float MoveSpeedBoss => _moveSpeedBoss;

    /// <summary>フェード速度 </summary>
    public float FadeSpeedBoss => _fadeSpeedBoss;

    /// <summary>リズム判定</summary>
    public float[] RhythmTimesBoss => _rhythmTimesBoss;

    /// <summary>アイドルパワーの獲得量 </summary>
    public int AddIdolPowerValueBoss => _addIdolPowerValueBoss;


}
