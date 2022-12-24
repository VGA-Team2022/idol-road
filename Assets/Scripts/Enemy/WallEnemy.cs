using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�ǂɂȂ�t�@���̏������s���N���X</summary>
[RequireComponent(typeof(EnemyVoice))]
public class WallEnemy : EnemyBase
{
    /// <summary>�������{�C�X��ID</summary>
    const int SUCCESS_VOICE_ID = 5;
    /// <summary>���s���{�C�X��ID </summary>
    const int FAIL_VOICE_ID = 13;

    /// <summary>�Đ�����{�C�X�����肷��N���X </summary>
    EnemyVoice _enemyVoice => GetComponent<EnemyVoice>();
    /// <summary>GOOD�EPERFECT���莞�ɍĐ�����T�E���hID </summary>
    int _voiceSuccessID = SUCCESS_VOICE_ID;
    /// <summary>BAD���莞�ɍĐ�����T�E���hID </summary>
    int _voiceFailureID = FAIL_VOICE_ID;

    protected override void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, 2)
            .SetDelay(0.3f)
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        Array.ForEach(EnemySprites, s => 
        s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: _currentParameter.FadeSpeed));

        GiveDamageRun();    //�_���[�W��^����
        StageScrollRun();        //�X�e�[�W�X�N���[�����s��

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
        if (currentGameState is BossTime)   //�{�X�X�e�[�W�p�̃{�C�X���Đ������
        {
            var voiceID = _enemyVoice.GetBossTimeVoiceID();

            _voiceSuccessID = voiceID[0];
            _voiceFailureID = voiceID[1];
        }

        base.SetUp(currentGameState, info);
    }

    /// <summary>
    /// ������уA�j���[�V�������I�������玩�g���폜����
    /// �A�j���[�V�����g���K�[�ŌĂяo��
    /// </summary>
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
