using UnityEngine;

/// <summary>インゲームパラメーターの設定を行うクラス </summary>
[CreateAssetMenu]
public class InGameParameter : ScriptableObject
{
    [SerializeField, Header("制限時間")]
    float _gamePlayTime = 0f;

    [SerializeField, Header("ボスステージを開始する時間")]
    float _startBossTime = 0f;

    [SerializeField, Header("プレイヤーの体力")]
    int _playerHp = 0;

    [SerializeField, Header("アイドルパワーの最大値")]
    int _idolPowerMaxValue = 0;

    /// <summary>制限時間</summary>
    public float GamePlayTime => _gamePlayTime;

    /// <summary>ボスステージを開始する時間</summary>
    public float StartBossTime => _startBossTime;

    /// <summary>プレイヤー体力</summary>
    public int PlayerHp => _playerHp;

    /// <summary>アイドルパワーの最大値 </summary>
    public int IdolPowerMaxValue => _idolPowerMaxValue;
}
