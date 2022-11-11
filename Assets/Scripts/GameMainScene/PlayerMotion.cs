using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Player�̓������~�߂���A�ĊJ�������肵�ăA�N�V������������T�|�[�g������X�N���v�g
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    [SerializeField , Tooltip("�A�C�h���̃��[�V�����G")]
    Sprite[] _sprites;

    [SerializeField, Tooltip("���[�V�����𑱂��鎞��")]
    float _motionTime = 3;

    [SerializeField , Tooltip("���͂𑗂��Ă����")]
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
        else if(_flickType != FlickType.None)
        {
            _timer += Time.deltaTime;
            if (_timer > _motionTime) 
            {
                _flickType = FlickType.None;
                _spriteRenderer.sprite = _sprites[0];
                _timer = 0;
            }
        }
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
