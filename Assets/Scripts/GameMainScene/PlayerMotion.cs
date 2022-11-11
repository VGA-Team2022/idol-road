using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Player�̓������~�߂���A�ĊJ�������肵�ăA�N�V������������T�|�[�g������X�N���v�g
/// </summary>
public class PlayerMotion : MonoBehaviour
{
    /// <summary>PlayerController�̕ϐ� </summary>
    PlayerController controller;

    /// <summary> PlayerController�ŃV���A���C�Y�����l���i�[�����邽�߂̕ϐ�</summary>
    float _newScroolSpeed ;

    void Start()
    {
        // �A�T�C������Ă�v���C���[�X�N���v�g�������Ă���iPlayerController.cs�𓮂����̂ɕK�{)
        controller = GetComponent<PlayerController>();

        // PlayerController�ɃV���A���C�Y�����l���i�[�B
        _newScroolSpeed = controller.ScrollSpeed;
    }

    // �������~�߂������ɂ��̊֐����O���Ɏ����Ă����悤�Ƀ��\�b�h�\�z
    public void StopMotion()
    {
        controller.ScrollSpeed = 0;
    }

    // �������ĊJ�������ɂ��̊֐����O���Ɏ����Ă����悤�Ƀ��\�b�h�\�z
    public void ResumeMotion()
    {
        controller.ScrollSpeed = _newScroolSpeed;
    }
}
