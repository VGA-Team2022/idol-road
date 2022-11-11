using System;
using UnityEngine;
using DG.Tweening;

/// <summary>アイドルパワーを増減させるアイテムのクラス</summary>
public class IdolPowerItem : MonoBehaviour
{
    [SerializeField, Header("アイテム取得時に増減する値")]
    int _getAmount = 0;
    [SerializeField, Header("移動にかかる時間")]
    float _duration = 2f;
    [SerializeField, Header("軌道の高さ")]
    float _jumpPower = 4f;
    [SerializeField, Header("表示時間")]
    float _liveTime = 3f;
    private GameManager _gameManager;
    event Action _scrollStart = default;
    /// <summary>ゲームマネージャーのプロパティ</summary>
    public GameManager GameManager { get => _gameManager; set => _gameManager = value; }
    private void Start()
    {
    }
    public event Action ScrollStart
    {
        add { _scrollStart += value; }
        remove { _scrollStart += value; }
    }

    /// <summary>移動処理 </summary>
    /// <param name="targetPoint">到着位置</param>
    /// /// <param name="scrollStart">移動処理が終了したらスクロールさせる</param>
    public void Move(Vector3 targetPoint, Action scrollStart)
    {
        transform.DOJump(targetPoint, jumpPower: _jumpPower, numJumps: 1, duration: _duration)
            .OnComplete(() => scrollStart.Invoke());

        Destroy(gameObject, _liveTime);
    }

    //TODO:アイテム効果の追加
    /// <summary>アイテム取得時</summary>
    public void GetItem()
    {
        _gameManager.IdlePower += _getAmount;
    }
}
