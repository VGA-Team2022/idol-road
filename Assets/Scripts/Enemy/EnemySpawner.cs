using UnityEngine;

/// <summary>�t�@���𐶐�����N���X </summary>
public class EnemySpawner : MonoBehaviour
{
    #region private SerializeField

    [SerializeField, Tooltip("�v���n�u")]
    Enemy _enemyPrefub = default;

    [ElementNames(new string[] { "����", "�E��", "����" })]
    [SerializeField, Tooltip("0=���� 1=�E�� 2=����")]
    Transform[] _spawnPoints = default;

    [SerializeField, Tooltip("�Q�[���}�l�W���[")]
    GameManager _manager = default;

#endregion

#region private

    [Tooltip("�G���o�����������b��")]
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    [Tooltip("�{�X��œG���o�����������b��")]
    float[] _bossTimeInterval = new float[4] { 6.4f, 4.8f, 3.2f, 1.6f };

    /// <summary>�����Ԋu�����߂�Y����</summary>
    int _timeIntervalIndex = 0;
    /// <summary>�b���𐔂���ϐ�</summary>
    float _generateTimer = 0;
    /// <summary>�ŏ��܂��͎��̓G�̔����ʒu�����߂�Y�����ϐ�</summary>
    int _positionCount = 0; // 0�Ȃ�^�񒆁A1�Ȃ�E�A2�Ȃ獶

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
                
                var enemy = Instantiate(_enemyPrefub, _spawnPoints[_positionCount].transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�(�ŏ��͐^�񒆂̈ʒu��)
                _manager.AddEnemy(enemy);
                enemy.SetUp(_manager.CurrentGameState);

                //�C�x���g��o�^
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.Scroller.ScrollOperation;
                enemy.GiveDamage += _manager.GetDamage;
                enemy.DisapperEnemies += _manager.RemoveEnemy;

                _manager.Scroller.ScrollOperation();    //�X�e�[�W�X�N���[�����~�߂�
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

    /// <summary>
    /// �{�^���ɐݒ肷��p�u���b�N�֐�
    /// false�ɂȂ�ƃG�l�~�[�̏o�����~�܂�
    /// ���Q�[���̎��Ԃ�G�l�~�[���o�Ă���C���^�[�o���̎��Ԃ����ׂă��Z�b�g
    /// </summary>
    public void OffEnemy()
    {
        _generateTimer = 0;
    }
}
