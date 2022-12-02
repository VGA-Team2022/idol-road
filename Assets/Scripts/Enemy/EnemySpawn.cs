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

    private void Start()
    {
        _i = Random.Range(0, _timeInterval.Length);
        Debug.Log(_timeInterval[_i]);
    }

    void Update()
    {
        //アイドルタイム中も出さないようにする
        if (_manager.Started && !_manager.GameEnd)
        {
            //ファンがいない場合に生成する。ボス戦は例外で連続で出す
            if (_manager.CurrentEnemy == null || _manager.BossBattle) 
            {
                _timeReset += Time.deltaTime;

                if (_timeReset >= _timeInterval[_i]) //_timeIntervalを超えるとInstantiateします
                {
                    var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //シリアライズで設定したオブジェクトの場所に出現します
                    enemy.SetDeadMovePoints(_trajectoryParent);
                    //イベントを登録
                    enemy.AddScore += _manager.KillFun;
                    enemy.StageScroll += _manager.Scroller.ScrollOperation;
                    enemy.GiveDamage += _manager.GetDamage;
                    _manager.CurrentEnemy = enemy;
                    _manager.Scroller.ScrollOperation();
                    _i = Random.Range(0, _timeInterval.Length);
                    Debug.Log(_timeInterval[_i]);
                    _timeReset = 0 + _generateOffset;
                }
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
        _timeReset = 0;
    }
}
