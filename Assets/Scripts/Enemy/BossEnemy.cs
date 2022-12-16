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
    [SerializeField, Tooltip("イラスト")]
    SpriteRenderer _bossSprite = default;
    /// <summary>ゲームクリア時の処理 </summary>
    event Action _gameClear = default;
    /// <summary>吹き飛ぶ演出で再生するアニメーションの名前</summary>
    string _playAnimName = "";

    /// <summary>移動中かどうか</summary>
    bool _isMove = false;

    public SpriteRenderer BossSprite { get => _bossSprite; }

    /// <summary>ゲームクリア時の処理 </summary>
    public event Action GameClear
    {
        add { _gameClear += value; }
        remove { _gameClear -= value; }
    }

    private void Start()
    {
        _time = _resultTimes[_resultTimeIndex];
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
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
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
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>移動を開始する</summary>
    public void MoveStart()
    {
        _rb.AddForce(-transform.forward * _enemySpped); //ファンを前に移動させる
        _isMove = true;
        FlickNum();

        switch (_currentRequest)    //各ファンサで吹き飛ぶ方向を決める
        {
            case FlickType.Left:
                _playAnimName = FlickType.Left.ToString();
                break;
            case FlickType.Right:
                _playAnimName = FlickType.Right.ToString();
                break;
            case FlickType.Up:
            case FlickType.Down:
                _playAnimName = FlickType.Up.ToString();
                break;
        }

        _requestUIArray[0].gameObject.SetActive(true);
    }

    /// <summary>
    /// 吹き飛びアニメーションが終了したら自身を削除する
    /// アニメーショントリガーで呼び出す
    /// </summary>
    public void ThisDestroy()
    {
        _gameClear?.Invoke();
        Destroy(this.gameObject);
    }
}
