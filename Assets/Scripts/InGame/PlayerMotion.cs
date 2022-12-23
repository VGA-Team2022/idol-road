using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
///  Player�̓������~�߂���A�ĊJ�������肵�ăA�N�V������������T�|�[�g������X�N���v�g
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    [SerializeField, Tooltip("�A�C�h���̃��[�V�����G")]
    Sprite[] _sprites;
    [SerializeField, Tooltip("�A�C�h���̃t�@���T�����o��"), ElementNames(new string[] { "�����L�X", "�E�B���N" })]
    GameObject[] _IdleBlowing;
    [SerializeField, Tooltip("�����o�����o��ꏊ")]
    GameObject _blowingSpawn = default;
    [SerializeField, Tooltip("���[�V�����𑱂��鎞��")]
    float _motionTime = 3;
    [SerializeField, Tooltip("�t�@���T�̐����o����\�����鎞��")]
    float _fansaShowTime = 0.5f;
    [SerializeField, Tooltip("���͂𑗂��Ă����")]
    private ScreenInput _screenInput;

    private SpriteRenderer _spriteRenderer;

    private FlickType _flickType = FlickType.None;

    private bool _gameStop = false;

    private float _timer = 0;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_gameStop) { return; }

        FlickType flickType = _screenInput.GetFlickType();

        if (_flickType != flickType && flickType != FlickType.None && flickType != FlickType.Tap)
        {
            int index = (int)flickType;

            //index������Ȃ�����
            _spriteRenderer.sprite = _sprites[index];

            _flickType = flickType;

            _timer = 0;
        }
        else if (_flickType != FlickType.None)
        {
            _timer += Time.deltaTime;
            if (_timer > _motionTime)
            {
                _flickType = FlickType.None;
                _spriteRenderer.sprite = _sprites[0];
                _timer = 0;
            }
        }
        switch (_flickType)
        {
            case FlickType.Down:
                var kissIllust = Instantiate(_IdleBlowing[0], _blowingSpawn.transform);
                Destroy(kissIllust, _fansaShowTime);
                break;
            case FlickType.Right:
                var winkIllust = Instantiate(_IdleBlowing[1], _blowingSpawn.transform);
                Destroy(winkIllust, _fansaShowTime);
                break;
        }
    }

    /// <summary>�Q�[���N���A���Ƀ^�N�V�[�Ɍ����킹�鏈�� </summary>
    public void GameClearMove()
    {
        transform.DOMoveZ(5, 30f);
    }

    // �������~�߂������ɂ��̊֐����O���Ɏ����Ă����悤�Ƀ��\�b�h�\�z
    public void StopMotion()
    {
        _gameStop = true;
    }

    // �������ĊJ�������ɂ��̊֐����O���Ɏ����Ă����悤�Ƀ��\�b�h�\�z
    public void ResumeMotion()
    {
        _gameStop = false;
    }
}
