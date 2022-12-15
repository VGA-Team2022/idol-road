using UnityEngine;
using DG.Tweening;

/// <summary>�t�@���𐶐�����N���X </summary>
public class EnemySpawner : MonoBehaviour
{
    #region private SerializeField

    [SerializeField, Tooltip("0=�m�[�}�� 1=�ǃt�@�� 2=�{�X"), ElementNames(new string[] {"�m�[�}��", "�ǃt�@��", "�{�X"})]
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
    /// <summary>�G���o�����������b�� 0=7.68f 1=5.76f 2=3.84f 3=1.92f</summary>
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    /// <summary>�{�X��œG���o�����������b�� 0=7.6.4f 1=5.4.8f 2=3.2f 3=1.6f</summary>
    float[] _bossTimeInterval = new float[4] { 6.4f, 4.8f, 3.2f, 1.6f };

    /// <summary>�����Ԋu�����߂�Y����</summary>
    int _timeIntervalIndex = 0;
    /// <summary>�b���𐔂���ϐ�</summary>
    float _generateTimer = 0;
    /// <summary>�ŏ��܂��͎��̓G�̔����ʒu�����߂�ϐ�  0�Ȃ�^�񒆁A1�Ȃ�E�A2�Ȃ獶</summary>
    int _positionCount = 0;

#endregion
    private void Start()
    {
        _timeIntervalIndex = Random.Range(0, _timeInterval.Length);
        _generateTimer = -_timeInterval[3]; //�ŏ��̂�2�b�Ԓx����������
        
    }

    void Update()
    {
        if (_manager.CurrentGameState is Playing || _manager.CurrentGameState is BossTime)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _timeInterval[_timeIntervalIndex]) //_timeInterval�𒴂����Instantiate���܂�
            {
                
                var enemy = Instantiate(_enemyPrefubs[GetEnemyIndex()], _spawnPoints[_positionCount].transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�(�ŏ��͐^�񒆂̈ʒu��)
                _manager.AddEnemy(enemy);
                enemy.SetUp(_manager.CurrentGameState);

                //�C�x���g��o�^
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.StageScroll;
                enemy.GiveDamage += _manager.GetDamage;
                enemy.DisapperEnemies += _manager.RemoveEnemy;

                _timeIntervalIndex = Random.Range(0, _timeInterval.Length);     //���̐����Ԋu�����߂�

                _positionCount += 1;
                if (_positionCount == 3)
                {
                    _positionCount= 0;
                }
                _generateTimer = 0;
            }
        }
    }

    /// <summary>��������t�@�������߂�i�Y���������߂�j</summary>
    /// <returns>��������t�@���̓Y����</returns>
    int GetEnemyIndex()
    {
        var index = Random.Range(0, _enemyPrefubs.Length - 1); //Boss�𐶐������Ȃ��ׂ�-1����
        return index;
    }

    /// <summary>�{�X�𐶐����� </summary>
    public void SpawnBossEnemy()
    {
       var boss = Instantiate(_enemyPrefubs[2], _bossSpawnPoint);

        if (boss is not BossEnemy)  //�L���X�g�ł��Ȃ���Ή������Ȃ�
        {
            return;   
        }

        _manager.StartBossMove += ((BossEnemy)boss).MoveStart;

        var sr = boss.GetComponent<SpriteRenderer>();
        sr.color = new Color(1f, 1f, 1f, 0f);
        sr.DOFade(1f, 2f)
            .SetDelay(5f);
    }
}
