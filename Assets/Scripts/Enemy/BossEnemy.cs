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

    private void Update()
    {
        if (_isMove && !_isdead)
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

           AudioManager.Instance.PlayVoice(8);
    }

    protected override void GoodEffect()
    {
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(1);
        AudioManager.Instance.PlaySE(9);

    }

    protected override void PerfactEffect()
    {
        _anim.Play(_playAnimName);

        //��] �������[�v���s����
        _bossSprite.transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        AudioManager.Instance.PlayVoice(1);
        AudioManager.Instance.PlaySE(9);

    }

    public override void Setup(IState currentGameState, EnemyInfo info)
    {
        base.Setup(currentGameState, info);

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

        _isMove = true;
    }

    /// <summary>�ړ��A�j���[�V�������Đ����� </summary>
    public void StartMoveAnim()
    {
        _anim.Play("Walk");
        AudioManager.Instance.PlaySE(17);
    }

    /// <summary>
    /// ������уA�j���[�V�������I�������玩�g���폜����
    /// �A�j���[�V�����g���K�[�ŌĂяo��
    /// </summary>
    public void ThisDestroy()
    {
        _gameClear?.Invoke();
        Destroy(gameObject);
    }
}
