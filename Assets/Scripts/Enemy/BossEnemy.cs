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
    [SerializeField, Tooltip("�C���X�g")]
    SpriteRenderer _bossSprite = default;
    /// <summary>�Q�[���N���A���̏��� </summary>
    event Action _gameClear = default;
    /// <summary>������ԉ��o�ōĐ�����A�j���[�V�����̖��O</summary>
    string _playAnimName = "";

    /// <summary>�ړ������ǂ���</summary>
    bool _isMove = false;

    public SpriteRenderer BossSprite { get => _bossSprite; }

    /// <summary>�Q�[���N���A���̏��� </summary>
    public event Action GameClear
    {
        add { _gameClear += value; }
        remove { _gameClear -= value; }
    }

    private void Start()
    {
        _time = _resultTimes[_resultTimeIndex];
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
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
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
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>�ړ����J�n����</summary>
    public void MoveStart()
    {
        _rb.AddForce(-transform.forward * _enemySpped); //�t�@����O�Ɉړ�������
        _isMove = true;
        FlickNum();

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

        _requestUIArray[0].gameObject.SetActive(true);
    }

    /// <summary>
    /// ������уA�j���[�V�������I�������玩�g���폜����
    /// �A�j���[�V�����g���K�[�ŌĂяo��
    /// </summary>
    public void ThisDestroy()
    {
        _gameClear?.Invoke();
        Destroy(this.gameObject);
    }
}
