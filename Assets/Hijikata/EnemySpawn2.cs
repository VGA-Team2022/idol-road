using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn2 : MonoBehaviour
{
    [Header("エネミー関係")]

    [SerializeField, Tooltip("エネミーをいれるところ")] GameObject _enemyPrefub = default;

    [SerializeField, Tooltip("エネミーを出現させるVector2")] Vector2 _spawnPosition = default;

    [Header("時間関係")]

    [SerializeField, Tooltip("秒数を数える変数(リセットされる)")] float _timeReset = default;

    [SerializeField, Tooltip("時間を保持しておく変数(リセットされない)")] float _time = default;

    [SerializeField, Tooltip("敵を出現させたい秒数")] float _timeInterval = 5f;

    [SerializeField, Tooltip("ゲーム終了時間")] float _gameFinishTime = 60f;

    void Update()
    {
        _time += Time.deltaTime;
        _timeReset += Time.deltaTime;

        if (_time < _gameFinishTime) //ゲーム時間を超えるとspawnスポーンしないように
        {
            if (_timeReset > _timeInterval) //_timeIntervalを超えるとInstantiateします
            {
                Instantiate(_enemyPrefub, _spawnPosition, Quaternion.identity); //シリアライズで設定したX,Yの場所にだします
                _timeReset = 0f;
            }
        }
    }
}
