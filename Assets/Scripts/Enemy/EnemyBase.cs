using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�t�@��(�G)�̊��N���X </summary>
[RequireComponent(typeof(Rigidbody))]
public abstract class EnemyBase : MonoBehaviour
{
    /// <summary>�t�@���T�v���̍ŏ��l FlickType�ɑΉ�</summary>
    const int REQUREST_MIN_VALUE = 1;
    /// <summary>�t�@���T�v���̍ő�l FlickType�ɑΉ�</summary>
    const int REQUREST_MAX_VALUE = 5;

    #region
    [SerializeField, Header("�t�@���̎��")]
    EnemyType _enemyType = EnemyType.None;
    [SerializeField, Tooltip("�t�@���T���X�V����N���X")]
    RequestUIController[] _requestUIArray = null;
    [SerializeField, Tooltip("�C���X�g���Ǘ�����N���X")]
    EnemySpriteChange[] _enemySprites = null;
    [SerializeField, Tooltip("�����G�t�F�N�g")]
    GameObject _explosionEffect = default;

    /// <summary>�e�t�@���̃p�����[�^�[ 0=�ʏ� 1=�ǃt�@��2 2=�ǃt�@��3 4=boss</summary>
    EnemyParameter[] _parameters => LevelManager.Instance.CurrentLevel.EnemyParameters;
    /// <summary>���݂̃p�����[�^�[</summary>
    protected EnemyParameter _currentParameter = default;
    /// <summary>�t�@���T�v����ێ�����z�� </summary>
    FlickType[] _requestArray = default;
    /// <summary>������ю��̕]�� </summary>
    TimingResult _currentResult = TimingResult.None;
    /// <summary>���ݗv�����Ă���t�@���T </summary>
    protected FlickType _currentRequest = FlickType.None;
    /// <summary>�������t�@���T��</summary>
    int _requestCount = 0;
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

    /// <summary>�R���{���𑝂₷���� </summary>
    event Action<TimingResult> _addComboCount = default;

    /// <summary>�G�����񂾂烊�X�g��������鏈�� </summary>
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
        _rb.AddForce(-transform.forward * _currentParameter.MoveSpped); //�t�@����O�Ɉړ�������
        _time = _currentParameter.RhythmTimes[_resultTimeIndex];
    }

    void Update()
    {
        if (!_isdead)
        {
            UpdateResultTime();
        }
    }

    /// <summary>�G�̎�ނɂ���ăp�����[�^�[��ύX���� </summary>
    void SelectEnemyParameter()
    {
        switch (_enemyType)
        {
            case EnemyType.Nomal:
                _currentParameter = _parameters[0];
                break;
            case EnemyType.Wall2:
                _currentParameter = _parameters[1];
                break;
            case EnemyType.Wall3:
                _currentParameter = _parameters[2];
                break;
            case EnemyType.Boss:
                _currentParameter = _parameters[3];
                break;
        }

        _time = _currentParameter.RhythmTimes[_resultTimeIndex];    //���Y������p�̃^�C�}�[������������
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
                OutEffect();
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
                // AudioManager.Instance.PlaySE(2, 0.5f);
                break;
            case TimingResult.Good:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);     //�����G�t�F�N�g�𐶐�
                                                                                            //  AudioManager.Instance.PlaySE(6, 0.7f);
                GoodEffect();
                break;
            case TimingResult.Perfect:
                Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                //    AudioManager.Instance.PlaySE(6, 0.7f);
                PerfactEffect();
                break;
        }
    }

    /// <summary>�A�E�g���̉��o (s����)</summary>
    void OutEffect()
    {
        //spirte���t�F�[�h������
        Array.ForEach(_enemySprites, s =>
              s.GetComponent<SpriteRenderer>().DOFade(endValue: 0, duration: _currentParameter.FadeSpeed).OnComplete(() => Destroy(gameObject)));

        _giveDamage?.Invoke(_currentParameter.AddDamageValue);  //�_���[�W��^����
        _addComboCount?.Invoke(_currentResult);
        _disapperEnemies?.Invoke(this);

        _isdead = true;
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

    /// <summary>�X�e�[�W�X�N���[�����������s���� </summary>
    protected void StageScrollRun()
    {
        _stageScroll?.Invoke();
    }

    /// <summary>�_���[�W���������s���� </summary>
    protected void GiveDamageRun()
    {
        _giveDamage?.Invoke(_currentParameter.AddDamageValue);
    }

    /// <summary>����Ɏg�p����o�ߎ��Ԃ��v������ </summary>
    protected void UpdateResultTime()
    {
        _time -= Time.deltaTime;// ���Y������p

        if (_time <= _currentParameter.RhythmTimes[_resultTimeIndex + 1] && !_isdead)    //������ю��̕]�����X�V����
        {
            _resultTimeIndex++;
            UpdateCurrentResult();
        }
    }

    /// <summary>�t�@���T�����߂�</summary>
    protected void FlickNum()
    {
        for (var i = 0; i < _requestUIArray.Length; i++)
        {
            var rnd = UnityEngine.Random.Range(REQUREST_MIN_VALUE, REQUREST_MAX_VALUE);

            _requestArray[i] = (FlickType)rnd;
            _requestUIArray[i].ChangeRequestImage((FlickType)rnd);
        }

        _currentRequest = _requestArray[0]; //�ŏ��̃t�@���T�����߂�
    }


    /// <summary>�������̏��������� </summary>
    /// <param name="istate">���݂̃Q�[���̏��</param>
    public virtual void SetUp(IState currentGameState)
    {
        _requestArray = new FlickType[_requestUIArray.Length];

        SelectEnemyParameter();

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
