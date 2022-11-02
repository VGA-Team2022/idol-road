using System;
using UnityEngine;
using DG.Tweening;

/// <summary>アイドルパワーを増減させるアイテムのクラス</summary>
public class IdolPowerItem : MonoBehaviour
{
    [SerializeField, Header("移動にかかる時間")]
    float _duration = 2f;
    [SerializeField, Header("軌道の高さ")]
    float _jumpPower = 4f;
    [SerializeField, Header("表示時間")]
    float _liveTime = 3f;

    event Action _scrollStart = default;

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
}
