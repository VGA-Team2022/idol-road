using System;
using UnityEngine;
using DG.Tweening;

/// <summary>ボスを管理するクラス </summary>
public class BossEnemy : EnemyBase
{
    /// <summary>吹き飛ぶまでの遅延 </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("吹き飛ばしている時間"), Range(0.1f, 10f)]
    float _deadMoveTime = 1f;
    [SerializeField, Header("吹き飛んだ時のサイズ"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("飛ぶ方向 0=left 1=right 2=up・Down"), ElementNames(new string[] { "Left", "Right", "Up・Down" })]
    Transform[] _trajectoryParent = default;

    /// <summary>移動中かどうか</summary>
    bool _isMove = false;

    /// <summary>倒された時の吹き飛ぶ軌道を構成するポイントの配列 </summary>
    Vector3[] _deadMovePoints = default;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (_isMove)
        {
            UpdateResultTime();
        }
    }

    protected override void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //透明になりながら消えていくパターン
        Array.ForEach(EnemySprites, s => s.GetComponent<SpriteRenderer>()
        .DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));
    }

    protected override void GoodEffect()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOPath(path: _deadMovePoints, duration: _deadMoveTime, pathType: PathType.CatmullRom))
            .Join(transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _deadMoveTime))
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //回転 無限ループを行う為
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //ダメージを与える
        Destroy(gameObject);
    }

    protected override void PerfactEffect()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOPath(path: _deadMovePoints, duration: _deadMoveTime, pathType: PathType.CatmullRom))
            .Join(transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _deadMoveTime))
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //回転 無限ループを行う為
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>移動を開始する</summary>
    public void MoveStart()
    {
        Debug.Log("Hello");
        _rb.AddForce(-transform.forward * _enemySpped); //ファンを前に移動させる
        _isMove = false;
        FlickNum(); //ランダムでフリック方向を取得する
    }
}
