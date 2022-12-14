using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�ǂɂȂ�t�@���̏������s���N���X</summary>
public class WallEnemy : EnemyBase
{
    /// <summary>������їp�A�j���[�V�����R���g���[���[ </summary>
    Animator _anim => GetComponent<Animator>();

    protected override void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, 2)
            .SetDelay(0.3f)
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        Array.ForEach(EnemySprites, s => 
        s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));

        StageScrollRun();        //�X�e�[�W�X�N���[�����s��
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
        GiveDamageRun(); //�_���[�W��^����
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
    /// ������уA�j���[�V�������I�������玩�g���폜����
    /// �A�j���[�V�����g���K�[�ŌĂяo��
    /// </summary>
    public void ThisDestroy()
    {
        Destroy(this.gameObject);
    }
}
