using DG.Tweening;
using System;
using UnityEngine;

/// <summary>�G�l�~�[���Ǘ�����N���X </summary>
[RequireComponent(typeof(Rigidbody), typeof(SpriteRenderer), typeof(SpriteChange))]
public class Enemy : MonoBehaviour
{
    /// <summary>������Ԃ܂ł̒x�� </summary>
    const float EXPLOSION_DELAY = 0.3f;

    [SerializeField, Header("�������Ă��鑬�x"), Range(1, 50)]
    float _enemySpped;
    [SerializeField, Header("�����ɂȂ�܂łɂ����鎞��")]
    float _fadedSpeed = 1;
    [ElementNames(new string[] { "���v����", "Bad", "Good", "Perfect", "Out" })]
    [SerializeField, Header("���Y������̕b��"), Tooltip("0=���v���� 1=bad 2=good 3=perfect 4=out")]
    float[] _resultTimes = default;
    [SerializeField, Header("�|�������̃X�R�A")]
    int _addScoreValue = 1;
    [SerializeField, Header("������΂��Ă��鎞��"), Range(0.1f, 10f)]
    float _deadMoveTime = 1f;
    [SerializeField, Header("������񂾎��̃T�C�Y"), Range(0.1f, 1f)]
    float _minScale = 0.3f;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    GameObject _explosionEffect = default;
    [SerializeField, Tooltip("�t�@���T")]
    RequestUIController _requestUIController = null;
    [SerializeField, Tooltip("��ԕ��� 0=left 1=right 2=up�EDown"), ElementNames(new string[] { "Left", "Right", "Up�EDown" })]
    Transform[] _trajectoryParent = default;

    /// <summary>�|���ꂽ���̐�����ԋO�����\������|�C���g�̔z�� </summary>
    Vector3[] _deadMovePoints = default;
    /// <summary>FlickType��ۑ������Ă����ϐ� </summary>
    FlickType _flickTypeEnemy;
    /// <summary>������ю��̕]�� </summary>
    TimingResult _currentResult = TimingResult.None;
    /// <summary>�t�@���T��v�����鐔</summary>
    int _fansaNum = 1;
    /// <summary>�]������̎��Ԃ̓Y���� </summary>
    int _resultTimeIndex = 0;
    /// <summary>�G�̎��S�t���O</summary>
    bool _isdead = false;
    /// <summary>�]���ύX�p�ϐ�</summary>
    float _time = 0f;
    /// <summary>�X�R�A�𑝂₷Action </summary>
    event Action<int> _addScore = default;
    /// <summary>�|���ꂽ��X�e�[�W�X�N���[�����J�n���� </summary>
    event Action _stageScroll = default;
    /// <summary>�_���[�W��^����i�v���C���[�̗̑͂����炷�j</summary>
    event Action<int> _giveDamage = default;

    Rigidbody _rb => GetComponent<Rigidbody>();
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();
    /// <summary>�G�̃X�v���C�g���Ǘ�����N���X�̕ϐ� </summary>
    SpriteChange _spriteChange => GetComponent<SpriteChange>();

    /// <summary>�X�R�A�𑝂₷Action </summary>
    public event Action<int> AddScore
    {
        add { _addScore += value; }
        remove { _addScore -= value; }
    }

    /// <summary>�|���ꂽ��X�e�[�W�X�N���[�����J�n���� </summary>
    public event Action StageScroll
    {
        add { _stageScroll += value; }
        remove { _stageScroll -= value; }
    }

    /// <summary>�_���[�W��^����i�v���C���[�̗̑͂����炷�j</summary>
    public event Action<int> GiveDamage
    {
        add { _giveDamage += value; }
        remove { _giveDamage -= value; }
    }

    private void Start()
    {
        // _rb.AddForce(-transform.forward * _enemySpped); //�t�@����O�Ɉړ�������
        _spriteChange.EnemyRandomMethod(_sr);
        _time = _resultTimes[_resultTimeIndex];
    }

    private void Update()
    {


        if (!_isdead)
        {
            _time -= Time.deltaTime;// ���Y������p

            if (_time <= _resultTimes[_resultTimeIndex + 1])    //������ю��̕]�����X�V����
            {
                _resultTimeIndex++;
                UpdateCurrentResult();
            }

        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            JugeTime(FlickType.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            JugeTime(FlickType.Right);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            JugeTime(FlickType.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            JugeTime(FlickType.Down);
        }
    }

    /// <summary>������ю��̕]�����X�V,�����o�����X�V</summary>
    void UpdateCurrentResult()
    {
        switch (_currentResult)
        {
            case TimingResult.None:
                _currentResult = TimingResult.Bad;
                break;
            case TimingResult.Bad:
                _currentResult = TimingResult.Good;
                break;
            case TimingResult.Good:
                _currentResult = TimingResult.Perfect;
                break;
            case TimingResult.Perfect:
                _currentResult = TimingResult.Out;
                Debug.Log(_time);
                _sr.DOFade(endValue: 0, duration: _fadedSpeed)
                          .OnComplete(OutMove);
                _isdead = true;
                break;
        }

        _requestUIController.ChangeRequestWindow(_currentResult);
    }

    /// <summary> Bad, Out�ȊO�Ŏg�p���鐁����щ��o </summary>
    void DeadMove()
    {
        //�ړ�����
        transform.DOPath(path: _deadMovePoints, duration: _deadMoveTime, pathType: PathType.CatmullRom)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => Destroy(gameObject));


        //�傫������
        transform.DOScale(new Vector3(_minScale, _minScale, _minScale), _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _stageScroll?.Invoke());

        //��]
        transform.DOLocalRotate(new Vector3(0, 0, 360f), 0.1f, RotateMode.FastBeyond360)
            .SetDelay(EXPLOSION_DELAY)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);
    }

    /// <summary>�]��Bad�̂Ƃ���Enemy�̓���</summary>
    void BadMove()
    {
        //���ړ�
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            //���������Ă��������p�^�[��
         //   .OnComplete(()=>_sr.DOFade(endValue:0,duration:2.0f))
            .OnComplete(() => _stageScroll?.Invoke())
            .OnComplete(() => Destroy(gameObject));

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        _sr.DOFade(endValue: 0, duration: 2.0f);
        _isdead = true;

        //�X�P�[����������
        /*transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), _moveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(() => _stageScroll?.Invoke());*/
    }

    /// <summary>out���̂̏���</summary>
    void OutMove()
    {
        _giveDamage?.Invoke(_fansaNum); //�_���[�W��^����
        _stageScroll?.Invoke();         //�X�e�[�W�X�N���[�����s��
        Destroy(gameObject);
    }

    /// <summary>�|���ꂽ���̏��� </summary>
    public void Dead()
    {
        Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //�����G�t�F�N�g�𐶐�
        DeadMove();
        _isdead = true;
        AudioManager.Instance.PlaySE(6, 0.1f);
    }

    /// <summary>Flick��������������_���Ɏ擾����</summary>
    public void FlickNum()
    {
        var rnd = UnityEngine.Random.Range(1, 5);
        _flickTypeEnemy = (FlickType)rnd;
        _requestUIController.ChangeRequestImage(_flickTypeEnemy);
    }

    /// <summary>����ʂ̏������s��</summary>
    public void JugeTime(FlickType playInput)
    {
        if (_flickTypeEnemy != playInput) { return; }

        if (!_isdead)
        {
            switch (_currentResult)
            {
                case TimingResult.Perfect:
                    Dead();
                    ResultManager.Instance.CountPerfect++;
                    break;
                case TimingResult.Good:
                    Dead();
                    ResultManager.Instance.CountGood++;
                    break;
                case TimingResult.Bad:
                    BadMove();
                    ResultManager.Instance.CountBad++;
                    break;
            }
        }

       
    }
    /// <summary>�������̏��������� </summary>
    public void SetUp()
    {
        var points = transform;     //�ꎞ�I�ɋO���̐e�I�u�W�F�N�g��ێ�����ׂ̕ϐ�

        FlickNum(); //�����_���Ńt���b�N�������擾����


        switch (_flickTypeEnemy)    //�e�t�@���T�Ő�����ԕ��������߂�
        {
            case FlickType.Left:
                points = _trajectoryParent[0];
                break;
            case FlickType.Right:
                points = _trajectoryParent[1];
                break;
            case FlickType.Up:
            case FlickType.Down:
                points = _trajectoryParent[2];
                break;
        }

        _deadMovePoints = new Vector3[points.childCount - 1];

        for (var i = 0; i < _deadMovePoints.Length; i++)
        {
            _deadMovePoints[i] = points.GetChild(i).position;
        }
    }
}