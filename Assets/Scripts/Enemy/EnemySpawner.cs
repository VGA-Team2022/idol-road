using System;
using UnityEngine;
using DG.Tweening;

/// <summary>ファンを生成するクラス </summary>
public class EnemySpawner : MonoBehaviour
{
    //TODO:EnemySpawnerがどこかで生成されているどこで生成されているか見つける
    static EnemySpawner _instance = default; //一時的にシングルトンパターンを使用しているが本来は使わない

    /// <summary>生成を辞める為の変数 </summary>
    const int LAST_ENEMY_GENERATED = 1;
    /// <summary>ボスを移動させる処理を登録する為変数 </summary>
    const int BOSS_MOVE_REGISTER = 2;
    /// <summary>左側の生成位置の添え字 </summary>
    const int LEFT_SPAWN_POINT = 2;



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

    BossEnemy _boss = default;

    /// <summary>敵を出現させたい秒数 0=7.68f 1=5.76f 2=3.84f 3=1.92f</summary>
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    /// <summary>ボス戦で敵を出現させたい秒数 0=7.6.4f 1=5.4.8f 2=3.2f 3=1.6f</summary>
    float[] _bossTimeInterval = new float[4] { 6.72f, 4.8f, 2.88f, 0.96f };

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
    /// <summary>ボスを移動させる処理を登録する</summary>
    bool _isRegister = false;

    public bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                GenerateEnemy(_positionCount);

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

    /// <summary>敵を生成し、初期化を行う</summary>
    void GenerateEnemy(int spawnPointNumber)
    {
        if (_nextEnemyInfo._enemyType == EnemyType.Wait) { return; }　//待機であれば何もしない

        var enemy = Instantiate(_enemyPrefubs[(int)_nextEnemyInfo._enemyType], _spawnPoints[_positionCount].transform);
        _manager.AddEnemy(enemy);
        enemy.Setup(_manager.CurrentGameState, _nextEnemyInfo);

        //イベントを登録
        enemy.AddComboCount += _manager.ComboAmountTotal;
        enemy.StageScroll += _manager.StageScroll;
        enemy.GiveDamage += _manager.GetDamage;
        enemy.DisapperEnemies += _manager.RemoveEnemy;
        enemy.AddIdolPower += _manager.IncreseIdlePower;
        enemy.EnterSuperIdolTime += _manager.EnemyCheckIdolPower;

        if (spawnPointNumber == LEFT_SPAWN_POINT)      //左側だったらイラストを反転する
        {
            enemy.ReverseEnemySprite(_manager.CurrentGameState);
        }

        if (_isRegister)
        {
            enemy.BossMove += BossMove;
        }
    }

    /// <summary>次の敵情報を設定する </summary>
    void NextEnemyInfoSetup()
    {
        _infoIndex++;


        if (_order.EnemyOrder.Count - LAST_ENEMY_GENERATED <= _infoIndex)  //最後の敵が生成されたら生成を辞める
        {
            _isGenerate = false;
           // return;
        }
        else if (_order.EnemyOrder.Count - BOSS_MOVE_REGISTER <= _infoIndex)  //最後の敵にボスを移動させる処理を登録する為
        {
            _isRegister = true;
        }

        _nextEnemyInfo = _order.EnemyOrder[_infoIndex];

        if (_manager.CurrentGameState is BossTime)
        {
            _nextGenerateTime = _bossTimeInterval[(int)_nextEnemyInfo._generateInterval];
            Debug.Log(_nextGenerateTime);
        }
        else
        {
            _nextGenerateTime = _timeInterval[(int)_nextEnemyInfo._generateInterval];
        }
    }

    /// <summary>ボスを生成する </summary>
    public void SpawnBossEnemy()
    {
        _generateTimer = 0f;

        var go = Instantiate(_enemyPrefubs[3], _bossSpawnPoint);

        if (go is not BossEnemy) { return; }    //キャストできなければ何もしない

        _boss = ((BossEnemy)go);

        _boss.AddComboCount += _manager.ComboAmountTotal;
        _boss.GiveDamage += _manager.GetDamage;
        _boss.GameClear += _manager.GameClear;

        _boss.BossSprite.color = new Color(1f, 1f, 1f, 0f);

        _boss.BossSprite.DOFade(1f, 2f)
            .SetDelay(5f);
    }

    /// <summary>ボスを移動させる処理</summary>
    void BossMove()
    {
        _manager.AddEnemy(_boss);
        _boss.Setup(_manager.CurrentGameState, _nextEnemyInfo);
        _isGenerate = false;
        _generateTimer = 0f;
        _boss.StartMoveAnim();
    }
}
