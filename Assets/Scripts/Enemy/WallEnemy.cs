using System;
using UnityEngine;
using DG.Tweening;

/// <summary>壁になるファンの処理を行うクラス</summary>
public class WallEnemy : EnemyBase
{
    /// <summary>吹き飛び用アニメーションコントローラー </summary>
    Animator _anim => GetComponent<Animator>();

    protected override void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, 2)
            .SetDelay(0.3f)
            .OnComplete(() => Destroy(gameObject));

        //透明になりながら消えていくパターン
        Array.ForEach(EnemySprites, s => 
        s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));

        StageScrollRun();        //ステージスクロールを行う
    }

    protected override void GoodEffect()
    {
        _anim.Play("Effect");
        Array.ForEach(EnemySprites, e =>
        {
            e.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear)
           .SetLoops(-1, LoopType.Restart);
        });
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //ダメージを与える
        Destroy(gameObject);
    }

    protected override void PerfactEffect()
    {
        _anim.Play("Enemy");
        Array.ForEach(EnemySprites, e =>
        {
            e.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear)
           .SetLoops(-1, LoopType.Restart);
        });
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
