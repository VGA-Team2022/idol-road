using UnityEngine;

/// <summary>���E�̃t�@���𑀍삷��N���X </summary>
[RequireComponent(typeof(Animator))]
public class FanController : MonoBehaviour
{
    /// <summary>�A�j���[�V�����̌� </summary>
    const int ANIMATION_COUNT = 3;
    /// <summary>�A�j���[�V�������Đ�����܂ł̍ŏ��҂����� </summary>
    [SerializeField,Header("�A�j���[�V�������Đ�����܂ł̍ŏ��҂�����")]
    float _minNextAnimationTime = 5f;
    /// <summary>�A�j���[�V�������Đ�����܂̍ő�҂����� </summary>
    [SerializeField,Header("�A�j���[�V�������Đ�����܂̍ő�҂�����")]
    float _maxNextAnimationTime = 10f;

    [SerializeField, Header("���]���邩�ǂ���")]
    bool _isReverse = default;
    [ElementNames(new string[] { "�n��1", "�n��2", "JK", "�C�P����", "���K�l", "��1�j", "��2�j", "��1��", "��2��", "���~" })]
    [SerializeField, Header("�t�@���C���X�g")]
    Sprite[] _fanSprites = default;
    [ElementNames(new string[] { "�n��1", "�n��2", "JK", "�C�P����", "���K�l", "��1�j", "��2�j", "��1��", "��2��", "���~" })]
    [SerializeField, Header("�t�@���C���X�g ���]")]
    Sprite[] _fanSpritesReverse = default;

    float _nextAnimationTime = 0f;

    float _timer = 0f;

    SpriteRenderer _spriteRenderer => transform.GetChild(0).GetComponent<SpriteRenderer>();

    Animator _anim => transform.GetComponent<Animator>();

    private void Awake()
    {
        ChangeSprites();

        _nextAnimationTime = Random.Range(_minNextAnimationTime, _maxNextAnimationTime);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_nextAnimationTime <= _timer)
        {
            PlayRandomAnimation();
            _nextAnimationTime = Random.Range(_minNextAnimationTime, _maxNextAnimationTime);
            _timer = 0f;
        }
    }

    /// <summary>�C���X�g��ύX���� </summary>
    void ChangeSprites()
    {
        var rand = Random.Range(0, _fanSprites.Length);

        if (_isReverse)
        {
            _spriteRenderer.sprite = _fanSpritesReverse[rand];
        }
        else
        {
            _spriteRenderer.sprite = _fanSprites[rand];
        }
    }

    /// <summary>�����_���ň�A�j���[�V�������Đ�����</summary>
    void PlayRandomAnimation()
    {
        var rand = Random.Range(0, ANIMATION_COUNT);

        switch ((AnimationType)rand)
        {
            case AnimationType.Jump:
                if (_isReverse)
                {
                    _anim.SetTrigger("JumpRight");
                }
                else if(!_isReverse)
                {
                    _anim.SetTrigger("JumpLeft");
                }
                break;
            case AnimationType.Spin:
                if (_isReverse)
                {
                    _anim.SetTrigger("SpinRight");
                    
                }
                else
                {
                    _anim.SetTrigger("SpinLeft");
                }
                break;
            case AnimationType.Move:
                if (_isReverse)
                {
                    _anim.SetTrigger("MoveRight");

                }
                else
                {
                    _anim.SetTrigger("MoveLeft");
                }
                break;
        }
    }

    /// <summary>�A�j���[�V�����̎�� </summary>
    enum AnimationType
    {
        /// <summary>�W�����v </summary>
        Jump = 0,
        /// <summary>��] </summary>
        Spin = 1,
        /// <summary>�ړ� </summary>
        Move = 2,
    }
}
