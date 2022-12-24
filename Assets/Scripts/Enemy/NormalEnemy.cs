using System;
using UnityEngine;
using DG.Tweening;

/// <summary>通常ファンの処理を行うクラス</summary>
[RequireComponent(typeof(EnemyVoice))]
public class NormalEnemy : EnemyBase
{
    /// <summary>成功・失敗時のIDを格納する為 </summary>
    const int VOICE_ID_SIZE = 2;
    /// <summary>吹き飛ぶまでの遅延 </summary>
    const float EXPLOSION_DELAY = 0.3f;
    /// <summary>再生するボイスを決定するクラス </summary>
    EnemyVoice _enemyVoice => GetComponent<EnemyVoice>();
    /// <summary>吹き飛ぶ演出で再生するアニメーションの名前</summary>
    string _playAnimName = "";
    /// <summary>GOOD・PERFECT判定時に再生するサウンドID </summary>
    int _voiceSuccessID = 0;
    /// <summary>BAD判定時に再生するサウンドID </summary>
    int _voiceFailureID = 0;

    protected override void BadEffect()
    {
        //横移動
        transform.DOMoveX(-3, 2)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //透明になりながら消えていくパターン
        Array.ForEach(EnemySprites, s => s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));
        
        StageScrollRun();        //ステージスクロールを行う

        AudioManager.Instance.PlayVoice(_voiceFailureID);
    }

    protected override void GoodEffect()
    {
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        EnemySprites[0].transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(_voiceSuccessID);
        AudioManager.Instance.PlaySE(6, 0.7f);
    }

    protected override void PerfactEffect()
    {
        _anim.Play(_playAnimName);

        //回転 無限ループを行う為
        EnemySprites[0].transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(_voiceSuccessID);
        AudioManager.Instance.PlaySE(6, 0.7f);
    }

    public override void SetUp(IState currentGameState, EnemyInfo info)
    {
        var voiceID = new int[VOICE_ID_SIZE];
        var nomalType = EnemySprites[0].ChangeNomalEnemySprite();   //イラスト変更

        if (currentGameState is BossTime)   //ボスステージ用のボイスを再生する為
        {
            voiceID = _enemyVoice.GetBossTimeVoiceID();
        }
        else
        {
            voiceID = _enemyVoice.GetNormalEnemyVoiceID(nomalType);    //0=成功ボイス 1=失敗ボイス
        }

        //ボイスを設定
        _voiceSuccessID = voiceID[0];
        _voiceFailureID = voiceID[1];

        base.SetUp(currentGameState, info);

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
