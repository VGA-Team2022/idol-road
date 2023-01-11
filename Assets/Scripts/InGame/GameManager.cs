using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>�C���Q�[�����Ǘ�����N���X �X�e�[�g�p�^�[���g�p</summary>
public class GameManager : MonoBehaviour
{
    #region
    /// <summary>�X�^�[�g��� </summary>
    public static GameStart _startState => new GameStart();
    /// <summary>�v���C��� </summary>
    public static Playing _playingState => new Playing();
    /// <summary>�A�C�h���^�C����� </summary>
    public static IdolTime _idleTimeState => new IdolTime();
    /// <summary>�{�X��ԏ�� </summary>
    public BossTime _bossTimeState => new BossTime();
    /// <summary>�Q�[���I����� </summary>
    public static GameEnd _gameEndState => new GameEnd();

    [SerializeField, Header("�f�o�b�O���[�h"), Tooltip("�f�o�b�O���̓`�F�b�N�����ĉ�����")]
    bool _debugMode = false;
    [SerializeField, Header("���݂̓�Փx"), Tooltip("�I������Ă����Փx���K�p����܂�")]
    GameLevel _currentLevel = GameLevel.Nomal;
    [SerializeField, Header("�X�N���[��������I�u�W�F�N�g")]
    StageScroller _stageScroller = default;
    [SerializeField, Tooltip("�t�F�[�h���s���N���X")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("InGameUI�̍X�V���s���N���X")]
    InGameUIController _uiController = default;
    [SerializeField, Tooltip("�G�𐶐�����N���X")]
    EnemySpawner _enemySpawner = default;
    [SerializeField, Tooltip("Warning�v���n�u")]
    PlayableDirector _warningTape = default;
    [SerializeField, Tooltip("�^�N�V�[�I�u�W�F�N�g")]
    SpriteRenderer _taxi = default;
    [SerializeField, Tooltip("�v���C���[")]
    PlayerMotion _player = default;
    [SerializeField, Tooltip("�X�[�p�[�A�C�h���^�C���̏������s���N���X")]
    SuperIdolTime _superIdolTime = default;
    [SerializeField, Tooltip("�A�C�e���W�F�l���[�^�[")]
    ItemGenerator _itemGenerator = default;

    InGameParameter _inGameParameter => LevelManager.Instance.CurrentLevel.InGame;
    /// <summary>�R���{�ɂ���ĕς���C���X�g</summary>
    List<ComboInfo> _comboInfos => LevelManager.Instance.CurrentLevel.ComboParameter.ComboInfos;
    /// <summary>���ݑΏۂ̓G </summary>
    EnemyBase _currentEnemy = default;
    /// <summary>���݂̃Q�[�����</summary>
    IState _currentGameState = null;
    /// <summary>�X�e�[�W�ɕ\������Ă���G�̃��X�g</summary>
    List<EnemyBase> _enemies = new List<EnemyBase>();
    /// <summary>���݂̗̑� </summary>
    int _idleHp;
    /// <summary>���݂̃A�C�h���p���[ </summary>
    float _idlePower;
    /// <summary>�R���{�𐔂���ϐ� </summary>
    int _comboAmount;
    /// <summary>���ɃR���{�C���X�g��\������J�E���g</summary>
    int _nextComboCount = -1;
    /// <summary>���̃R���{�ŕ\������C���X�g�̗v�f</summary>
    int _comboIndex = 0;
    /// <summary>�Q�[���J�n����̌o�ߎ��� </summary>
    float _elapsedTime = 0f;
    /// <summary>���Ԃ��o�߂��Ă��邩�ۂ�</summary>
    bool _isElapsing = true;
    /// <summary>���S�������ǂ��� </summary>
    bool _isdead = false;
    /// <summary�X�[�p�[�A�C�h���^�C�����s�������ǂ���</summary>
    bool _isPlayedIdolTime = false;
    /// <summary>�{�X�̈ړ����J�n���鏈��</summary>
    event Action _startBossMove = default;

    /// <summary>���݂̃Q�[����� </summary>
    public IState CurrentGameState { get => _currentGameState; }
    /// <summary>���ݑΏۂ̓G</summary>
    public EnemyBase CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }
    /// <summary>�X�N���[��������I�u�W�F�N�g</summary>
    public StageScroller Scroller { get => _stageScroller; }
    /// <summary>�G�𐶐�����N���X</summary>
    public EnemySpawner EnemyGenerator { get => _enemySpawner; }
    /// <summary>�t�F�[�h���s���N���X</summary>
    public FadeController FadeCanvas { get => _fadeController; }
    public PlayableDirector WarningTape { get => _warningTape; }
    public SpriteRenderer Taxi { get => _taxi; }
    public SuperIdolTime IdolTime { get => _superIdolTime; }
    public InGameUIController UIController { get => _uiController; }

    /// <summary>�{�X�̈ړ����J�n���鏈��</summary>
    public event Action StartBossMove
    {
        add { _startBossMove += value; }
        remove { _startBossMove -= value; }
    }

    #endregion

    private void Awake()
    {
        if (_debugMode)
        {
            LevelManager.Instance.SelectLevel(_currentLevel);
        }
    }

    void Start()
    {
        PlayResult.Instance.ResetResult();
        _idleHp = _inGameParameter.PlayerHp;
        _currentGameState = _startState;
        _currentGameState.OnEnter(this, null);
        _uiController.InitializeInGameUI(_inGameParameter.PlayerHp, _inGameParameter.GamePlayTime, _inGameParameter.IdolPowerMaxValue); //UI�̏���������
        Application.targetFrameRate = 60; //FPS��60�ɐݒ�

        if (0 < _comboInfos.Count)
        {
            _nextComboCount = _comboInfos[0]._nextCombo;
        }
    }

    void Update()
    {
        if (_currentGameState is not GameStart && _currentGameState is not GameEnd && _isElapsing) //���Ԍo�߂�UI���X�V����
        {
            _elapsedTime += Time.deltaTime;
            _uiController.UpdateGoalDistanceUI(_elapsedTime);
        }

        if (_inGameParameter.StartBossTime >= Math.Abs(_inGameParameter.GamePlayTime - _elapsedTime) && _currentGameState is Playing)    //�{�X�X�e�[�W���J�n
        {
            ChangeGameState(_bossTimeState);
            _itemGenerator.IsGenerate = false;
            _uiController.DisactiveIdolPowerGauge();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log(_enemies.Count);
        }
    }

    /// <summary>BGM���~�߂鏈��</summary>
    IEnumerator StopBGM()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.StopBGM(10);
        AudioManager.Instance.StopBGM(15);
    }

    /// <summary>BGM���������s���� </summary>
    public void RunStopBGM()
    {
        StartCoroutine(StopBGM());
    }

    /// <summary>�A�C�h���p���[����������֐�</summary>
    public void IncreseIdlePower(float power)
    {
        //�Q�[���X�e�[�g���ʏ킩�A�C�h���^�C������x�����Ă��Ȃ��Ȃ�
        if (_currentGameState is Playing && !_isPlayedIdolTime)
        {
            _idlePower += (power / _inGameParameter.IdolPowerMaxValue);
            _uiController.UpdateIdolPowerGauge(_idlePower);
        }
        if (_enemies.Count <= 0)
        {
            EnterSuperIdolTime();
        }
    }
    /// <summary>�X�[�p�[�A�C�h���^�C���𔭓�����֐�</summary>
    public void EnemyCheckIdolPower()
    {
        EnterSuperIdolTime();
    }

    public void EnterSuperIdolTime()
    {
        if (_idlePower >= 1 && !BossTime.IsPlaying && _currentGameState is not BossTime && !_isPlayedIdolTime)
        {
            _idlePower = 0;
            _isPlayedIdolTime = true;
            _itemGenerator.IsGenerate = false;
            _fadeController.FadeOut(() =>
            {
                _superIdolTime.gameObject.SetActive(true);
                _superIdolTime.CurrentState = _currentGameState;
                ChangeGameState(_idleTimeState);
                _fadeController.FadeIn(() =>
                {
                    _uiController.UpdateIdolPowerGauge(0);
                    _uiController.DisactiveIdolPowerGauge();
                });
            });
        }
    }

    /// <summary>�R���{�����Ǘ�����֐�</summary>
    public void ComboAmountTotal(TimingResult result)
    {
        if (result == TimingResult.Out || result == TimingResult.Bad)
        {
            _comboAmount = 0;
            _comboIndex = 0;

            if (0 < _nextComboCount)
            {
                _nextComboCount = _comboInfos[0]._nextCombo;
            }
        }
        else
        {
            _comboAmount++;
        }

        _uiController.UpdateComboText(_comboAmount);    //UI���X�V


        if(_nextComboCount < 0) { return; }

        if (_comboAmount == _nextComboCount)    //�R���{�C���X�g��\��
        {
            _uiController.PlayComboAnimation(_comboInfos[_comboIndex]._comboSprites);
            _comboIndex++;
            if (_nextComboCount > _comboIndex) 
            {
                _nextComboCount = _comboInfos[_comboIndex]._nextCombo;
            }  
        }
    }

    /// <summary>�̗͂����炷 </summary>
    /// <param name="damage">HP�������</param>
    public void GetDamage(int damage)
    {
        for (var i = 0; i < damage; i++)    //HPUI�����炷�ׂɉ�
        {
            _idleHp -= 1;

            _player.FailmMotion();

            if (0 <= _idleHp)
            {
                _uiController.UpdateHpUI(_idleHp); //HPUI���X�V
            }

            if (_idleHp <= 0 && !_isdead)   //�Q�[���I�[�o�[
            {
                ChangeGameState(_gameEndState); //�Q�[���I����ԂɑJ��
                _isdead = true;
                return;
            }
        }
    }

    /// <summary>�Q�[����Ԃ�؂�ւ��� </summary>
    /// <param name="nextState">���̏��</param>
    public void ChangeGameState(IState nextState)
    {
        _currentGameState.OnExit(this, nextState);
        nextState.OnEnter(this, _currentGameState);
        _currentGameState = nextState;
    }

    /// <summary>Enemy.cs�ɓo�^����X�e�[�W�X�N���[������</summary>
    public void StageScroll()
    {
        if (_enemies.Count <= 0 && _currentGameState is not BossTime)    //�t�@�������Ȃ��Ȃ�����X�N���[�����J�n����
        {
            _stageScroller.ScrollOperation();
        }
    }

    /// <summary>�������ꂽ�t�@�������X�g�ɒǉ����� </summary>
    /// <param name="enemy">�ǉ�����t�@��</param>
    public void AddEnemy(EnemyBase enemy)
    {
        if (_enemies.Count <= 0)    //�X�e�[�W�Ƀt�@�����o��
        {
            _currentEnemy = enemy;

            if (_currentGameState is not BossTime)
            {
                _stageScroller.ScrollOperation();
            }
        }

        _enemies.Add(enemy);
    }

    /// <summary>�|���ꂽ�t�@�������X�g����폜����</summary>
    /// <param name="enemy">�폜����t�@��</param>
    public void RemoveEnemy(EnemyBase enemy)
    {
        _enemies.Remove(enemy);

        if (1 <= _enemies.Count)
        {
            _currentEnemy = _enemies[0];
            return;
        }
    }

    public void ChangeTimeElapsing(bool which)
    {
        _isElapsing = which;
    }
     
    /// <summary>�Q�[���N���A���̏���</summary>
    public void GameClear()
    {
        ChangeGameState(_gameEndState);
        _player.GameClearMove();
    }
}