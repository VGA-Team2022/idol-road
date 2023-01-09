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

    private void Update()
    {
        if (_isMove && !_isdead)
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

           AudioManager.Instance.PlayVoice(8);
    }

    protected override void GoodEffect()
    {
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(1);
        AudioManager.Instance.PlaySE(9);

    }

    protected override void PerfactEffect()
    {
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(1);
        AudioManager.Instance.PlaySE(9);

    }

    public override void Setup(IState currentGameState, EnemyInfo info)
    {
        base.Setup(currentGameState, info);

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

        _isMove = true;
    }

    /// <summary>移動アニメーションを再生する </summary>
    public void StartMoveAnim()
    {
        _anim.Play("Walk");
        AudioManager.Instance.PlaySE(17);
    }

    /// <summary>
    /// 吹き飛びアニメーションが終了したら自身を削除する
    /// アニメーショントリガーで呼び出す
    /// </summary>
    public void ThisDestroy()
    {
        _gameClear?.Invoke();
        Destroy(gameObject);
    }
}
