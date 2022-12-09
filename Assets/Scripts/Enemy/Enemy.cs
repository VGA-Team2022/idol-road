using DG.Tweening;
using System;
using UnityEngine;

/// <summary>�G�l�~�[���Ǘ�����N���X </summary>
[RequireComponent(typeof(Rigidbody), typeof(SpriteRenderer), typeof(EnemySpriteChange))]
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
    /// <summary>�|���ꂽ��X�e�[�W�X�N���[�����J�n���� </summary>
    event Action _stageScroll = default;
    /// <summary>�_���[�W��^����i�v���C���[�̗̑͂����炷�j</summary>
    event Action<int> _giveDamage = default;
    /// <summary>�R���{���𑝂₷���� </summay>
    event Action<TimingResult> _addComboCount = default;

#region
    Rigidbody _rb => GetComponent<Rigidbody>();
    SpriteRenderer _sr => GetComponent<SpriteRenderer>();
    /// <summary>�G�̃X�v���C�g���Ǘ�����N���X�̕ϐ� </summary>
    EnemySpriteChange _spriteChange => GetComponent<EnemySpriteChange>();

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

    /// <summary>�R���{���𑝂₷���� </summay>
    public event Action<TimingResult> AddComboCount
    {
        add { _addComboCount += value; }
        remove { _addComboCount -= value; }
    }

#endregion

    private void Start()
    {
        _rb.AddForce(-transform.forward * _enemySpped); //�t�@����O�Ɉړ�������
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
                _sr.DOFade(endValue: 0, duration: _fadedSpeed).OnComplete(OutEffect);
                _addComboCount?.Invoke(_currentResult);
                _isdead = true;
                break;
        }

        _requestUIController.ChangeRequestWindow(_currentResult);
    }

    /// <summary>�t�@���T�����߂�</summary>
    void FlickNum()
    {
        var rnd = UnityEngine.Random.Range(1, 5);
        _flickTypeEnemy = (FlickType)rnd;
        _requestUIController.ChangeRequestImage(_flickTypeEnemy);
    }


    /// <summary>�]���ɂ���ē|���ꂽ���̈ړ����@��ύX���� </summary>
    void SelectDeadEffect()
    {
        switch (_currentResult)
        {
            case TimingResult.Bad:
                BadEffect();
                break;
            case TimingResult.Good:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //�����G�t�F�N�g�𐶐�
                AudioManager.Instance.PlaySE(6, 0.1f);
                DeadEffect();
                break;
            case TimingResult.Perfect:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySE(6, 0.1f);
                DeadEffect();
                break;
        }
    }

    /// <summary> Bad, Out�ȊO�Ŏg�p���鐁����щ��o </summary>
    void DeadEffect()
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
    void BadEffect()
    {
        //���ړ�
        transform.DOMoveX(-3, _deadMoveTime)
            .SetDelay(EXPLOSION_DELAY)
            .OnComplete(DeadProcess);
        //.OnComplete(() =>
        //{
        //    _sr.DOFade(endValue: 0, duration: 2.0f).OnComplete(DeadProcess);
        //});

        //�����ɂȂ�Ȃ�������Ă����p�^�[��
        _sr.DOFade(endValue: 0, duration: 2.0f).OnComplete(() => _giveDamage?.Invoke(_fansaNum));
    }

    /// <summary>out���̂̏���</summary>
    void OutEffect()
    {
        _giveDamage?.Invoke(_fansaNum); //�_���[�W��^����
        DeadProcess();
    }

    /// <summary>���S���̋��ʏ���</summary>
    void DeadProcess()
    {
        _stageScroll?.Invoke();         //�X�e�[�W�X�N���[�����s��
        Destroy(gameObject);
    }

    /// <summary>�|���ꂽ���̏��� </summary>
    public void Dead()
    {
        if (_isdead) { return; }

        _isdead = true;
        _requestUIController.gameObject.SetActive(false);
        SelectDeadEffect();
    }

    /// <summary>����ʂ̏������s��</summary>
    public void JugeTime(FlickType playInput)
    {
        if (_flickTypeEnemy != playInput || _isdead) { return; }

        switch (_currentResult)
        {
            case TimingResult.Perfect:
                ResultManager.Instance.CountPerfect++;
                break;
            case TimingResult.Good:
                ResultManager.Instance.CountGood++;
                break;
            case TimingResult.Bad:
                ResultManager.Instance.CountBad++;
                break;
        }

        _addComboCount?.Invoke(_currentResult);
        Dead();
    }

    /// <summary>�������̏��������� </summary>
    public void SetUp(IState istate)
    {
        var points = transform;     //�ꎞ�I�ɋO���̐e�I�u�W�F�N�g��ێ�����ׂ̕ϐ�

        FlickNum(); //�����_���Ńt���b�N�������擾����

        if(istate is BossTime) 
        {
            _spriteChange.EnemyBossMethod(_sr);
        }
        else { _spriteChange.EnemyRandomMethod(_sr); }

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