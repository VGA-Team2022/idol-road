using DG.Tweening;
using System;
using UnityEngine;

/// <summary>エネミーを管理するクラス </summary>
[RequireComponent(typeof(Rigidbody), typeof(SpriteRenderer), typeof(EnemySpriteChange))]
public class Enemy : MonoBehaviour
{
    /// <summary>吹き飛ぶまでの遅延 </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("向かってくる速度"), Range(1, 50)]
    float _enemySpped;
    [SerializeField, Header("透明になるまでにかかる時間")]
    float _fadedSpeed = 1;
    [ElementNames(new string[] { "合計時間", "Bad", "Good", "Perfect", "Out" })]
    [SerializeField, Header("リズム判定の秒数"), Tooltip("0=合計時間 1=bad 2=good 3=perfect 4=out")]
    float[] _resultTimes = default;
    [SerializeField, Header("倒した時のスコア")]
    int _addScoreValue = 1;
    [SerializeField, Header("吹き飛ばしている時間"), Range(0.1f, 10f)]
    float _deadMoveTime = 1f;
    [SerializeField, Header("吹き飛んだ時のサイズ"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("爆発エフェクト")]
    GameObject _explosionEffect = default;
    [SerializeField, Tooltip("ファンサ")]
    RequestUIController _requestUIController = null;
    [SerializeField, Tooltip("飛ぶ方向 0=left 1=right 2=up・Down"), ElementNames(new string[] { "Left", "Right", "Up・Down" })]
    Transform[] _trajectoryParent = default;

    /// <summary>倒された時の吹き飛ぶ軌道を構成するポイントの配列 </summary>
    Vector3[] _deadMovePoints = default;
    /// <summary>FlickTypeを保存させておく変数 </summary>
    FlickType _flickTypeEnemy;
    /// <summary>吹き飛び時の評価 </summary>
    TimingResult _currentResult = TimingResult.None;
    /// <summary>ファンサを要求する数</summary>
    int _fansaNum = 1;
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

#region
    Rigidbody _rb => GetComponent<Rigidbody>();
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();
    /// <summary>敵のスプライトを管理するクラスの変数 </summary>
    EnemySpriteChange _spriteChange => GetComponent<EnemySpriteChange>();

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
                _sr.DOFade(endValue: 0, duration: _fadedSpeed).OnComplete(OutEffect);
                _addComboCount?.Invoke(_currentResult);
                _isdead = true;
                break;
        }

        _requestUIController.ChangeRequestWindow(_currentResult);
    }

    /// <summary>ファンサを決める</summary>
    void FlickNum()
    {
        var rnd = UnityEngine.Random.Range(1, 5);
        _flickTypeEnemy = (FlickType)rnd;
        _requestUIController.ChangeRequestImage(_flickTypeEnemy);
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
                DeadEffect();
                break;
            case TimingResult.Perfect:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySE(6, 0.1f);
                DeadEffect();
                break;
        }
    }

    /// <summary> Bad, Out以外で使用する吹き飛び演出 </summary>
    void DeadEffect()
    {
        //移動処理
        transform.DOPath(path: _deadMovePoints, duration: _deadMoveTime, pathType: PathType.CatmullRom)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));


        //大きさ調整
        transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _stageScroll?.Invoke());

        //回転
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>評価BadのときのEnemyの動き</summary>
    void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(DeadProcess);
        //.OnComplete(() =>
        //{
        //    _sr.DOFade(endValue: 0, duration: 2.0f).OnComplete(DeadProcess);
        //});

        //透明になりながら消えていくパターン
        _sr.DOFade(endValue: 0, duration: 2.0f).OnComplete(() => _giveDamage?.Invoke(_fansaNum));
    }

    /// <summary>out時のの処理</summary>
    void OutEffect()
    {
        _giveDamage?.Invoke(_fansaNum); //ダメージを与える
        DeadProcess();
    }

    /// <summary>死亡時の共通処理</summary>
    void DeadProcess()
    {
        _stageScroll?.Invoke();         //ステージスクロールを行う
        Destroy(gameObject);
    }

    /// <summary>倒された時の処理 </summary>
    public void Dead()
    {
        if (_isdead) { return; }

        _isdead = true;
        _requestUIController.gameObject.SetActive(false);
        SelectDeadEffect();
    }

    /// <summary>判定別の処理を行う</summary>
    public void JugeTime(FlickType playInput)
    {
        if (_flickTypeEnemy != playInput || _isdead) { return; }

        switch (_currentResult)
        {
            case TimingResult.Perfect:
                ResultManager.Instance.CountPerfect++;
                break;
            case TimingResult.Good:
                ResultManager.Instance.CountGood++;
                break;
            case TimingResult.Bad:
                ResultManager.Instance.CountBad++;
                break;
        }

        _addComboCount?.Invoke(_currentResult);
        Dead();
    }

    /// <summary>生成時の初期化処理 </summary>
    public void SetUp(IState istate)
    {
        var points = transform;     //一時的に軌道の親オブジェクトを保持する為の変数

        FlickNum(); //ランダムでフリック方向を取得する

        if(istate is BossTime) 
        {
            _spriteChange.EnemyBossMethod(_sr);
        }
        else { _spriteChange.EnemyRandomMethod(_sr); }

        switch (_flickTypeEnemy)    //各ファンサで吹き飛ぶ方向を決める
        {
            case FlickType.Left:
                points = _trajectoryParent[0];
                break;
            case FlickType.Right:
                points = _trajectoryParent[1];
                break;
            case FlickType.Up:
            case FlickType.Down:
                points = _trajectoryParent[2];
                break;
        }

        _deadMovePoints = new Vector3[points.childCount - 1];

        for (var i = 0; i < _deadMovePoints.Length; i++)
        {
            _deadMovePoints[i] = points.GetChild(i).position;
        }
    }
}