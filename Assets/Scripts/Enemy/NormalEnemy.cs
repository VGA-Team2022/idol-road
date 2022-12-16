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
   
    /// <summary>吹き飛ぶ演出で再生するアニメーションの名前</summary>
    string _playAnimName = "";

    protected override void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //透明になりながら消えていくパターン
        Array.ForEach(EnemySprites, s => s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));
        
        StageScrollRun();        //ステージスクロールを行う

        AudioManager.Instance.PlayVoice(12);
    }

    protected override void GoodEffect()
    {
        _anim.Play(_playAnimName);
    
        //回転 無限ループを行う為
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(2);
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //ダメージを与える
        Destroy(gameObject);

        AudioManager.Instance.PlayVoice(11);
    }

    protected override void PerfactEffect()
    {
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        EnemySprites[0].transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(4);
    }

    public override void SetUp(IState currentGameState)
    {
        base.SetUp(currentGameState);

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
    }


    /// <summary>
    /// 吹き飛びアニメーションが終了したら自身を削除する
    /// アニメーショントリガーで呼び出す
    /// </summary>
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
