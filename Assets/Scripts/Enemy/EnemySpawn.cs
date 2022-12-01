using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField, Tooltip("エネミーをいれるところ"), Header("エネミー関係")]
    Enemy _enemyPrefub = default;
    [SerializeField, Tooltip("エネミーを出現させたいオブジェクトをいれるところ")]
    GameObject _positionObject = default;
    [SerializeField, Header("敵が飛んでいく軌道")]
    Transform _trajectoryParent = default;
    [SerializeField, Tooltip("ゲームマネジャー")]
    GameManager _manager = default;
    [SerializeField, Tooltip("秒数を数える変数(リセットされる)"), Header("時間関係")]
    float _timeReset = default;
    [SerializeField, Tooltip("時間を保持しておく変数(リセットされない)")]
    float _gameTime = default;
    [SerializeField, Header("最初の敵を生成するまで遅れ")]
    float _firstDelayTime = 7.68f;
    [SerializeField, Header("各生成のオフセット")]
    float _generateOffset = default;
    [Tooltip("敵を出現させたい秒数")]
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    [Tooltip("random変数")]
    int _i = 0;
    [SerializeField, Tooltip("ゲーム終了時間")]
    float _gameFinishTime = 60f;
    /// <summary> tureになるとエネミーが出現するようにするトリガー</summary>
    [SerializeField]
    bool _onEnemy = default;
    /// <summary>スタートフラグ</summary>
    bool _isStart = default;
    private void Start()
    {
        _onEnemy = false;　//エネミーが最初から湧き出さないためにbool型の変数を使い、出現を管理する。
        _isStart = false;
        _i = Random.Range(0, _timeInterval.Length);
        Debug.Log(_timeInterval[_i]);
    }

    void Update()
    {
        if (_isStart == true && _gameTime < _gameFinishTime)//ゲーム時間を超えるとspawnスポーンしないように
        {
            _gameTime += Time.deltaTime;

            //_onEnemyがtrueになると_timeと_timeResetが動き出し、エネミーが湧き出す
            //ここにボス戦のフラグをorで入れたい
            if (_onEnemy == true && _manager.CurrentEnemy == null) 
            {
                _timeReset += Time.deltaTime;

                if (_timeReset >= _timeInterval[_i]) //_timeIntervalを超えるとInstantiateします
                {
                    //if (_manager.CurrentEnemy == null)
                    //{
                    var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //シリアライズで設定したオブジェクトの場所に出現します
                    enemy.SetDeadMovePoints(_trajectoryParent);
                    //イベントを登録
                    enemy.AddScore += _manager.KillFun;
                    enemy.StageScroll += _manager.Scroller.ScrollOperation;
                    enemy.GiveDamage += _manager.GetDamage;

                    _manager.CurrentEnemy = enemy;
                    _manager.Scroller.ScrollOperation();
                    //_onEnemy = false;//ここでFalseにしないと生成管理の時間が進んでしまう
                    _i = Random.Range(0, _timeInterval.Length);
                    Debug.Log(_timeInterval[_i]);
                    //}
                    _timeReset = 0 + _generateOffset;
                }
            }
        }
    }

    /// <summary>
    /// ボタンに設定するパブリック関数
    /// trueになるとエネミーが湧き出す
    /// </summary>
    public void OnEnemy()
    {
        _onEnemy = true;
        _isStart = true;
    }
    /// <summary>プレイ中の生成フラグを管理する関数</summary>
    public void InGameOnEnemy()
    {
        StartCoroutine(OnEnemyCoroutine());
    }
    /// <summary>Enemyの生成フラグを1秒後にtrueにする関数</summary>
    public IEnumerator OnEnemyCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        _onEnemy = true;
        yield return null;
    }

    /// <summary>
    /// ボタンに設定するパブリック関数
    /// falseになるとエネミーの出現が止まる
    /// 且つゲームの時間やエネミーが出てくるインターバルの時間をすべてリセット
    /// </summary>
    public void OffEnemy()
    {
        _onEnemy = false;

        _gameTime = 0;
        _timeReset = 0;
    }
}
