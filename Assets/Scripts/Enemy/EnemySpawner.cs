using UnityEngine;
using DG.Tweening;

/// <summary>ファンを生成するクラス </summary>
public class EnemySpawner : MonoBehaviour
{
    #region private SerializeField

    [SerializeField, Tooltip("0=ノーマル 1=壁ファン 2=ボス"), ElementNames(new string[] { "ノーマル", "壁ファン", "ボス" })]
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
    /// <summary>敵を出現させたい秒数 0=7.68f 1=5.76f 2=3.84f 3=1.92f</summary>
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    /// <summary>ボス戦で敵を出現させたい秒数 0=7.6.4f 1=5.4.8f 2=3.2f 3=1.6f</summary>
    float[] _bossTimeInterval = new float[4] { 6.4f, 4.8f, 3.2f, 1.6f };

    /// <summary>生成間隔を決める添え字</summary>
    int _timeIntervalIndex = 0;
    /// <summary>秒数を数える変数</summary>
    float _generateTimer = 0;
    /// <summary>最初または次の敵の発生位置を決める変数  0なら真ん中、1なら右、2なら左</summary>
    int _positionCount = 0;
    /// <summary>ファンを生成するかどうか true=生成する </summary>
    bool _isGenerate = true;

    public bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }

    #endregion
    private void Start()
    {
        _timeIntervalIndex = Random.Range(0, _timeInterval.Length);
        _generateTimer = -_timeInterval[3]; //最初のみ2秒間遅延をさせる
    }

    void Update()
    {
        if (_isGenerate)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _timeInterval[_timeIntervalIndex]) //_timeIntervalを超えるとInstantiateします
            {

                var enemy = Instantiate(_enemyPrefubs[GetEnemyIndex()], _spawnPoints[_positionCount].transform); //シリアライズで設定したオブジェクトの場所に出現します(最初は真ん中の位置に)
                _manager.AddEnemy(enemy);
                enemy.SetUp(_manager.CurrentGameState);

                //イベントを登録
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.StageScroll;
                enemy.GiveDamage += _manager.GetDamage;
                enemy.DisapperEnemies += _manager.RemoveEnemy;

                _timeIntervalIndex = Random.Range(0, _timeInterval.Length);     //次の生成間隔を決める

                _positionCount++;

                if (_positionCount == 3)
                {
                    _positionCount = 0;
                }

                _generateTimer = 0;
            }
        }

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log(_isGenerate);
        }
    }

    /// <summary>生成するファンを決める（添え字を決める）</summary>
    /// <returns>生成するファンの添え字</returns>
    int GetEnemyIndex()
    {
        var index = Random.Range(0, _enemyPrefubs.Length - 1); //Bossを生成させない為に-1する
        return index;
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
