using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField, Tooltip("�G�l�~�[�������Ƃ���"), Header("�G�l�~�[�֌W")]
    Enemy _enemyPrefub = default;
    [SerializeField, Tooltip("�G�l�~�[���o�����������I�u�W�F�N�g�������Ƃ���")]
    GameObject _positionObject = default;
    [SerializeField, Header("�G�����ł����O��")]
    Transform _trajectoryParent = default;
    [SerializeField, Tooltip("�Q�[���}�l�W���[")]
    GameManager _manager = default;
    [SerializeField, Tooltip("�b���𐔂���ϐ�(���Z�b�g�����)"), Header("���Ԋ֌W")]
    float _timeReset = default;
    [SerializeField, Header("�ŏ��̓G�𐶐�����܂Œx��")]
    float _firstDelayTime = 7.68f;
    [SerializeField, Header("�e�����̃I�t�Z�b�g")]
    float _generateOffset = default;
    [Tooltip("�G���o�����������b��")]
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };
    [Tooltip("random�ϐ�")]
    int _i = 0;
    [SerializeField, Tooltip("�Q�[���I������")]
    float _gameFinishTime = 60f;

    private void Start()
    {
        _i = Random.Range(0, _timeInterval.Length);
        Debug.Log(_timeInterval[_i]);
    }

    void Update()
    {
        //�A�C�h���^�C�������o���Ȃ��悤�ɂ���
        if (_manager.Started && !_manager.GameEnd)
        {
            //�t�@�������Ȃ��ꍇ�ɐ�������B�{�X��͗�O�ŘA���ŏo��
            if (_manager.CurrentEnemy == null || _manager.BossBattle) 
            {
                _timeReset += Time.deltaTime;

                if (_timeReset >= _timeInterval[_i]) //_timeInterval�𒴂����Instantiate���܂�
                {
                    var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�
                    enemy.SetDeadMovePoints(_trajectoryParent);
                    //�C�x���g��o�^
                    enemy.AddScore += _manager.KillFun;
                    enemy.StageScroll += _manager.Scroller.ScrollOperation;
                    enemy.GiveDamage += _manager.GetDamage;
                    _manager.CurrentEnemy = enemy;
                    _manager.Scroller.ScrollOperation();
                    _i = Random.Range(0, _timeInterval.Length);
                    Debug.Log(_timeInterval[_i]);
                    _timeReset = 0 + _generateOffset;
                }
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
        _timeReset = 0;
    }
}
