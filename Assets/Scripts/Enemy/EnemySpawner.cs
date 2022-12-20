using UnityEngine;
using DG.Tweening;

/// <summary>ファンを生成するクラス </summary>
public class EnemySpawner : MonoBehaviour
{
    #region private SerializeField

    [SerializeField, Tooltip("0=ノーマル 1=壁ファン2 3=壁ファン3 4=ボス"), ElementNames(new string[] { "ノーマル", "壁ファン2", "壁ファン3", "ボス" })]
    EnemyBase[] _enemyPrefubs = default;

    [ElementNames(new string[] { "中央", "右側", "左側" })]
    [SerializeField, Tooltip("0=中央 1=右側 2=左側")]
    Transform[] _spawnPoints = default;

    [SerializeField, Tooltip("ゲームマネジャー")]
    GameManager _manager = default;

    [SerializeField, Tooltip("Bossの生成位置")]
    Transform _bossSpawnPoint = default;

    #endregion

    #region private
    /// <summary>敵の生成順番 </summary>
    EnemyOrderParameter _order => LevelManager.Instance.CurrentLevel.EnemyOrder;
    /// <summary>次に生成する敵の情報 </summary>
    EnemyInfo _nextEnemyInfo = default;

    /// <summary>敵を出現させたい秒数 0=7.68f 1=5.76f 2=3.84f 3=1.92f</summary>
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    /// <summary>ボス戦で敵を出現させたい秒数 0=7.6.4f 1=5.4.8f 2=3.2f 3=1.6f</summary>
    float[] _bossTimeInterval = new float[4] { 6.4f, 4.8f, 3.2f, 1.6f };

    /// <summary>生成順番のどこまで生成したかどうかの添え字 </summary>
    int _infoIndex = 0;
    /// <summary>秒数を数えるタイマー</summary>
    float _generateTimer = 0;
    /// <summary>次生成する敵の生成時間 </summary>
    float _nextGenerateTime = 0;
    /// <summary>最初または次の敵の発生位置を決める変数  0なら真ん中、1なら右、2なら左</summary>
    int _positionCount = 0;
    /// <summary>ファンを生成するかどうか true=生成する </summary>
    bool _isGenerate = true;

    public bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }

    #endregion
    private void Start()
    {
        _nextEnemyInfo = _order.EnemyOrder[_infoIndex];
        _nextGenerateTime = _timeInterval[(int)_nextEnemyInfo._generateInterval];
    }

    void Update()
    {
        if (_isGenerate)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _nextGenerateTime) //_timeIntervalを超えるとInstantiateします
            {

                var enemy = Instantiate(_enemyPrefubs[(int)_nextEnemyInfo._enemyType], _spawnPoints[_positionCount].transform); //シリアライズで設定したオブジェクトの場所に出現します(最初は真ん中の位置に)
                _manager.AddEnemy(enemy);
                enemy.SetUp(_manager.CurrentGameState, _nextEnemyInfo);

                //イベントを登録
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.StageScroll;
                enemy.GiveDamage += _manager.GetDamage;
                enemy.DisapperEnemies += _manager.RemoveEnemy;

                _positionCount++;

                if (_positionCount == _spawnPoints.Length)
                {
                    _positionCount = 0;
                }

                NextEnemyInfoSetup();
                _generateTimer = 0;
            }
        }
    }
    
    /// <summary>次の敵情報を設定する </summary>
    void NextEnemyInfoSetup()
    {
        _infoIndex++;

        if (_order.EnemyOrder.Count <= _infoIndex)  //最後の敵を生成した
        {
            _isGenerate = false;
            return;
        }

        _nextEnemyInfo = _order.EnemyOrder[_infoIndex];
        _nextGenerateTime = _timeInterval[(int)_nextEnemyInfo._generateInterval];  
    }

    /// <summary>ボスを生成する </summary>
    public void SpawnBossEnemy()
    {
        _isGenerate = false;

        var go = Instantiate(_enemyPrefubs[2], _bossSpawnPoint);

        if (go is not BossEnemy)  //キャストできなければ何もしない
        {
            return;
        }

        var boss = ((BossEnemy)go);
        _manager.StartBossMove += boss.MoveStart;
        boss.GameClear += _manager.GameClear;
        _manager.AddEnemy(boss);

        boss.BossSprite.color = new Color(1f, 1f, 1f, 0f);
        boss.BossSprite.DOFade(1f, 2f)
            .SetDelay(5f)
            .OnComplete(() => _isGenerate = true);
    }
}
