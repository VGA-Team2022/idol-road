using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// �t���b�N�̕�����^�b�v�����擾�ł���
/// </summary>

public class ScreenInput : MonoBehaviour
{
    [SerializeField, Tooltip("�t���b�N�̔���ɕK�v�ȑ��싗��")]
    Vector2 _flickMinRange = new Vector2(0f, 0f);


    //����ɒǉ�
    //==========================================
    [SerializeField, Tooltip("audioManager")]
    SEPlay _sePlay = default;
    [SerializeField, Tooltip("���͕�����\������e�L�X�g")]
    Text _inputText = default;
    [SerializeField]
    GameManager _manager = default;
    //==========================================

    /// <summary>
    /// ���݂̃t���b�N�����Ȃǂ�ۑ�����
    /// </summary>
    FlickType _flickType = FlickType.None;

    /// <summary>
    /// ���߂ɐG�ꂽ�A�������̓X���C�v�������ۑ�����
    /// </summary>
    Vector2 _StartPosition = new Vector2();

    /// <summary>
    /// �w�𗣂����ꏊ��ۑ�����
    /// </summary>
    Vector2 _EndPosition = new Vector2();

    void Update()
    {
        InputFrickGet();
    }

    /// <summary>
    /// ��ʂ�G���Ă��邩���擾����
    /// </summary>
    private void InputFrickGet()
    {
        //Unity��ł̃f�o�b�N�p
        if (UnityEngine.Application.isEditor)
        {
            //�������ꏊ��ۑ�
            if (Input.GetMouseButtonDown(0))
            {
                ResetParameter();
                _StartPosition = Input.mousePosition;
            }//�X���C�v�����ꏊ��ۑ�
            else if (Input.GetMouseButton(0))
            {
                 _StartPosition = Input.mousePosition;
            }
            //�������ꏊ��ۑ�
            else if (Input.GetMouseButtonUp(0))
            {
                _EndPosition = Input.mousePosition;
                Frickdirection();
            }
            //���͂��Ȃ��ꍇ�̓��Z�b�g����
            else if (_flickType != FlickType.None || _flickType != FlickType.None)
            {
                ResetParameter();
            }
        }
        else //�X�}�z�p
        {   
            //��{�ȏ�w���G�ꂽ��
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];

                switch (touch.phase)
                {
                    //�������A�������̓X���C�v�����ꏊ��ۑ�
                    case TouchPhase.Began:
                        ResetParameter();
                        _StartPosition = touch.position;
                        break;
                    case TouchPhase.Moved:
                        _StartPosition = touch.position;
                        break;
                    //�������ꏊ��ۑ�
                    case TouchPhase.Ended:
                        _EndPosition = touch.position;
                        Frickdirection();
                        break;
                }
            }//���͂��Ȃ��ꍇ�̓��Z�b�g����
            else if (_flickType != FlickType.None || _flickType != FlickType.None)
            {
                ResetParameter();
            }
        }
    }

    /// <summary>
    /// �G�ꂽ�ʒu�֌W����^�b�v���t���b�N�����肷��
    /// </summary>
    private void Frickdirection()
    {
        Vector2 flickVector = new Vector2((new Vector3(_EndPosition.x, 0 , 0) - new Vector3(_StartPosition.x, 0 , 0)).magnitude,
                                          (new Vector3(0 , _EndPosition.y, 0) - new Vector3(0, _StartPosition.y , 0)).magnitude);

        if (flickVector.x <= _flickMinRange.x && flickVector.y <= _flickMinRange.y)
        {
            _flickType = FlickType.Tap;
        }
        else if (flickVector.x > flickVector.y)
        {
            if (Mathf.Sign(_EndPosition.x - _StartPosition.x) > 0)
            {
                _flickType = FlickType.Right;
            }
            else { _flickType = FlickType.Left; }
        }
        else
        {
            if (Mathf.Sign(_EndPosition.y - _StartPosition.y) > 0)
            {
                _flickType = FlickType.Up;
            }
            else { _flickType = FlickType.Down; }
        }


        //����ɒǉ�
        if (_manager.CurrentEnemy != null)
        {
            _manager.CurrentEnemy.Dead();
            _sePlay.SEShot(_flickType);
        }
       
        _inputText.text = _flickType.ToString();
    }

    /// <summary>
    /// �ϐ��̏������p
    /// </summary>
    private void ResetParameter()
    {
        _flickType = FlickType.None;
    }

    public FlickType GetFlickType() 
    {
        return _flickType;
    }
}

public enum FlickType
{
    None,
    Tap,
    Up,
    Down,
    Right,
    Left
}
