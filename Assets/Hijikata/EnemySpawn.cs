using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField, Tooltip("�G�l�~�[�������Ƃ���"), Header("�G�l�~�[�֌W")] 
    GameObject _enemyPrefub = default;
    [SerializeField, Tooltip("�G�l�~�[���o�����������I�u�W�F�N�g�������Ƃ���")]
    GameObject _positionObject = default;
    [SerializeField, Tooltip("�b���𐔂���ϐ�(���Z�b�g�����)"), Header("���Ԋ֌W")] 
    float _timeReset = default;
    [SerializeField, Tooltip("���Ԃ�ێ����Ă����ϐ�(���Z�b�g����Ȃ�)")] 
    float _time = default;
    [SerializeField, Tooltip("�G���o�����������b��")] 
    float _timeInterval = 5f;
    [SerializeField, Tooltip("�Q�[���I������")] 
    float _gameFinishTime = 60f;

    /// <summary> ture�ɂȂ�ƃG�l�~�[���o������悤�ɂ���g���K�[</summary>
    bool _onEnemy = default;

    private void Start()
    {
        _onEnemy = false;�@//�G�l�~�[���ŏ�����N���o���Ȃ����߂�bool�^�̕ϐ����g���A�o�����Ǘ�����B
    }

    void Update()
    {
        if( _onEnemy == true) //_onEnemy��true�ɂȂ��_time��_timeReset�������o���A�G�l�~�[���N���o��
        {
            _time += Time.deltaTime;
            _timeReset += Time.deltaTime;

            if (_time < _gameFinishTime) //�Q�[�����Ԃ𒴂����spawn�X�|�[�����Ȃ��悤��
            {
                if (_timeReset > _timeInterval) //_timeInterval�𒴂����Instantiate���܂�
                {
                    Instantiate(_enemyPrefub, _positionObject.transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�
                    _timeReset = 0f;
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
