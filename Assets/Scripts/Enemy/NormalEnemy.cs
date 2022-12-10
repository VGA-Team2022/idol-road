using System;
using UnityEngine;
using DG.Tweening;

/// <summary>通常ファンの処理を行うクラス</summary>
public class NormalEnemy : EnemyBase
{
    /// <summary>吹き飛ぶまでの遅延 </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("吹き飛ばしている時間"), Range(0.1f, 10f)]
    float _deadMoveTime = 1f;
    [SerializeField, Header("吹き飛んだ時のサイズ"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("飛ぶ方向 0=left 1=right 2=up・Down"), ElementNames(new string[] { "Left", "Right", "Up・Down" })]
    Transform[] _trajectoryParent = default;

    /// <summary>倒された時の吹き飛ぶ軌道を構成するポイントの配列 </summary>
    Vector3[] _deadMovePoints = default;

    protected override void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //透明になりながら消えていくパターン
        _sr.DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun);

        StageScrollRun();        //ステージスクロールを行う
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

    public override void SetUp(IState currentGameState)
    {
        base.SetUp(currentGameState);

        var points = transform;     //一時的に軌道の親オブジェクトを保持する為の変数

        switch (_currentRequest)    //各ファンサで吹き飛ぶ方向を決める
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
