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
    [SerializeField, Tooltip("���Ԃ�ێ����Ă����ϐ�(���Z�b�g����Ȃ�)")]
    float _time = default;
    [SerializeField, Header("�ŏ��̓G�𐶐�����܂Œx��")]
    float _firstDelayTime = 7f;
    [SerializeField, Header("�e�����̃I�t�Z�b�g")]
    float _generateOffset = default;
    [SerializeField, Tooltip("�G���o�����������b��")]
    float _timeInterval = 1f;
    [SerializeField, Tooltip("�Q�[���I������")]
    float _gameFinishTime = 60f;

    /// <summary> ture�ɂȂ�ƃG�l�~�[���o������悤�ɂ���g���K�[</summary>
    bool _onEnemy = default;

    private void Start()
    {
        _onEnemy = false;�@//�G�l�~�[���ŏ�����N���o���Ȃ����߂�bool�^�̕ϐ����g���A�o�����Ǘ�����B
        _time += _firstDelayTime;
    }

    void Update()
    {
        if (_onEnemy == true) //_onEnemy��true�ɂȂ��_time��_timeReset�������o���A�G�l�~�[���N���o��
        {
            _time += Time.deltaTime;
            _timeReset += Time.deltaTime;

            if (_time < _gameFinishTime) //�Q�[�����Ԃ𒴂����spawn�X�|�[�����Ȃ��悤��
            {
                if (_timeReset > _timeInterval) //_timeInterval�𒴂����Instantiate���܂�
                {
                    if (_manager.CurrentEnemy == null)
                    {
                        var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�
                        enemy.SetDeadMovePoints(_trajectoryParent);
                        enemy.AddScore += _manager.KillFun;
                        enemy.StageScroll += _manager.Scroller.ScrollOperation;
                        _manager.CurrentEnemy = enemy;
                        _manager.Scroller.ScrollOperation();
                    }

                    _timeReset = 0 + _generateOffset;
                }
            }
        }
    }

    /// <summary>
    /// �{�^���ɐݒ肷��p�u���b�N�֐�
    /// true�ɂȂ�ƃG�l�~�[���N���o��
    /// </summary>
    public void OnEnemy()
    {
        _onEnemy = true;
    }

    /// <summary>
    /// �{�^���ɐݒ肷��p�u���b�N�֐�
    /// false�ɂȂ�ƃG�l�~�[�̏o�����~�܂�
    /// ���Q�[���̎��Ԃ�G�l�~�[���o�Ă���C���^�[�o���̎��Ԃ����ׂă��Z�b�g
    /// </summary>
    public void OffEnemy()
    {
        _onEnemy = false;

        _time = 0;
        _timeReset = 0;
    }
}