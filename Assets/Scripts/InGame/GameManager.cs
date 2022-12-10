using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>�C���Q�[�����Ǘ�����N���X �X�e�[�g�p�^�[���g�p</summary>
public class GameManager : MonoBehaviour
{
    /// <summary>�C���X�g���ς��^�C�~���O</summary>
    const int ADD_COMBO_ILLUST_CCHANGE = 5;

    /// <summary>�X�^�[�g��� </summary>
    public static GameStart _startState => new GameStart();
    /// <summary>�v���C��� </summary>
    public static Playing _playingState => new Playing();
    /// <summary>�A�C�h���^�C����� </summary>
    public static IdleTime _idleTimeState => new IdleTime();
    /// <summary>�{�X��ԏ�� </summary>
    public static BossTime _bossTimeState => new BossTime();
    /// <summary>�Q�[���I����� </summary>
    public static GameEnd _gameEndState => new GameEnd();

    [SerializeField, Header("Max�A�C�h���p���[")]
    int _maxIdlePower = 100;
    [SerializeField, Header("�A�C�h����MaxHp")]
    int _maxIdleHp = 5;
    [SerializeField, Header("��������")]
    float _gameTime = 60;
    [SerializeField, Header("�{�X�X�e�[�W���n�܂鎞��")]
    float _bossTime = 30;
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
    /// <summary>���ݑΏۂ̓G </summary>
    Enemy _currentEnemy = default;
    /// <summary>���݂̃Q�[�����</summary>
    IState _currentGameState = null;
    /// <summary>�X�e�[�W�ɕ\������Ă���G�̃��X�g</summary>
    List<Enemy> _enemies = new List<Enemy>();
    /// <summary>���݂̗̑� </summary>
    int _idleHp;
    /// <summary>���݂̃A�C�h���p���[ </summary>
    int _idlePower;
    /// <summary>�R���{�𐔂���ϐ� </summary>
    int _comboAmount;
    /// <summary>���ɃR���{�C���X�g��\������J�E���g</summary>
    int _nextComboCount = ADD_COMBO_ILLUST_CCHANGE;
    /// <summary>�Q�[���J�n����̌o�ߎ��� </summary>
    float _elapsedTime = 0f;

#region
    /// <summary>���݂̃Q�[����� </summary>
    public IState CurrentGameState { get => _currentGameState; }
    /// <summary>���ݑΏۂ̓G</summary>
    public Enemy CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }
    /// <summary>�X�N���[��������I�u�W�F�N�g</summary>
    public StageScroller Scroller { get => _stageScroller; }
    /// <summary>�G�𐶐�����N���X</summary>
    public EnemySpawner EnemyGenerator { get => _enemySpawner; }
    /// <summary>�t�F�[�h���s���N���X</summary>
    public FadeController FadeCanvas { get => _fadeController; }
    public PlayableDirector WarningTape { get => _warningTape; }
#endregion

    void Start()
    {
        _idleHp = _maxIdleHp;
        _currentGameState = _startState;
        _currentGameState.OnEnter(this, null);
        _uiController.InitializeInGameUI(_maxIdleHp, _gameTime, _maxIdlePower); //UI�̏���������
    }

    void Update()
    {
        if (_currentGameState is not GameStart && _currentGameState is not GameEnd) //���Ԍo�߂�UI���X�V����
        {
            _elapsedTime += Time.deltaTime;
            _uiController.UpdateGoalDistanceUI(_elapsedTime);
        }

        if (_bossTime >= Math.Abs(_gameTime - _elapsedTime) && _currentGameState is not BossTime)    //�{�X�X�e�[�W���J�n
        {
            ChangeGameState(_bossTimeState);
        }

        if (Input.GetButtonDown("Jump"))
        {
            //Debug.Log(_enemies.Count);
            Debug.Log(_currentGameState);
        }
    }

    /// <summary>�A�C�h���p���[����������֐�</summary>
    public void IncreseIdlePower(int power)
    {
        _idlePower += power;
        _uiController.UpdateIdolPowerGauge(_idlePower);

        if (_maxIdlePower <= _idlePower)    //�X�[�p�[�A�C�h���^�C���𔭓�
        {
            _idlePower = 0;
            _uiController.UpdateIdolPowerGauge(_idlePower);
            ChangeGameState(_idleTimeState);
        }
    }

    /// <summary>�R���{�����Ǘ�����֐�</summary>
    public void ComboAmountTotal(TimingResult result)
    {
        if (result == TimingResult.Out || result == TimingResult.Bad)
        {
            _comboAmount = 0;
        }
        else
        {
            _comboAmount++;
        }

        _uiController.UpdateComboText(_comboAmount);    //UI���X�V

        if (_comboAmount == _nextComboCount)    //�R���{�C���X�g��\��
        {
            _uiController.PlayComboAnimation();
            _nextComboCount += ADD_COMBO_ILLUST_CCHANGE;
        }
    }

    /// <summary>�̗͂����炷 </summary>
    /// <param name="damage">HP�������</param>
    public void GetDamage(int damage)
    {
        for (var i = 0; i < damage; i++)    //HPUI�����炷�ׂɉ�
        {
            _idleHp -= 1;

            if (0 <= _idleHp)
            {
                _uiController.UpdateHpUI(_idleHp); //HPUI���X�V
            }

            if (_idleHp <= 0)   //�Q�[���I�[�o�[
            {
                ChangeGameState(_gameEndState); //�Q�[���I����ԂɑJ��
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

    public void AddEnemy(Enemy enemy)
    {
        if (_enemies.Count <= 0)
        {
            _currentEnemy = enemy;
        }

        _enemies.Add(enemy);    
    }

    /// <summary>�|���ꂽ�G�����X�g����폜����</summary>
    /// <param name="enemy">�폜����G</param>
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);

        if (1 <= _enemies.Count)
        {
            _currentEnemy = _enemies[0];
        }
    }
}