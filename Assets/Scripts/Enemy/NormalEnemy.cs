using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�ʏ�t�@���̏������s���N���X</summary>
[RequireComponent(typeof(EnemyVoice))]
public class NormalEnemy : EnemyBase
{
    /// <summary>�����E���s����ID���i�[����� </summary>
    const int VOICE_ID_SIZE = 2;
    /// <summary>������Ԃ܂ł̒x�� </summary>
    const float EXPLOSION_DELAY = 0.3f;
    /// <summary>�Đ�����{�C�X�����肷��N���X </summary>
    EnemyVoice _enemyVoice => GetComponent<EnemyVoice>();
    /// <summary>������ԉ��o�ōĐ�����A�j���[�V�����̖��O</summary>
    string _playAnimName = "";
    /// <summary>GOOD�EPERFECT���莞�ɍĐ�����T�E���hID </summary>
    int _voiceSuccessID = 0;
    /// <summary>BAD���莞�ɍĐ�����T�E���hID </summary>
    int _voiceFailureID = 0;

    protected override void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, 2)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        Array.ForEach(EnemySprites, s => s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));
        
        StageScrollRun();        //�X�e�[�W�X�N���[�����s��

        AudioManager.Instance.PlayVoice(_voiceFailureID);
    }

    protected override void GoodEffect()
    {
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        EnemySprites[0].transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(_voiceSuccessID);
        AudioManager.Instance.PlaySE(6, 0.7f);
    }

    protected override void PerfactEffect()
    {
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        EnemySprites[0].transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(_voiceSuccessID);
        AudioManager.Instance.PlaySE(6, 0.7f);
    }

    public override void SetUp(IState currentGameState, EnemyInfo info)
    {
        var voiceID = new int[VOICE_ID_SIZE];
        var nomalType = EnemySprites[0].ChangeNomalEnemySprite();   //�C���X�g�ύX

        if (currentGameState is BossTime)   //�{�X�X�e�[�W�p�̃{�C�X���Đ������
        {
            voiceID = _enemyVoice.GetBossTimeVoiceID();
        }
        else
        {
            voiceID = _enemyVoice.GetNormalEnemyVoiceID(nomalType);    //0=�����{�C�X 1=���s�{�C�X
        }

        //�{�C�X��ݒ�
        _voiceSuccessID = voiceID[0];
        _voiceFailureID = voiceID[1];

        base.SetUp(currentGameState, info);

        switch (_currentRequest)    //�e�t�@���T�Ő�����ԕ��������߂�
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
    /// ������уA�j���[�V�������I�������玩�g���폜����
    /// �A�j���[�V�����g���K�[�ŌĂяo��
    /// </summary>
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
