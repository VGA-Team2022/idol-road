using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn2 : MonoBehaviour
{
    [Header("�G�l�~�[�֌W")]

    [SerializeField, Tooltip("�G�l�~�[�������Ƃ���")] GameObject _enemyPrefub = default;

    [SerializeField, Tooltip("�G�l�~�[���o��������Vector2")] Vector2 _spawnPosition = default;

    [Header("���Ԋ֌W")]

    [SerializeField, Tooltip("�b���𐔂���ϐ�(���Z�b�g�����)")] float _timeReset = default;

    [SerializeField, Tooltip("���Ԃ�ێ����Ă����ϐ�(���Z�b�g����Ȃ�)")] float _time = default;

    [SerializeField, Tooltip("�G���o�����������b��")] float _timeInterval = 5f;

    [SerializeField, Tooltip("�Q�[���I������")] float _gameFinishTime = 60f;

    void Update()
    {
        _time += Time.deltaTime;
        _timeReset += Time.deltaTime;

        if (_time < _gameFinishTime) //�Q�[�����Ԃ𒴂����spawn�X�|�[�����Ȃ��悤��
        {
            if (_timeReset > _timeInterval) //_timeInterval�𒴂����Instantiate���܂�
            {
                Instantiate(_enemyPrefub, _spawnPosition, Quaternion.identity); //�V���A���C�Y�Őݒ肵��X,Y�̏ꏊ�ɂ����܂�
                _timeReset = 0f;
            }
        }
    }
}
