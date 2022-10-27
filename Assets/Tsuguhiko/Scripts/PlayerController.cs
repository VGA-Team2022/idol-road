using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Play����Player(�J�����t��)�̓��쐧��
/// </summary>

public class PlayerController : MonoBehaviour
{

    [SerializeField] GameObject _playerObj;

    //[Header("���[�h")]
    //[SerializeField] ScrollMode _scrollMode;

    [Header("�X�N���[���̑���")]
    [SerializeField] float _scrollSpeed;

    Transform _objPos;

    PlayerMotion playerMotion;

    // �J�E���g�_�E���I������Update�֐����񂷔���
    bool _isUpdated = false;

    public float ScrollSpeed { get { return _scrollSpeed; } set { _scrollSpeed = value; } }

    void Start()
    {
        playerMotion = GetComponent<PlayerMotion>();

        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(4.0f);

        _isUpdated = true;
    }
    void Update()
    {
        if (_isUpdated)
        {
            _objPos = this.transform;

            Vector3 movepos = _objPos.position;

            movepos.y += _scrollSpeed;

            //if (movepos.y >= 9.062126f)
            //{
            //    isPassed = true;

            //    _objPos.position = movepos;
            //}

            _objPos.position = movepos;

            // �{�^��������������ɊO���̊֐������s�ł���悤�ɐݒ�
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerMotion.StopMotion();
            }
            // �{�^��������������ɊO���̊֐������s�ł���悤�ɐݒ�
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerMotion.ResumeMotion();
            }
        }
    }
}

//[Serializable]
//enum ScrollMode
//{
//    Anime,
//    Trans
//}
