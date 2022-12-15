using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�{�X���Ǘ�����N���X </summary>
public class BossEnemy : EnemyBase
{
    /// <summary>������Ԃ܂ł̒x�� </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("������΂��Ă��鎞��"), Range(0.1f, 10f)]
    float _deadMoveTime = 1f;
    [SerializeField, Header("������񂾎��̃T�C�Y"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("��ԕ��� 0=left 1=right 2=up�EDown"), ElementNames(new string[] { "Left", "Right", "Up�EDown" })]
    Transform[] _trajectoryParent = default;

    /// <summary>�ړ������ǂ���</summary>
    bool _isMove = false;

    /// <summary>�|���ꂽ���̐�����ԋO�����\������|�C���g�̔z�� </summary>
    Vector3[] _deadMovePoints = default;

    private void Start()
    {
       
    }

    private void Update()
    {
        if (_isMove)
        {
            UpdateResultTime();
        }
    }

    protected override void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        Array.ForEach(EnemySprites, s => s.GetComponent<SpriteRenderer>()
        .DOFade(endValue: 0, duration: 2.0f).OnComplete(GiveDamageRun));
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

    /// <summary>�ړ����J�n����</summary>
    public void MoveStart()
    {
        Debug.Log("Hello");
        _rb.AddForce(-transform.forward * _enemySpped); //�t�@����O�Ɉړ�������
        _isMove = false;
        FlickNum(); //�����_���Ńt���b�N�������擾����
    }
}
