using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
/// <summary>�G�l�~�[���Ǘ�����N���X </summary>
public class Enemy : MonoBehaviour
{
    /// <summary>������Ԃ܂ł̒x�� </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("���݂̈ړ����@")]
    MoveMethod _currentMoveMethod = MoveMethod.Path;
    [SerializeField, Tooltip("�t�@������������"), Header("�t�@���֌W")]
    Vector3 _enemySpped;
    [SerializeField, Tooltip("�t�@���T��v�����鐔")]
    int _fansaNum = 1;
    [SerializeField, Tooltip("�t�@����out�ɂȂ����Ƃ��̓����ɂȂ鑬�x")]
    float _fadedSpeed = 0.01f;
    [SerializeField, Tooltip("���Y����������邽�߂̎���(�f�o�b�O�p)"), Header("���Y���֌W")]
    float _time = default;
    [SerializeField, Tooltip("���Y������̕b��")]
    float _perfect, _good, _bad, _out;
    [SerializeField, Header("�|�������̃X�R�A")]
    int _addScoreValue = 1;
    [SerializeField, Header("�ړ��ɂ����鎞��"), Range(0.1f, 10f)]
    float _moveTime = 1f;
    [SerializeField, Header("������񂾎��̃T�C�Y"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    GameObject _explosionEffect = default;
    [SerializeField]
    EnemySpawn _enemySpawn = default;
    /// <summary>�|���ꂽ���̐�����ԋO�����\������|�C���g�̔z�� </summary>
    Vector3[] _deadMovePoints = default;
    /// <summary>FlickType��ۑ������Ă����ϐ� </summary>
    public FlickType _flickTypeEnemy;
    /// <summary>�A�E�g���ǂ����̔��������t���O</summary>
    public bool _isOut;
    /// <summary>�X�R�A�𑝂₷Action </summary>
    event Action<int> _addScore = default;
    /// <summary>�|���ꂽ��X�e�[�W�X�N���[�����J�n���� </summary>
    event Action _stageScroll = default;
    Rigidbody _rb;
    SpriteRenderer _sr;
    /// <summary>�G���|�ꂽ�ۂɂ�����DoTween�ł̓����̎��Ԃ̃v���p�e�B</summary>
    public float MoveTime { get => _moveTime; set => _moveTime = value; }
    /// <summary>�X�R�A�𑝂₷Action </summary>
    public event Action<int> AddScore
    {
        add { _addScore += value; }
        remove { _addScore -= value; }
    }

    public event Action StageScroll
    {
        add {_stageScroll += value; }
        remove { _stageScroll -= value; }
    }
    private void Start()
    {
        for(int i = 0; i < _fansaNum; i++)
        {
            FlickNum(); //�����_���Ńt���b�N�������擾����
        }
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_enemySpped); //�t�@����O�ɓ������i��)
        _isOut = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Dead();
        }
        _time -= Time.deltaTime;// ���Y������p

        if(_time <= _out) //_out�𒴂������΂Ȃ��悤��bool�ŊǗ�
        {
            _isOut = true;
            StartFade();
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
                .OnComplete(() => _addScore.Invoke(_addScoreValue))
                .OnComplete(() => Destroy(gameObject));
        }

        //�傫������
        transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _moveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _stageScroll?.Invoke());

        //��]
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    void StartFade()
    {
        _sr.color -= new Color(0, 0, 0, _fadedSpeed); //���X�ɓ����x�������Ă���
        if(_sr.color.a <= 0)//�����ɂȂ��������
        {
            Debug.Log("out");
            Destroy(gameObject);
        }
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
    /// <summary>Flick��������������_���Ɏ擾����</summary>
    public void FlickNum()
    {
        var rnd = new System.Random();
        _flickTypeEnemy = (FlickType)rnd.Next(2, 5);        
        Debug.Log(_flickTypeEnemy);
    }
    /// <summary>���Y������p</summary>
    public void JugeTime()
    {
        if (_time <= _out)
        {
            Debug.Log("out");
        }
        else if (_time <= _perfect)
        {
            Debug.Log($"perfect { _time:F1}");
        }
        else if (_time <= _good)
        {
            Debug.Log($"good { _time:F1}");
        }
        else if (_time <= _bad)
        {
            Debug.Log($"bad { _time:F1}");
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