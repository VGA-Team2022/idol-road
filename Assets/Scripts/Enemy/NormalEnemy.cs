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
    [SerializeField, Tooltip("��ԕ��� 0=left 1=right 2=up�EDown"), ElementNames(new string[] { "Left", "Right", "Up�EDown" })]
    Transform[] _trajectoryParent = default;

    /// <summary>�|���ꂽ���̐�����ԋO�����\������|�C���g�̔z�� </summary>
    Vector3[] _deadMovePoints = default;

    protected override void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        _sr.DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun);

        StageScrollRun();        //�X�e�[�W�X�N���[�����s��
    }

    protected override void GoodEffect()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOPath(path: _deadMovePoints, duration: _deadMoveTime, pathType: PathType.CatmullRom))
            .Join(transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _deadMoveTime))
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //��] �������[�v���s����
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    protected override void OutEffect()
    {
        GiveDamageRun(); //�_���[�W��^����
        Destroy(gameObject);
    }

    protected override void PerfactEffect()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(transform.DOPath(path: _deadMovePoints, duration: _deadMoveTime, pathType: PathType.CatmullRom))
            .Join(transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _deadMoveTime))
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //��] �������[�v���s����
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    public override void SetUp(IState currentGameState)
    {
        base.SetUp(currentGameState);

        var points = transform;     //�ꎞ�I�ɋO���̐e�I�u�W�F�N�g��ێ�����ׂ̕ϐ�

        switch (_currentRequest)    //�e�t�@���T�Ő�����ԕ��������߂�
        {
            case FlickType.Left:
                points = _trajectoryParent[0];
                break;
            case FlickType.Right:
                points = _trajectoryParent[1];
                break;
            case FlickType.Up:
            case FlickType.Down:
                points = _trajectoryParent[2];
                break;
        }

        _deadMovePoints = new Vector3[points.childCount - 1];

        for (var i = 0; i < _deadMovePoints.Length; i++)
        {
            _deadMovePoints[i] = points.GetChild(i).position;
        }
    }
}
