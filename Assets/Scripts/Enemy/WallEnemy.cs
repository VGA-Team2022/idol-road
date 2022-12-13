using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�ǂɂȂ�t�@���̏������s���N���X</summary>
public class WallEnemy : EnemyBase
{
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
        Destroy(gameObject);
        // throw new System.NotImplementedException();
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //�_���[�W��^����
        Destroy(gameObject);
    }

    protected override void PerfactEffect()
    {
        Destroy(gameObject);
        // throw new System.NotImplementedException();
    }
}
