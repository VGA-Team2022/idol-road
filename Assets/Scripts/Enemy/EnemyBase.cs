using System;
using UnityEngine;
using DG.Tweening;

/// <summary>ファン(敵)の基底クラス </summary>
[RequireComponent(typeof(Rigidbody), typeof(SpriteRenderer))]
public abstract class EnemyBase : MonoBehaviour
{
#region
    [SerializeField, Header("向かってくる速度"), Range(1, 50)]
    float _enemySpped;
    [SerializeField, Header("透明になるまでにかかる時間")]
    float _fadedSpeed = 1;
    [ElementNames(new string[] { "合計時間", "Bad", "Good", "Perfect", "Out" })]
    [SerializeField, Header("リズム判定の秒数"), Tooltip("0=合計時間 1=bad 2=good 3=perfect 4=out")]
    float[] _resultTimes = default;
    [SerializeField, Tooltip("ファンサを更新するクラス")]
    RequestUIController[] _requestUIArray = null;
    [SerializeField, Tooltip("イラストを管理するクラス")]
    EnemySpriteChange[] _enemySprites = null;
    [SerializeField, Tooltip("爆発エフェクト")]
    protected GameObject _explosionEffect = default;

    /// <summary>ファンサ要求を保持する配列 </summary>
    protected FlickType[] _requestArray = default;
    /// <summary>吹き飛び時の評価 </summary>
    protected TimingResult _currentResult = TimingResult.None;
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
    /// <summary>コンボ数を増やす処理 </summay>
    event Action<TimingResult> _addComboCount = default;
    /// <summary>敵が死んだらリストから消える処理 </summay>
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

    Rigidbody _rb => GetComponent<Rigidbody>();

    protected EnemySpriteChange[] EnemySprites { get => _enemySprites; }

    #endregion

    private void Start()
    {
        _rb.AddForce(-transform.forward * _enemySpped); //ファンを前に移動させる
        _time = _resultTimes[_resultTimeIndex];
    }

    private void Update()
    {
        if (!_isdead)
        {
            _time -= Time.deltaTime;// リズム判定用

            if (_time <= _resultTimes[_resultTimeIndex + 1])    //吹き飛び時の評価を更新する
            {
                _resultTimeIndex++;
                UpdateCurrentResult();
            }
        }
    }

    /// <summary>スコアを要求数分増加させる </summary>
    void AddScore()
    {
        switch (_currentResult)
        {
            case TimingResult.Perfect:
                ResultManager.Instance.CountPerfect += _requestArray.Length;
                break;
            case TimingResult.Good:
                ResultManager.Instance.CountGood += _requestArray.Length;
                break;
            case TimingResult.Bad:
                ResultManager.Instance.CountBad += _requestArray.Length;
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
                Array.ForEach(_enemySprites, s => 
                s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: _fadedSpeed).OnComplete(() => Destroy(gameObject)));
                _giveDamage?.Invoke(_requestArray.Length);
                _addComboCount?.Invoke(_currentResult);
                _disapperEnemies?.Invoke(this);
                _isdead = true;
                break;
        }

        Array.ForEach(_requestUIArray, r => r.ChangeRequestWindow(_currentResult));
    }

    /// <summary>ファンサを決める</summary>
    void FlickNum()
    {
        for (var i = 0; i < _requestUIArray.Length; i++)
        {
            var rnd = UnityEngine.Random.Range(1, 5);
            _requestArray[i] = (FlickType)rnd;
            _requestUIArray[i].ChangeRequestImage((FlickType)rnd);
        }

        _currentRequest = _requestArray[0];
    }

    /// <summary>評価によって倒された時の移動方法を変更する </summary>
    void SelectDeadEffect()
    {
        switch (_currentResult)
        {
            case TimingResult.Bad:
                BadEffect();
                break;
            case TimingResult.Good:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //爆発エフェクトを生成
                AudioManager.Instance.PlaySE(6, 0.1f);
                GoodEffect();
                break;
            case TimingResult.Perfect:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySE(6, 0.1f);
                PerfactEffect();
                break;
        }
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

    /// <summary>アウト判定時の演出 </summary>
    protected abstract void OutEffect();

    /// <summary>ステージスクロール処理を実行する </summary>
    protected void StageScrollRun()
    {
        _stageScroll?.Invoke();
    }

    /// <summary>ダメージ処理を実行する </summary>
    protected void GiveDamageRun()
    {
        _giveDamage?.Invoke(_requestArray.Length);
    }


    /// <summary>生成時の初期化処理 </summary>
    /// <param name="istate">現在のゲームの状態</param>
    public virtual void SetUp(IState currentGameState)
    {
        _requestArray = new FlickType[_requestUIArray.Length];

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
