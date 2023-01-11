using System;
using UnityEngine;
using DG.Tweening;

/// <summary>�t�@���𐶐�����N���X </summary>
public class EnemySpawner : MonoBehaviour
{
    //TODO:EnemySpawner���ǂ����Ő�������Ă���ǂ��Ő�������Ă��邩������
    static EnemySpawner _instance = default; //�ꎞ�I�ɃV���O���g���p�^�[�����g�p���Ă��邪�{���͎g��Ȃ�

    /// <summary>���������߂�ׂ̕ϐ� </summary>
    const int LAST_ENEMY_GENERATED = 1;
    /// <summary>�{�X���ړ������鏈����o�^����וϐ� </summary>
    const int BOSS_MOVE_REGISTER = 2;
    /// <summary>�����̐����ʒu�̓Y���� </summary>
    const int LEFT_SPAWN_POINT = 2;



    #region private SerializeField

    [SerializeField, Tooltip("0=�m�[�}�� 1=�ǃt�@��2 3=�ǃt�@��3 4=�{�X"), ElementNames(new string[] { "�m�[�}��", "�ǃt�@��2", "�ǃt�@��3", "�{�X" })]
    EnemyBase[] _enemyPrefubs = default;

    [ElementNames(new string[] { "����", "�E��", "����" })]
    [SerializeField, Tooltip("0=���� 1=�E�� 2=����")]
    Transform[] _spawnPoints = default;

    [SerializeField, Tooltip("�Q�[���}�l�W���[")]
    GameManager _manager = default;

    [SerializeField, Tooltip("Boss�̐����ʒu")]
    Transform _bossSpawnPoint = default;

    #endregion

    #region private
    /// <summary>�G�̐������� </summary>
    EnemyOrderParameter _order => LevelManager.Instance.CurrentLevel.EnemyOrder;
    /// <summary>���ɐ�������G�̏�� </summary>
    EnemyInfo _nextEnemyInfo = default;

    BossEnemy _boss = default;

    /// <summary>�G���o�����������b�� 0=7.68f 1=5.76f 2=3.84f 3=1.92f</summary>
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    /// <summary>�{�X��œG���o�����������b�� 0=7.6.4f 1=5.4.8f 2=3.2f 3=1.6f</summary>
    float[] _bossTimeInterval = new float[4] { 6.72f, 4.8f, 2.88f, 0.96f };

    /// <summary>�������Ԃ̂ǂ��܂Ő����������ǂ����̓Y���� </summary>
    int _infoIndex = 0;
    /// <summary>�b���𐔂���^�C�}�[</summary>
    float _generateTimer = 0;
    /// <summary>����������G�̐������� </summary>
    float _nextGenerateTime = 0;
    /// <summary>�ŏ��܂��͎��̓G�̔����ʒu�����߂�ϐ�  0�Ȃ�^�񒆁A1�Ȃ�E�A2�Ȃ獶</summary>
    int _positionCount = 0;
    /// <summary>�t�@���𐶐����邩�ǂ��� true=�������� </summary>
    bool _isGenerate = true;
    /// <summary>�{�X���ړ������鏈����o�^����</summary>
    bool _isRegister = false;

    public bool IsGenerate { get => _isGenerate; set => _isGenerate = value; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    private void Start()
    {
        _nextEnemyInfo = _order.EnemyOrder[_infoIndex];
        _nextGenerateTime = _timeInterval[(int)_nextEnemyInfo._generateInterval];
    }

    void Update()
    {
        if (_isGenerate)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _nextGenerateTime) //_timeInterval�𒴂����Instantiate���܂�
            {
                GenerateEnemy(_positionCount);

                _positionCount++;

                if (_positionCount == _spawnPoints.Length)
                {
                    _positionCount = 0;
                }


                NextEnemyInfoSetup();

                _generateTimer = 0;
            }
        }
    }

    /// <summary>�G�𐶐����A���������s��</summary>
    void GenerateEnemy(int spawnPointNumber)
    {
        if (_nextEnemyInfo._enemyType == EnemyType.Wait) { return; }�@//�ҋ@�ł���Ή������Ȃ�

        var enemy = Instantiate(_enemyPrefubs[(int)_nextEnemyInfo._enemyType], _spawnPoints[_positionCount].transform);
        _manager.AddEnemy(enemy);
        enemy.Setup(_manager.CurrentGameState, _nextEnemyInfo);

        //�C�x���g��o�^
        enemy.AddComboCount += _manager.ComboAmountTotal;
        enemy.StageScroll += _manager.StageScroll;
        enemy.GiveDamage += _manager.GetDamage;
        enemy.DisapperEnemies += _manager.RemoveEnemy;
        enemy.AddIdolPower += _manager.IncreseIdlePower;
        enemy.EnterSuperIdolTime += _manager.EnemyCheckIdolPower;

        if (spawnPointNumber == LEFT_SPAWN_POINT)      //������������C���X�g�𔽓]����
        {
            enemy.ReverseEnemySprite(_manager.CurrentGameState);
        }

        if (_isRegister)
        {
            enemy.BossMove += BossMove;
        }
    }

    /// <summary>���̓G����ݒ肷�� </summary>
    void NextEnemyInfoSetup()
    {
        _infoIndex++;


        if (_order.EnemyOrder.Count - LAST_ENEMY_GENERATED <= _infoIndex)  //�Ō�̓G���������ꂽ�琶�������߂�
        {
            _isGenerate = false;
           // return;
        }
        else if (_order.EnemyOrder.Count - BOSS_MOVE_REGISTER <= _infoIndex)  //�Ō�̓G�Ƀ{�X���ړ������鏈����o�^�����
        {
            _isRegister = true;
        }

        _nextEnemyInfo = _order.EnemyOrder[_infoIndex];

        if (_manager.CurrentGameState is BossTime)
        {
            _nextGenerateTime = _bossTimeInterval[(int)_nextEnemyInfo._generateInterval];
            Debug.Log(_nextGenerateTime);
        }
        else
        {
            _nextGenerateTime = _timeInterval[(int)_nextEnemyInfo._generateInterval];
        }
    }

    /// <summary>�{�X�𐶐����� </summary>
    public void SpawnBossEnemy()
    {
        _generateTimer = 0f;

        var go = Instantiate(_enemyPrefubs[3], _bossSpawnPoint);

        if (go is not BossEnemy) { return; }    //�L���X�g�ł��Ȃ���Ή������Ȃ�

        _boss = ((BossEnemy)go);

        _boss.AddComboCount += _manager.ComboAmountTotal;
        _boss.GiveDamage += _manager.GetDamage;
        _boss.GameClear += _manager.GameClear;

        _boss.BossSprite.color = new Color(1f, 1f, 1f, 0f);

        _boss.BossSprite.DOFade(1f, 2f)
            .SetDelay(5f);
    }

    /// <summary>�{�X���ړ������鏈��</summary>
    void BossMove()
    {
        _manager.AddEnemy(_boss);
        _boss.Setup(_manager.CurrentGameState, _nextEnemyInfo);
        _isGenerate = false;
        _generateTimer = 0f;
        _boss.StartMoveAnim();
    }
}
