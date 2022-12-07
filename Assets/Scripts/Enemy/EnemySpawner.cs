using UnityEngine;

/// <summary>�t�@���𐶐�����N���X </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Tooltip("�v���n�u")]
    Enemy _enemyPrefub = default;
    [SerializeField, Tooltip("�X�|�[���n�_")]
    GameObject _positionObject = default;
    [SerializeField, Tooltip("�Q�[���}�l�W���[")]
    GameManager _manager = default;
    [Tooltip("�G���o�����������b��")]
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };

    /// <summary>�����Ԋu�����߂�Y����</summary>
    int _timeIntervalIndex = 0;
    /// <summary>�b���𐔂���ϐ�</summary>
    float _generateTimer = 0;

    private void Start()
    {
        _timeIntervalIndex = Random.Range(0, _timeInterval.Length);
    }

    void Update()
    {
        if (_manager.CurrentEnemy == null)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _timeInterval[_timeIntervalIndex]) //_timeInterval�𒴂����Instantiate���܂�
            {
                var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�
                enemy.SetUp();

                //�C�x���g��o�^
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.Scroller.ScrollOperation;
                enemy.GiveDamage += _manager.GetDamage;

                _manager.CurrentEnemy = enemy;
                _manager.Scroller.ScrollOperation();    //�X�e�[�W�X�N���[�����~�߂�
                _timeIntervalIndex = Random.Range(0, _timeInterval.Length);     //���̐����Ԋu�����߂�
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
