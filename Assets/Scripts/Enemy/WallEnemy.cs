using System;
using UnityEngine;
using DG.Tweening;

/// <summary>壁になるファンの処理を行うクラス</summary>
public class WallEnemy : EnemyBase
{
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
        Destroy(gameObject);
        // throw new System.NotImplementedException();
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //ダメージを与える
        Destroy(gameObject);
    }

    protected override void PerfactEffect()
    {
        Destroy(gameObject);
        // throw new System.NotImplementedException();
    }
}
