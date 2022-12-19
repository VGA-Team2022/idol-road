using System;
using UnityEngine;
using DG.Tweening;

/// <summary>ファン(敵)の基底クラス </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class EnemyBase : MonoBehaviour
{
    /// <summary>ファンサ要求の最小値 FlickTypeに対応</summary>
    const int REQUREST_MIN_VALUE = 1;
    /// <summary>ファンサ要求の最大値 FlickTypeに対応</summary>
    const int REQUREST_MAX_VALUE = 5;

    #region
    [SerializeField, Header("ファンの種類")]
    EnemyType _enemyType = EnemyType.None;
    [SerializeField, Tooltip("ファンサを更新するクラス")]
    RequestUIController[] _requestUIArray = null;
    [SerializeField, Tooltip("イラストを管理するクラス")]
    EnemySpriteChange[] _enemySprites = null;
    [SerializeField, Tooltip("爆発エフェクト")]
    GameObject _explosionEffect = default;

    /// <summary>各ファンのパラメーター 0=通常 1=壁ファン2 2=壁ファン3 4=boss</summary>
    EnemyParameter[] _parameters => LevelManager.Instance.CurrentLevel.EnemyParameters;
    /// <summary>現在のパラメーター</summary>
    protected EnemyParameter _currentParameter = default;
    /// <summary>ファンサ要求を保持する配列 </summary>
    FlickType[] _requestArray = default;
    /// <summary>吹き飛び時の評価 </summary>
    TimingResult _currentResult = TimingResult.None;
    /// <summary>現在要求しているファンサ </summary>
    protected FlickType _currentRequest = FlickType.None;
    /// <summary>答えたファンサ数</summary>
    int _requestCount = 0;
    /// <summary>評価判定の時間の添え字 </summary>
    int _resultTimeIndex = 0;
    /// <summary>敵の死亡フラグ</summary>
    bool _isdead = false;
    /// <summary>評価変更用変数</summary>
    float _time = 0f;

    /// <summary>倒されたらステージスクロールを開始する </summary>
    event Action _stageScroll = default;
    /// <summary>ダメージを与える（プレイヤーの体力を減らす）</summary>
    event Action<int> _giveDamage = default;

    /// <summary>コンボ数を増やす処理 </summary>
    event Action<TimingResult> _addComboCount = default;

    /// <summary>敵が死んだらリストから消える処理 </summary>
    event Action<EnemyBase> _disapperEnemies = default;

    /// <summary>倒されたらステージスクロールを開始する </summary>
    public event Action StageScroll
    {
        add { _stageScroll += value; }
        remove { _stageScroll -= value; }
    }

    /// <summary>ダメージを与える（プレイヤーの体力を減らす）</summary>
    public event Action<int> GiveDamage
    {
        add { _giveDamage += value; }
        remove { _giveDamage -= value; }
    }

    /// <summary>コンボ数を増やす処理 </summay>
    public event Action<TimingResult> AddComboCount
    {
        add { _addComboCount += value; }
        remove { _addComboCount -= value; }
    }

    /// <summary>敵が死んだらリストから消える処理 </summay>
    public event Action<EnemyBase> DisapperEnemies
    {
        add { _disapperEnemies += value; }
        remove { _disapperEnemies -= value; }
    }

    protected Rigidbody _rb => GetComponent<Rigidbody>();

    /// <summary>吹き飛び用アニメーションコントローラー </summary>
    protected Animator _anim => GetComponent<Animator>();

    protected EnemySpriteChange[] EnemySprites { get => _enemySprites; }

    #endregion

    void Start()
    {
        _rb.AddForce(-transform.forward * _currentParameter.MoveSpped); //ファンを前に移動させる
        _time = _currentParameter.RhythmTimes[_resultTimeIndex];
    }

    void Update()
    {
        if (!_isdead)
        {
            UpdateResultTime();
        }
    }

    /// <summary>敵の種類によってパラメーターを変更する </summary>
    void SelectEnemyParameter()
    {
        switch (_enemyType)
        {
            case EnemyType.Nomal:
                _currentParameter = _parameters[0];
                break;
            case EnemyType.Wall2:
                _currentParameter = _parameters[1];
                break;
            case EnemyType.Wall3:
                _currentParameter = _parameters[2];
                break;
            case EnemyType.Boss:
                _currentParameter = _parameters[3];
                break;
        }

        _time = _currentParameter.RhythmTimes[_resultTimeIndex];    //リズム判定用のタイマーを初期化する
    }

    /// <summary>スコアを要求数分増加させる </summary>
    void AddScore()
    {
        switch (_currentResult)
        {
            case TimingResult.Perfect:
                PlayResult.Instance.CountPerfect += _requestArray.Length;
                break;
            case TimingResult.Good:
                PlayResult.Instance.CountGood += _requestArray.Length;
                break;
            case TimingResult.Bad:
                PlayResult.Instance.CountBad += _requestArray.Length;
                break;
        }
    }

    /// <summary>吹き飛び時の評価を更新,吹き出しを更新</summary>
    void UpdateCurrentResult()
    {
        switch (_currentResult)
        {
            case TimingResult.None:
                _currentResult = TimingResult.Bad;
                break;
            case TimingResult.Bad:
                _currentResult = TimingResult.Good;
                break;
            case TimingResult.Good:
                _currentResult = TimingResult.Perfect;
                break;
            case TimingResult.Perfect:
                _currentResult = TimingResult.Out;
                OutEffect();
                break;
        }

        Array.ForEach(_requestUIArray, r => r.ChangeRequestWindow(_currentResult));
    }

    /// <summary>評価によって倒された時の移動方法を変更する </summary>
    void SelectDeadEffect()
    {
        switch (_currentResult)
        {
            case TimingResult.Bad:
                BadEffect();
                // AudioManager.Instance.PlaySE(2, 0.5f);
                break;
            case TimingResult.Good:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //爆発エフェクトを生成
                                                                                            //  AudioManager.Instance.PlaySE(6, 0.7f);
                GoodEffect();
                break;
            case TimingResult.Perfect:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                //    AudioManager.Instance.PlaySE(6, 0.7f);
                PerfactEffect();
                break;
        }
    }

    /// <summary>アウト時の演出 (s処理)</summary>
    void OutEffect()
    {
        //spirteをフェードさせる
        Array.ForEach(_enemySprites, s =>
              s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: _currentParameter.FadeSpeed).OnComplete(() => Destroy(gameObject)));

        _giveDamage?.Invoke(_currentParameter.AddDamageValue);  //ダメージを与える
        _addComboCount?.Invoke(_currentResult);
        _disapperEnemies?.Invoke(this);

        _isdead = true;
    }

    /// <summary>倒された時の処理 </summary>
    void Dead()
    {
        if (_isdead) { return; }

        _isdead = true;
        Array.ForEach(_requestUIArray, r => r.gameObject.SetActive(false));
        SelectDeadEffect();
    }

    /// <summary>bad判定時の演出 </summary>
    protected abstract void BadEffect();

    /// <summary>good判定時の演出 </summary>
    protected abstract void GoodEffect();

    /// <summary>パーフェクト判定時の演出 </summary>
    protected abstract void PerfactEffect();

    /// <summary>ステージスクロール処理を実行する </summary>
    protected void StageScrollRun()
    {
        _stageScroll?.Invoke();
    }

    /// <summary>ダメージ処理を実行する </summary>
    protected void GiveDamageRun()
    {
        _giveDamage?.Invoke(_currentParameter.AddDamageValue);
    }

    /// <summary>判定に使用する経過時間を計測する </summary>
    protected void UpdateResultTime()
    {
        _time -= Time.deltaTime;// リズム判定用

        if (_time <= _currentParameter.RhythmTimes[_resultTimeIndex + 1] && !_isdead)    //吹き飛び時の評価を更新する
        {
            _resultTimeIndex++;
            UpdateCurrentResult();
        }
    }

    /// <summary>ファンサを決める</summary>
    protected void FlickNum()
    {
        for (var i = 0; i < _requestUIArray.Length; i++)
        {
            var rnd = UnityEngine.Random.Range(REQUREST_MIN_VALUE, REQUREST_MAX_VALUE);

            _requestArray[i] = (FlickType)rnd;
            _requestUIArray[i].ChangeRequestImage((FlickType)rnd);
        }

        _currentRequest = _requestArray[0]; //最初のファンサを決める
    }


    /// <summary>生成時の初期化処理 </summary>
    /// <param name="istate">現在のゲームの状態</param>
    public virtual void SetUp(IState currentGameState)
    {
        _requestArray = new FlickType[_requestUIArray.Length];

        SelectEnemyParameter();

        FlickNum(); //ランダムでフリック方向を取得する

        if (currentGameState is BossTime)
        {
            Array.ForEach(_enemySprites, e => e.EnemyBossMethod(e.gameObject.GetComponent<SpriteRenderer>()));
        }
        else
        {
            Array.ForEach(_enemySprites, e => e.EnemyRandomMethod(e.gameObject.GetComponent<SpriteRenderer>()));
        }

        //各ファンごとに行いたい処理があればoverrideする
    }

    /// <summary>判定別の処理を行う</summary>
    public void JugeTime(FlickType playInput)
    {
        if (_currentRequest != playInput || _isdead) { return; }

        _requestUIArray[_requestCount].gameObject.SetActive(false);     //達成したファンサUIを非表示にする
        _requestCount++;

        if (_requestCount == _requestArray.Length)  //倒された
        {
            AddScore();
            _disapperEnemies?.Invoke(this);
            _addComboCount?.Invoke(_currentResult);
            _stageScroll?.Invoke();
            Dead();
            return;
        }

        _currentRequest = _requestArray[_requestCount];     //要求ファンサを更新
    }
}
