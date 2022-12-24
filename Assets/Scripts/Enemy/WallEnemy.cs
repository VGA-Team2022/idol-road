using System;
using UnityEngine;
using DG.Tweening;

/// <summary>壁になるファンの処理を行うクラス</summary>
[RequireComponent(typeof(EnemyVoice))]
public class WallEnemy : EnemyBase
{
    /// <summary>成功時ボイスのID</summary>
    const int SUCCESS_VOICE_ID = 5;
    /// <summary>失敗時ボイスのID </summary>
    const int FAIL_VOICE_ID = 13;

    /// <summary>再生するボイスを決定するクラス </summary>
    EnemyVoice _enemyVoice => GetComponent<EnemyVoice>();
    /// <summary>GOOD・PERFECT判定時に再生するサウンドID </summary>
    int _voiceSuccessID = SUCCESS_VOICE_ID;
    /// <summary>BAD判定時に再生するサウンドID </summary>
    int _voiceFailureID = FAIL_VOICE_ID;

    protected override void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, 2)
            .SetDelay(0.3f)
            .OnComplete(() => Destroy(gameObject));

        //透明になりながら消えていくパターン
        Array.ForEach(EnemySprites, s => 
        s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: _currentParameter.FadeSpeed));

        GiveDamageRun();    //ダメージを与える
        StageScrollRun();        //ステージスクロールを行う

        AudioManager.Instance.PlayVoice(_voiceFailureID);
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

        AudioManager.Instance.PlayVoice(_voiceSuccessID);
        AudioManager.Instance.PlaySE(6, 0.7f);
    }

    protected override void PerfactEffect()
    {
        _anim.Play("Effect");
        Array.ForEach(EnemySprites, e =>
        {
            e.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear)
           .SetLoops(-1, LoopType.Restart);
        });

        AudioManager.Instance.PlayVoice(_voiceSuccessID);
        AudioManager.Instance.PlaySE(6, 0.7f);
    }

    public override void SetUp(IState currentGameState, EnemyInfo info)
    {
        if (currentGameState is BossTime)   //ボスステージ用のボイスを再生する為
        {
            var voiceID = _enemyVoice.GetBossTimeVoiceID();

            _voiceSuccessID = voiceID[0];
            _voiceFailureID = voiceID[1];
        }

        base.SetUp(currentGameState, info);
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
