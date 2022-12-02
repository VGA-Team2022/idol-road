using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�A�C�h���p���[�𑝌�������A�C�e���̃N���X</summary>
public class IdolPowerItem : MonoBehaviour
{
    [SerializeField, Header("�A�C�e���擾���ɑ�������l")]
    int _getAmount = 0;
    [SerializeField, Header("�ړ��ɂ����鎞��")]
    float _duration = 2f;
    [SerializeField, Header("�O���̍���")]
    float _jumpPower = 4f;
    private GameManager _gameManager;
    event Action _scrollStart = default;
    /// <summary>�Q�[���}�l�[�W���[�̃v���p�e�B</summary>
    public GameManager GameManager { get => _gameManager; set => _gameManager = value; }

    /// <summary>�ړ����� </summary>
    /// <param name="targetPoint">�����ʒu</param>
    /// /// <param name="scrollStart">�ړ��������I��������X�N���[��������</param>
    public void Move(Vector3 targetPoint)
    {
        transform.DOJump(targetPoint, jumpPower: _jumpPower, numJumps: 1, duration: _duration)
            .OnComplete(() => Destroy(this.gameObject));
    }

    //TODO:�A�C�e�����ʂ̒ǉ�
    /// <summary>�A�C�e���擾��</summary>
    public void GetItem()
    {
        _gameManager.IdlePower += _getAmount;
        AudioManager.Instance.PlaySE(16, 0.5f);
        Destroy(gameObject);
    }
}
