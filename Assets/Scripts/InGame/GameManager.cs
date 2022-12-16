using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>インゲームを管理するクラス ステートパターン使用</summary>
public class GameManager : MonoBehaviour
{
    /// <summary>イラストが変わるタイミング</summary>
    const int ADD_COMBO_ILLUST_CCHANGE = 5;

    /// <summary>スタート状態 </summary>
    public static GameStart _startState => new GameStart();
    /// <summary>プレイ状態 </summary>
    public static Playing _playingState => new Playing();
    /// <summary>アイドルタイム状態 </summary>
    public static IdleTime _idleTimeState => new IdleTime();
    /// <summary>ボス状態状態 </summary>
    public static BossTime _bossTimeState => new BossTime();
    /// <summary>ゲーム終了状態 </summary>
    public static GameEnd _gameEndState => new GameEnd();

    [SerializeField, Header("Maxアイドルパワー")]
    int _maxIdlePower = 100;
    [SerializeField, Header("アイドルのMaxHp")]
    int _maxIdleHp = 5;
    [SerializeField, Header("制限時間")]
    float _gameTime = 60;
    [SerializeField, Header("ボスステージが始まる時間")]
    float _bossTime = 30;
    [SerializeField, Header("スクロールさせるオブジェクト")]
    StageScroller _stageScroller = default;
    [SerializeField, Tooltip("フェードを行うクラス")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("InGameUIの更新を行うクラス")]
    InGameUIController _uiController = default;
    [SerializeField, Tooltip("敵を生成するクラス")]
    EnemySpawner _enemySpawner = default;
    [SerializeField, Tooltip("Warningプレハブ")]
    PlayableDirector _warningTape = default;
    /// <summary>現在対象の敵 </summary>
    EnemyBase _currentEnemy = default;
    /// <summary>現在のゲーム状態</summary>
    IState _currentGameState = null;
    /// <summary>ステージに表示されている敵のリスト</summary>
    List<EnemyBase> _enemies = new List<EnemyBase>();
    /// <summary>現在の体力 </summary>
    int _idleHp;
    /// <summary>現在のアイドルパワー </summary>
    int _idlePower;
    /// <summary>コンボを数える変数 </summary>
    int _comboAmount;
    /// <summary>次にコンボイラストを表示するカウント</summary>
    int _nextComboCount = ADD_COMBO_ILLUST_CCHANGE;
    /// <summary>ゲーム開始からの経過時間 </summary>
    float _elapsedTime = 0f;
    /// <summary>ボスがプレイヤーに向かって移動しているかどうか </summary>
    bool _isBossMove = false;

    bool _isBoss = false;
    /// <summary>ボスの移動を開始する処理</summary>
    event Action _startBossMove = default;

#region
    /// <summary>現在のゲーム状態 </summary>
    public IState CurrentGameState { get => _currentGameState; }
    /// <summary>現在対象の敵</summary>
    public EnemyBase CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }
    /// <summary>スクロールさせるオブジェクト</summary>
    public StageScroller Scroller { get => _stageScroller; }
    /// <summary>敵を生成するクラス</summary>
    public EnemySpawner EnemyGenerator { get => _enemySpawner; }
    /// <summary>フェードを行うクラス</summary>
    public FadeController FadeCanvas { get => _fadeController; }
    public PlayableDirector WarningTape { get => _warningTape; }

    /// <summary>ボスの移動を開始する処理</summary>
    public event Action StartBossMove
    {
        add { _startBossMove += value; }
        remove { _startBossMove -= value; }
    }

#endregion

    void Start()
    {
        _idleHp = _maxIdleHp;
        _currentGameState = _startState;
        _currentGameState.OnEnter(this, null);
        _uiController.InitializeInGameUI(_maxIdleHp, _gameTime, _maxIdlePower); //UIの初期化処理
    }

    void Update()
    {
        if (_currentGameState is not GameStart && _currentGameState is not GameEnd) //時間経過のUIを更新する
        {
            _elapsedTime += Time.deltaTime;
            _uiController.UpdateGoalDistanceUI(_elapsedTime);
        }

        if (_bossTime >= Math.Abs(_gameTime - _elapsedTime) && _currentGameState is not BossTime　&& _currentGameState is not GameEnd)    //ボスステージを開始
        {
            ChangeGameState(_bossTimeState);
        }

        if (_bossTime + 7 >= Math.Abs(_gameTime - _elapsedTime) && !_isBoss)
        {
            _enemySpawner.IsGenerate = false;
            _isBoss = true;
        }

        if (_gameTime - _elapsedTime <= 20 && !_isBossMove)    //制限時間が0になったらボスを移動させる
        {
            Debug.Log("移動開始");
            _enemySpawner.IsGenerate = false;
            _startBossMove?.Invoke();
            _isBossMove = true;
        }
    }

    /// <summary>アイドルパワーが増加する関数</summary>
    public void IncreseIdlePower(int power)
    {
        _idlePower += power;
        _uiController.UpdateIdolPowerGauge(_idlePower);

        if (_maxIdlePower <= _idlePower)    //スーパーアイドルタイムを発動
        {
            _idlePower = 0;
            _uiController.UpdateIdolPowerGauge(_idlePower);
            ChangeGameState(_idleTimeState);
        }
    }

    /// <summary>コンボ数を管理する関数</summary>
    public void ComboAmountTotal(TimingResult result)
    {
        if (result == TimingResult.Out || result == TimingResult.Bad)
        {
            _comboAmount = 0;
        }
        else
        {
            _comboAmount++;
        }

        _uiController.UpdateComboText(_comboAmount);    //UIを更新

        if (_comboAmount == _nextComboCount)    //コンボイラストを表示
        {
            _uiController.PlayComboAnimation();
            _nextComboCount += ADD_COMBO_ILLUST_CCHANGE;
        }
    }

    /// <summary>体力を減らす </summary>
    /// <param name="damage">HPが減る量</param>
    public void GetDamage(int damage)
    {
        for (var i = 0; i < damage; i++)    //HPUIを減らす為に回す
        {
            _idleHp -= 1;

            if (0 <= _idleHp)
            {
                _uiController.UpdateHpUI(_idleHp); //HPUIを更新
            }

            if (_idleHp <= 0)   //ゲームオーバー
            {
                ChangeGameState(_gameEndState); //ゲーム終了状態に遷移
                return;
            }
        }
    }

    /// <summary>ゲーム状態を切り替える </summary>
    /// <param name="nextState">次の状態</param>
    public void ChangeGameState(IState nextState)
    {
        _currentGameState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentGameState);
        _currentGameState = nextState;
    }

    /// <summary>Enemy.csに登録するステージスクロール処理</summary>
    public void StageScroll()
    {
        if (_enemies.Count <= 0)    //ファンがいなくなったらスクロールを開始する
        {
            _stageScroller.ScrollOperation();
        }
    }

    /// <summary>生成されたファンをリストに追加する </summary>
    /// <param name="enemy">追加するファン</param>
    public void AddEnemy(EnemyBase enemy)
    {
        if (_enemies.Count <= 0)    //ステージにファンが出現
        {
            _currentEnemy = enemy;
            _stageScroller.ScrollOperation();
        }

        _enemies.Add(enemy);    
    }

    /// <summary>倒されたファンをリストから削除する</summary>
    /// <param name="enemy">削除するファン</param>
    public void RemoveEnemy(EnemyBase enemy)
    {
        _enemies.Remove(enemy);

        if (1 <= _enemies.Count)
        {
            _currentEnemy = _enemies[0];
        }
    }

    /// <summary>ゲームクリア時の処理</summary>
    public void GameClear()
    {
        ChangeGameState(_gameEndState);
        _stageScroller.ScrollOperation();
    }
}