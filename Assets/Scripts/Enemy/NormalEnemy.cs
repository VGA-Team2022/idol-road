using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�ʏ�t�@���̏������s���N���X</summary>
public class NormalEnemy : EnemyBase
{
    /// <summary>������Ԃ܂ł̒x�� </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("������΂��Ă��鎞��"), Range(0.1f, 10f)]
    float _deadMoveTime = 1f;
    [SerializeField, Header("������񂾎��̃T�C�Y"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
   
    /// <summary>������ԉ��o�ōĐ�����A�j���[�V�����̖��O</summary>
    string _playAnimName = "";

    protected override void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        Array.ForEach(EnemySprites, s => s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));
        
        StageScrollRun();        //�X�e�[�W�X�N���[�����s��

        AudioManager.Instance.PlayVoice(12);
    }

    protected override void GoodEffect()
    {
        _anim.Play(_playAnimName);
    
        //��] �������[�v���s����
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(2);
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //�_���[�W��^����
        Destroy(gameObject);

        AudioManager.Instance.PlayVoice(11);
    }

    protected override void PerfactEffect()
    {
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        EnemySprites[0].transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(4);
    }

    public override void SetUp(IState currentGameState)
    {
        base.SetUp(currentGameState);

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
