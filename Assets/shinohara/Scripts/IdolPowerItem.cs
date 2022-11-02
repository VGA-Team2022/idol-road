using UnityEngine;
using DG.Tweening;

/// <summary>�A�C�h���p���[�𑝌�������A�C�e���̃N���X</summary>
public class IdolPowerItem : MonoBehaviour
{
    [SerializeField, Header("�ړ��ɂ����鎞��")]
    float _duration = 2f;
    [SerializeField, Header("�O���̍���")]
    float _jumpPower = 4f;
    [SerializeField, Header("�\������")]
    float _liveTime = 3f;

    /// <summary>�ړ����� </summary>
    /// <param name="targetPoint">�����ʒu</param>
    public void Move(Vector3 targetPoint)
    {
        transform.DOJump(targetPoint, jumpPower: _jumpPower, numJumps: 1, duration: _duration)
            .OnComplete(() => Destroy(gameObject, _liveTime));
    }

    //TODO:�A�C�e�����ʂ̒ǉ�
}
