using UnityEngine;

/// <summary>ファンを生成するクラス </summary>
public class EnemySpawner : MonoBehaviour
{
#region private SerializeField

    [SerializeField, Tooltip("プレハブ")]
    Enemy _enemyPrefub = default;
    [SerializeField, Tooltip("スポーン地点")]
     GameObject _positionObject = default;  // 1つのポジションにEnemyをスポーン
    [SerializeField, Tooltip("ゲームマネジャー")]
    GameManager _manager = default;

#endregion

#region private

    [Tooltip("敵を出現させたい秒数")]
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    [Tooltip("ボス戦で敵を出現させたい秒数")]
    float[] _bossTimeInterval = new float[4] { 6.4f, 4.8f, 3.2f, 1.6f };

    /// <summary>生成間隔を決める添え字</summary>
    int _timeIntervalIndex = 0;
    /// <summary>秒数を数える変数</summary>
    float _generateTimer = 0;

#endregion
    private void Start()
    {
        _timeIntervalIndex = Random.Range(0, _timeInterval.Length);
        _generateTimer = -_timeInterval[3]; //最初のみ2秒間遅延をさせる
    }

    void Update()
    {
        if (_manager.CurrentGameState is Playing || _manager.CurrentGameState is BossTime)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _timeInterval[_timeIntervalIndex]) //_timeIntervalを超えるとInstantiateします
            {
                var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //シリアライズで設定したオブジェクトの場所に出現します

                _manager.Enemies.Add(enemy);

                enemy.SetUp(_manager.CurrentGameState);

                //イベントを登録
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.Scroller.ScrollOperation;
                enemy.GiveDamage += _manager.GetDamage;
                enemy.DisapperEnemies += _manager.RemoveEnemy;


                _manager.CurrentEnemy = enemy;
                _manager.Scroller.ScrollOperation();    //ステージスクロールを止める
                _manager.Enemies.Add(enemy);
                _timeIntervalIndex = Random.Range(0, _timeInterval.Length);     //次の生成間隔を決める
                _generateTimer = 0;
            }
        }
    }

    /// <summary>
    /// ボタンに設定するパブリック関数
    /// falseになるとエネミーの出現が止まる
    /// 且つゲームの時間やエネミーが出てくるインターバルの時間をすべてリセット
    /// </summary>
    public void OffEnemy()
    {
        _generateTimer = 0;
    }
}
