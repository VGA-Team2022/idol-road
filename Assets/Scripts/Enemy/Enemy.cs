using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>エネミーを管理するクラス </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>吹き飛ぶまでの遅延 </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("現在の移動方法")]
    MoveMethod _currentMoveMethod = MoveMethod.Path;
    [SerializeField, Tooltip("ファンが動く方向"), Header("ファン関係")]
    Vector3 _enemySpped;
    [SerializeField, Tooltip("ファンサを要求する数")]
    int _fansaNum = 1;
    [SerializeField, Tooltip("リズム判定をするための時間(デバッグ用)"), Header("リズム関係")]
    float _time = default;
    [SerializeField, Tooltip("リズム判定の秒数")]
    float _perfect, _good, _bad, _out;
    [SerializeField, Header("倒した時のスコア")]
    int _addScoreValue = 1;
    [SerializeField, Header("移動にかかる時間"), Range(0.1f, 10f)]
    float _moveTime = 1f;
    [SerializeField, Header("吹き飛んだ時のサイズ"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("爆発エフェクト")]
    GameObject _explosionEffect = default;
    [SerializeField, Tooltip("フリック方向を表示するテキスト")]
    Text _flickDirtext = default;
    /// <summary>倒された時の吹き飛ぶ軌道を構成するポイントの配列 </summary>
    Vector3[] _deadMovePoints = default;
    public FlickType _flickTypeEnemy;
    /// <summary>スコアを増やすAction </summary>
    event Action<int> _addScore = default;
    /// <summary>倒されたらステージスクロールを開始する </summary>
    event Action _stageScroll = default;
    Rigidbody _rb;


    /// <summary>スコアを増やすAction </summary>
    public event Action<int> AddScore
    {
        add { _addScore += value; }
        remove { _addScore -= value; }
    }

    public event Action StageScroll
    {
        add {_stageScroll += value; }
        remove { _stageScroll -= value; }
    }
    private void Start()
    {
        for(int i = 0; i < _fansaNum; i++)
        {
            FlickNum(); //ランダムでフリック方向を取得する
        }
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_enemySpped); //ファンを前に動かす（仮)
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dead();
        }
        _time -= Time.deltaTime;// リズム判定用
    }

    /// <summary> 吹き飛ぶ演出（移動） </summary>
    void DeadMove()
    {
        if (_currentMoveMethod == MoveMethod.Path)
        {
            //移動処理
            transform.DOPath(path: _deadMovePoints, duration: _moveTime, pathType: PathType.CatmullRom)
                .SetDelay(EXPLOSION_DELAY)
                .OnComplete(() => Destroy(gameObject));
        }
        else if (_currentMoveMethod == MoveMethod.Jump)
        {
            transform.DOJump(_deadMovePoints[_deadMovePoints.Length - 1], jumpPower: 1f, numJumps: 1, duration: _moveTime)
                .SetDelay(EXPLOSION_DELAY)
                .OnComplete(() => _addScore.Invoke(_addScoreValue))
                .OnComplete(() => Destroy(gameObject));
        }

        //大きさ調整
        transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _moveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _stageScroll?.Invoke());

        //回転
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>倒された時の処理 </summary>
    public void Dead()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //爆発エフェクトを生成
        DeadMove();
    }

    /// <summary>倒された時の軌道のポイントを取得する </summary>
    /// <param name="pointParent">軌道のポイントを子オブジェクトに持つオブジェクト(親オブジェ)</param>
    public void SetDeadMovePoints(Transform pointParent)
    {
        _deadMovePoints = new Vector3[pointParent.childCount];

        for (var i = 0; i < _deadMovePoints.Length; i++)
        {
            _deadMovePoints[i] = pointParent.GetChild(i).position;
        }
    }
    /// <summary>Flickする方向をランダムに取得する</summary>
    public void FlickNum()
    {
        var rnd = new System.Random();
        _flickTypeEnemy = (FlickType)rnd.Next(2, 5);        
        Debug.Log(_flickTypeEnemy);
        _flickDirtext.text = _flickTypeEnemy.ToString();
    }
    /// <summary>リズム判定用</summary>
    public void JugeTime()
    {
        if (_time <= _out)
        {
            Debug.Log("out");
        }
        else if (_time <= _perfect)
        {
            Debug.Log($"perfect { _time:F1}");
        }
        else if (_time <= _good)
        {
            Debug.Log($"good { _time:F1}");
        }
        else if (_time <= _bad)
        {
            Debug.Log($"bad { _time:F1}");
        }
    }

    /// <summary>移動方法 </summary>
    public enum MoveMethod
    {
        /// <summary>移動位置を指定する方法 </summary>
        Path,
        /// <summary>最終的な位置にジャンプする方法 </summary>
        Jump,
    }
}