using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField, Tooltip("エネミーをいれるところ"), Header("エネミー関係")] 
    GameObject _enemyPrefub = default;
    [SerializeField, Tooltip("エネミーを出現させたいオブジェクトをいれるところ")]
    GameObject _positionObject = default;
    [SerializeField, Tooltip("秒数を数える変数(リセットされる)"), Header("時間関係")] 
    float _timeReset = default;
    [SerializeField, Tooltip("時間を保持しておく変数(リセットされない)")] 
    float _time = default;
    [SerializeField, Tooltip("敵を出現させたい秒数")] 
    float _timeInterval = 5f;
    [SerializeField, Tooltip("ゲーム終了時間")] 
    float _gameFinishTime = 60f;

    /// <summary> tureになるとエネミーが出現するようにするトリガー</summary>
    bool _onEnemy = default;

    private void Start()
    {
        _onEnemy = false;　//エネミーが最初から湧き出さないためにbool型の変数を使い、出現を管理する。
    }

    void Update()
    {
        if( _onEnemy == true) //_onEnemyがtrueになると_timeと_timeResetが動き出し、エネミーが湧き出す
        {
            _time += Time.deltaTime;
            _timeReset += Time.deltaTime;

            if (_time < _gameFinishTime) //ゲーム時間を超えるとspawnスポーンしないように
            {
                if (_timeReset > _timeInterval) //_timeIntervalを超えるとInstantiateします
                {
                    Instantiate(_enemyPrefub, _positionObject.transform); //シリアライズで設定したオブジェクトの場所に出現します
                    _timeReset = 0f;
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
    }

    /// <summary>
    /// ボタンに設定するパブリック関数
    /// falseになるとエネミーの出現が止まる
    /// 且つゲームの時間やエネミーが出てくるインターバルの時間をすべてリセット
    /// </summary>
    public void OffEnemy()
    {
        _onEnemy = false;

        _time = 0;
        _timeReset = 0;
    }
}
