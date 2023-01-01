using UnityEngine;

/// <summary>各レベルの情報をまとめるクラス</summary>
[CreateAssetMenu]
public class GameParameter : ScriptableObject
{
    [SerializeField, Header("インゲームパラメーター")]
    InGameParameter _inGame = default;

    [SerializeField, Header("リザルトパラメーター")]
    ResultParameter _result = default;

    [SerializeField, Header("ファンの生成順番")]
    EnemyOrderParameter _enemyOrder = default;

    [SerializeField, Header("アイテムの生成順番")]
    ItemParameter _itemParameter = default;

    [SerializeField, Header("スーパーアイドルタイムのパラメーター")]
    SuperIdolTimeParamater _idolTimeParamater = default;

    [SerializeField, Header("各エネミーのパラメーター"), ElementNames(new string[] { "通常", "壁ファン2", "壁ファン3", "ボス" })]
    EnemyParameter[] _enemyParameters = new EnemyParameter[4];

    [SerializeField, Header("ボスステージでの各敵パラメーター"), ElementNames(new string[] { "通常", "壁ファン2", "壁ファン3" })]
    BossStageParameter[] _bossStageParameters = new BossStageParameter[3];

    /// <summary>インゲームパラメーター </summary>
    public InGameParameter InGame => _inGame;

    /// <summary>リザルトパラメーター </summary>
    public ResultParameter Result => _result;

    /// <summary>ファンの生成順番 </summary>
    public EnemyOrderParameter EnemyOrder => _enemyOrder;

    /// <summary>アイテムの生成順番 </summary>
    public ItemParameter ItemParameter => _itemParameter;

    /// <summary>スーパーアイドルタイムのパラメーター</summary>
    public SuperIdolTimeParamater IdolTimeParamater => _idolTimeParamater;

    /// <summary>各エネミーのパラメーター 0=通常 1=壁ファン2 2=壁ファン3  3=ボス</summary>
    public EnemyParameter[] EnemyParameters => _enemyParameters;

    /// <summary>ボスステージでの各敵パラメーター </summary>
    public BossStageParameter[] BossStageParameter => _bossStageParameters;
}
