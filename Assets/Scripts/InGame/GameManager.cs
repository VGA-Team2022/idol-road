using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>インゲームを管理するクラス ステートパターン使用</summary>
public class GameManager : MonoBehaviour
{
    #region
    /// <summary>スタート状態 </summary>
    public static GameStart _startState => new GameStart();
    /// <summary>プレイ状態 </summary>
    public static Playing _playingState => new Playing();
    /// <summary>アイドルタイム状態 </summary>
    public static IdolTime _idleTimeState => new IdolTime();
    /// <summary>ボス状態状態 </summary>
    public BossTime _bossTimeState => new BossTime();
    /// <summary>ゲーム終了状態 </summary>
    public static GameEnd _gameEndState => new GameEnd();

    [SerializeField, Header("デバッグモード"), Tooltip("デバッグ時はチェックを入れて下さい")]
    bool _debugMode = false;
    [SerializeField, Header("現在の難易度"), Tooltip("選択されている難易度が適用されます")]
    GameLevel _currentLevel = GameLevel.Nomal;
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
    [SerializeField, Tooltip("タクシーオブジェクト")]
    SpriteRenderer _taxi = default;
    [SerializeField, Tooltip("プレイヤー")]
    PlayerMotion _player = default;
    [SerializeField, Tooltip("スーパーアイドルタイムの処理を行うクラス")]
    SuperIdolTime _superIdolTime = default;
    [SerializeField, Tooltip("アイテムジェネレーター")]
    ItemGenerator _itemGenerator = default;

    InGameParameter _inGameParameter => LevelManager.Instance.CurrentLevel.InGame;
    /// <summary>コンボによって変えるイラスト</summary>
    List<ComboInfo> _comboInfos => LevelManager.Instance.CurrentLevel.ComboParameter.ComboInfos;
    /// <summary>現在対象の敵 </summary>
    EnemyBase _currentEnemy = default;
    /// <summary>現在のゲーム状態</summary>
    IState _currentGameState = null;
    /// <summary>ステージに表示されている敵のリスト</summary>
    List<EnemyBase> _enemies = new List<EnemyBase>();
    /// <summary>現在の体力 </summary>
    int _idleHp;
    /// <summary>現在のアイドルパワー </summary>
    float _idlePower;
    /// <summary>コンボを数える変数 </summary>
    int _comboAmount;
    /// <summary>次にコンボイラストを表示するカウント</summary>
    int _nextComboCount = -1;
    /// <summary>次のコンボで表示するイラストの要素</summary>
    int _comboIndex = 0;
    /// <summary>ゲーム開始からの経過時間 </summary>
    float _elapsedTime = 0f;
    /// <summary>時間が経過しているか否か</summary>
    bool _isElapsing = true;
    /// <summary>死亡したかどうか </summary>
    bool _isdead = false;
    /// <summaryスーパーアイドルタイムを行ったかどうか</summary>
    bool _isPlayedIdolTime = false;
    /// <summary>ボスの移動を開始する処理</summary>
    event Action _startBossMove = default;

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
    public SpriteRenderer Taxi { get => _taxi; }
    public SuperIdolTime IdolTime { get => _superIdolTime; }
    public InGameUIController UIController { get => _uiController; }

    /// <summary>ボスの移動を開始する処理</summary>
    public event Action StartBossMove
    {
        add { _startBossMove += value; }
        remove { _startBossMove -= value; }
    }

    #endregion

    private void Awake()
    {
        if (_debugMode)
        {
            LevelManager.Instance.SelectLevel(_currentLevel);
        }
    }

    void Start()
    {
        PlayResult.Instance.ResetResult();
        _idleHp = _inGameParameter.PlayerHp;
        _currentGameState = _startState;
        _currentGameState.OnEnter(this, null);
        _uiController.InitializeInGameUI(_inGameParameter.PlayerHp, _inGameParameter.GamePlayTime, _inGameParameter.IdolPowerMaxValue); //UIの初期化処理
        Application.targetFrameRate = 60; //FPSを60に設定

        if (0 < _comboInfos.Count)
        {
            _nextComboCount = _comboInfos[0]._nextCombo;
        }
    }

    void Update()
    {
        if (_currentGameState is not GameStart && _currentGameState is not GameEnd && _isElapsing) //時間経過のUIを更新する
        {
            _elapsedTime += Time.deltaTime;
            _uiController.UpdateGoalDistanceUI(_elapsedTime);
        }

        if (_inGameParameter.StartBossTime >= Math.Abs(_inGameParameter.GamePlayTime - _elapsedTime) && _currentGameState is Playing)    //ボスステージを開始
        {
            ChangeGameState(_bossTimeState);
            _itemGenerator.IsGenerate = false;
            _uiController.DisactiveIdolPowerGauge();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(_enemies.Count);
        }
    }

    /// <summary>BGMを止める処理</summary>
    IEnumerator StopBGM()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.StopBGM(10);
        AudioManager.Instance.StopBGM(15);
    }

    /// <summary>BGM処理を実行する </summary>
    public void RunStopBGM()
    {
        StartCoroutine(StopBGM());
    }

    /// <summary>アイドルパワーが増加する関数</summary>
    public void IncreseIdlePower(float power)
    {
        //ゲームステートが通常かつアイドルタイムを一度もしていないなら
        if (_currentGameState is Playing && !_isPlayedIdolTime)
        {
            _idlePower += (power / _inGameParameter.IdolPowerMaxValue);
            _uiController.UpdateIdolPowerGauge(_idlePower);
        }
        if (_enemies.Count <= 0)
        {
            EnterSuperIdolTime();
        }
    }
    /// <summary>スーパーアイドルタイムを発動する関数</summary>
    public void EnemyCheckIdolPower()
    {
        EnterSuperIdolTime();
    }

    public void EnterSuperIdolTime()
    {
        if (_idlePower >= 1 && !BossTime.IsPlaying && _currentGameState is not BossTime && !_isPlayedIdolTime)
        {
            _idlePower = 0;
            _isPlayedIdolTime = true;
            _itemGenerator.IsGenerate = false;
            _fadeController.FadeOut(() =>
            {
                _superIdolTime.gameObject.SetActive(true);
                _superIdolTime.CurrentState = _currentGameState;
                ChangeGameState(_idleTimeState);
                _fadeController.FadeIn(() =>
                {
                    _uiController.UpdateIdolPowerGauge(0);
                    _uiController.DisactiveIdolPowerGauge();
                });
            });
        }
    }

    /// <summary>コンボ数を管理する関数</summary>
    public void ComboAmountTotal(TimingResult result)
    {
        if (result == TimingResult.Out || result == TimingResult.Bad)
        {
            _comboAmount = 0;
            _comboIndex = 0;

            if (0 < _nextComboCount)
            {
                _nextComboCount = _comboInfos[0]._nextCombo;
            }
        }
        else
        {
            _comboAmount++;
        }

        _uiController.UpdateComboText(_comboAmount);    //UIを更新


        if(_nextComboCount < 0) { return; }

        if (_comboAmount == _nextComboCount)    //コンボイラストを表示
        {
            _uiController.PlayComboAnimation(_comboInfos[_comboIndex]._comboSprites);
            _comboIndex++;
            if (_nextComboCount > _comboIndex) 
            {
                _nextComboCount = _comboInfos[_comboIndex]._nextCombo;
            }  
        }
    }

    /// <summary>体力を減らす </summary>
    /// <param name="damage">HPが減る量</param>
    public void GetDamage(int damage)
    {
        for (var i = 0; i < damage; i++)    //HPUIを減らす為に回す
        {
            _idleHp -= 1;

            _player.FailmMotion();

            if (0 <= _idleHp)
            {
                _uiController.UpdateHpUI(_idleHp); //HPUIを更新
            }

            if (_idleHp <= 0 && !_isdead)   //ゲームオーバー
            {
                ChangeGameState(_gameEndState); //ゲーム終了状態に遷移
                _isdead = true;
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
        if (_enemies.Count <= 0 && _currentGameState is not BossTime)    //ファンがいなくなったらスクロールを開始する
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

            if (_currentGameState is not BossTime)
            {
                _stageScroller.ScrollOperation();
            }
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
            return;
        }
    }

    public void ChangeTimeElapsing(bool which)
    {
        _isElapsing = which;
    }
     
    /// <summary>ゲームクリア時の処理</summary>
    public void GameClear()
    {
        ChangeGameState(_gameEndState);
        _player.GameClearMove();
    }
}