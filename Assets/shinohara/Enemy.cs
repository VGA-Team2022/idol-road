using System;
using DG.Tweening;
using UnityEngine;

/// <summary>�G�l�~�[���Ǘ�����N���X </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>������Ԃ܂ł̒x�� </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("���݂̈ړ����@")]
    MoveMethod _currentMoveMethod = MoveMethod.Path;
    [SerializeField, Header("�|�������̃X�R�A")]
    int _addScoreValue = 1;
    [SerializeField, Header("�ړ��ɂ����鎞��"), Range(0.1f, 10f)]
    float _moveTime = 1f;
    [SerializeField, Header("������񂾎��̃T�C�Y"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    GameObject _explosionEffect = default;
    /// <summary>�|���ꂽ���̐�����ԋO�����\������|�C���g�̔z�� </summary>
    Vector3[] _deadMovePoints = default;
    /// <summary>�X�R�A�𑝂₷Action </summary>
    event Action<int> _addScore = default;

    /// <summary>�X�R�A�𑝂₷Action </summary>
    public event Action<int> AddScore
    {
        add { _addScore += value; }
        remove { _addScore -= value; }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dead();
        }
    }

    /// <summary> ������ԉ��o�i�ړ��j </summary>
    void DeadMove()
    {
        if (_currentMoveMethod == MoveMethod.Path)
        {
            //�ړ�����
            transform.DOPath(path: _deadMovePoints, duration: _moveTime, pathType: PathType.CatmullRom)
                .SetDelay(EXPLOSION_DELAY)
                .OnComplete(() => Destroy(gameObject));
        }
        else if (_currentMoveMethod == MoveMethod.Jump)
        {
            transform.DOJump(_deadMovePoints[_deadMovePoints.Length - 1], jumpPower: 1f, numJumps: 1, duration: _moveTime)
                .SetDelay(EXPLOSION_DELAY)
                .OnComplete(() => Destroy(gameObject));
        }

        //�傫������
        transform.DOScale(new Vector3(_minScale, _minScale, _minScale),_moveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _addScore.Invoke(_addScoreValue));
    }

    /// <summary>�|���ꂽ���̏��� </summary>
    public void Dead()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //�����G�t�F�N�g�𐶐�
        DeadMove();
    }

    /// <summary>�|���ꂽ���̋O���̃|�C���g���擾���� </summary>
    /// <param name="pointParent">�O���̃|�C���g���q�I�u�W�F�N�g�Ɏ��I�u�W�F�N�g(�e�I�u�W�F)</param>
    public void SetDeadMovePoints(Transform pointParent)
    {
        _deadMovePoints = new Vector3[pointParent.childCount];

        for (var i = 0; i < _deadMovePoints.Length; i++)
        {
            _deadMovePoints[i] = pointParent.GetChild(i).position;
        }
    }

    /// <summary>�ړ����@ </summary>
    public enum MoveMethod
    {
        /// <summary>�ړ��ʒu���w�肷����@ </summary>
        Path,
        /// <summary>�ŏI�I�Ȉʒu�ɃW�����v������@ </summary>
        Jump,
    }
}