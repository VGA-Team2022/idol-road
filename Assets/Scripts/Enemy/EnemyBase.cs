using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�t�@��(�G)�̊��N���X </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class EnemyBase : MonoBehaviour
{
#region
    [SerializeField, Header("�������Ă��鑬�x"), Range(1, 50)]
    protected float _enemySpped;
    [SerializeField, Header("�����ɂȂ�܂łɂ����鎞��")]
    float _fadedSpeed = 1;
    [ElementNames(new string[] { "���v����", "Bad", "Good", "Perfect", "Out" })]
    [SerializeField, Header("���Y������̕b��"), Tooltip("0=���v���� 1=bad 2=good 3=perfect 4=out")]
    protected float[] _resultTimes = default;
    [SerializeField, Tooltip("�t�@���T���X�V����N���X")]
    protected RequestUIController[] _requestUIArray = null;
    [SerializeField, Tooltip("�C���X�g���Ǘ�����N���X")]
    EnemySpriteChange[] _enemySprites = null;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    protected GameObject _explosionEffect = default;

    /// <summary>�t�@���T�v����ێ�����z�� </summary>
    protected FlickType[] _requestArray = default;
    /// <summary>������ю��̕]�� </summary>
    protected TimingResult _currentResult = TimingResult.None;
    /// <summary>���ݗv�����Ă���t�@���T </summary>
    protected FlickType _currentRequest = FlickType.None;
    /// <summary>�������t�@���T��</summary>
    int _requestCount = 0;
    /// <summary>�]������̎��Ԃ̓Y���� </summary>
    protected int _resultTimeIndex = 0;
    /// <summary>�G�̎��S�t���O</summary>
    bool _isdead = false;
    /// <summary>�]���ύX�p�ϐ�</summary>
    protected float _time = 0f;

    /// <summary>�|���ꂽ��X�e�[�W�X�N���[�����J�n���� </summary>
    event Action _stageScroll = default;
    /// <summary>�_���[�W��^����i�v���C���[�̗̑͂����炷�j</summary>
    event Action<int> _giveDamage = default;
    /// <summary>�R���{���𑝂₷���� </summay>
    event Action<TimingResult> _addComboCount = default;
    /// <summary>�G�����񂾂烊�X�g��������鏈�� </summay>
    event Action<EnemyBase> _disapperEnemies = default;


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

    /// <summary>�G�����񂾂烊�X�g��������鏈�� </summay>
    public event Action<EnemyBase> DisapperEnemies
    {
        add { _disapperEnemies += value; }
        remove { _disapperEnemies -= value; }
    }

    protected Rigidbody _rb => GetComponent<Rigidbody>();

    /// <summary>������їp�A�j���[�V�����R���g���[���[ </summary>
    protected Animator _anim => GetComponent<Animator>();

    protected EnemySpriteChange[] EnemySprites { get => _enemySprites; }

    #endregion

    void Start()
    {
        _rb.AddForce(-transform.forward * _enemySpped); //�t�@����O�Ɉړ�������
        _time = _resultTimes[_resultTimeIndex];
    }

    void Update()
    {
        if (!_isdead)
        {
            UpdateResultTime();
        }
    }

    /// <summary>�X�R�A��v���������������� </summary>
    void AddScore()
    {
        switch (_currentResult)
        {
            case TimingResult.Perfect:
                PlayResult.Instance.CountPerfect += _requestArray.Length;
                break;
            case TimingResult.Good:
                PlayResult.Instance.CountGood += _requestArray.Length;
                break;
            case TimingResult.Bad:
                PlayResult.Instance.CountBad += _requestArray.Length;
                break;
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
                Array.ForEach(_enemySprites, s => 
                s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: _fadedSpeed).OnComplete(() => Destroy(gameObject)));
                _giveDamage?.Invoke(1);
                _addComboCount?.Invoke(_currentResult);
                _disapperEnemies?.Invoke(this);
                _isdead = true;
                break;
        }

        Array.ForEach(_requestUIArray, r => r.ChangeRequestWindow(_currentResult));
    }

    /// <summary>�]���ɂ���ē|���ꂽ���̈ړ����@��ύX���� </summary>
    void SelectDeadEffect()
    {
        switch (_currentResult)
        {
            case TimingResult.Bad:
                BadEffect();
                AudioManager.Instance.PlaySE(2, 0.5f);
                break;
            case TimingResult.Good:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //�����G�t�F�N�g�𐶐�
                AudioManager.Instance.PlaySE(6, 0.7f);
                GoodEffect();
                break;
            case TimingResult.Perfect:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                AudioManager.Instance.PlaySE(6, 0.7f);
                PerfactEffect();
                break;
        }
    }

    /// <summary>�|���ꂽ���̏��� </summary>
    void Dead()
    {
        if (_isdead) { return; }

        _isdead = true;
        Array.ForEach(_requestUIArray, r => r.gameObject.SetActive(false));
        SelectDeadEffect();
    }

    /// <summary>bad���莞�̉��o </summary>
    protected abstract void BadEffect();

    /// <summary>good���莞�̉��o </summary>
    protected abstract void GoodEffect();

    /// <summary>�p�[�t�F�N�g���莞�̉��o </summary>
    protected abstract void PerfactEffect();

    /// <summary>�A�E�g���莞�̉��o </summary>
    protected abstract void OutEffect();

    /// <summary>�X�e�[�W�X�N���[�����������s���� </summary>
    protected void StageScrollRun()
    {
        _stageScroll?.Invoke();
    }

    /// <summary>�_���[�W���������s���� </summary>
    protected void GiveDamageRun()
    {
        _giveDamage?.Invoke(1);
    }

    /// <summary>����Ɏg�p����o�ߎ��Ԃ��v������ </summary>
    protected void UpdateResultTime()
    {
        _time -= Time.deltaTime;// ���Y������p

        if (_time <= _resultTimes[_resultTimeIndex + 1] && !_isdead)    //������ю��̕]�����X�V����
        {
            _resultTimeIndex++;
            UpdateCurrentResult();
        }
    }

    /// <summary>�t�@���T�����߂�</summary>
    protected void FlickNum()
    {
        _requestArray = new FlickType[_requestUIArray.Length];
        _time = _resultTimes[_resultTimeIndex];

        for (var i = 0; i < _requestUIArray.Length; i++)
        {
            var rnd = UnityEngine.Random.Range(1, 5);
            _requestArray[i] = (FlickType)rnd;
            _requestUIArray[i].ChangeRequestImage((FlickType)rnd);
        }

        _currentRequest = _requestArray[0];
    }


    /// <summary>�������̏��������� </summary>
    /// <param name="istate">���݂̃Q�[���̏��</param>
    public virtual void SetUp(IState currentGameState)
    {
        FlickNum(); //�����_���Ńt���b�N�������擾����

        if (currentGameState is BossTime)
        {
            Array.ForEach(_enemySprites, e => e.EnemyBossMethod(e.gameObject.GetComponent<SpriteRenderer>()));
        }
        else 
        {
            Array.ForEach(_enemySprites, e => e.EnemyRandomMethod(e.gameObject.GetComponent<SpriteRenderer>())); 
        }

        //�e�t�@�����Ƃɍs�����������������override����
    }

    /// <summary>����ʂ̏������s��</summary>
    public void JugeTime(FlickType playInput)
    {
        if (_currentRequest != playInput || _isdead) { return; }

        _requestUIArray[_requestCount].gameObject.SetActive(false);     //�B�������t�@���TUI���\���ɂ���
        _requestCount++;

        if (_requestCount == _requestArray.Length)  //�|���ꂽ
        {
            AddScore();
            _disapperEnemies?.Invoke(this);
            _addComboCount?.Invoke(_currentResult);
            _stageScroll?.Invoke();
            Dead();
            return;
        }

        _currentRequest = _requestArray[_requestCount];     //�v���t�@���T���X�V
    }
}
