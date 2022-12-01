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
    float _gameTime = default;
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
    /// <summary> ture�ɂȂ�ƃG�l�~�[���o������悤�ɂ���g���K�[</summary>
    [SerializeField]
    bool _onEnemy = default;
    /// <summary>�X�^�[�g�t���O</summary>
    bool _isStart = default;
    private void Start()
    {
        _onEnemy = false;�@//�G�l�~�[���ŏ�����N���o���Ȃ����߂�bool�^�̕ϐ����g���A�o�����Ǘ�����B
        _isStart = false;
        _i = Random.Range(0, _timeInterval.Length);
        Debug.Log(_timeInterval[_i]);
    }

    void Update()
    {
        if (_isStart == true && _gameTime < _gameFinishTime)//�Q�[�����Ԃ𒴂����spawn�X�|�[�����Ȃ��悤��
        {
            _gameTime += Time.deltaTime;

            //_onEnemy��true�ɂȂ��_time��_timeReset�������o���A�G�l�~�[���N���o��
            //�����Ƀ{�X��̃t���O��or�œ��ꂽ��
            if (_onEnemy == true && _manager.CurrentEnemy == null) 
            {
                _timeReset += Time.deltaTime;

                if (_timeReset >= _timeInterval[_i]) //_timeInterval�𒴂����Instantiate���܂�
                {
                    //if (_manager.CurrentEnemy == null)
                    //{
                    var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //�V���A���C�Y�Őݒ肵���I�u�W�F�N�g�̏ꏊ�ɏo�����܂�
                    enemy.SetDeadMovePoints(_trajectoryParent);
                    //�C�x���g��o�^
                    enemy.AddScore += _manager.KillFun;
                    enemy.StageScroll += _manager.Scroller.ScrollOperation;
                    enemy.GiveDamage += _manager.GetDamage;

                    _manager.CurrentEnemy = enemy;
                    _manager.Scroller.ScrollOperation();
                    //_onEnemy = false;//������False�ɂ��Ȃ��Ɛ����Ǘ��̎��Ԃ��i��ł��܂�
                    _i = Random.Range(0, _timeInterval.Length);
                    Debug.Log(_timeInterval[_i]);
                    //}
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
        _isStart = true;
    }
    /// <summary>�v���C���̐����t���O���Ǘ�����֐�</summary>
    public void InGameOnEnemy()
    {
        StartCoroutine(OnEnemyCoroutine());
    }
    /// <summary>Enemy�̐����t���O��1�b���true�ɂ���֐�</summary>
    public IEnumerator OnEnemyCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        _onEnemy = true;
        yield return null;
    }

    /// <summary>
    /// �{�^���ɐݒ肷��p�u���b�N�֐�
    /// false�ɂȂ�ƃG�l�~�[�̏o�����~�܂�
    /// ���Q�[���̎��Ԃ�G�l�~�[���o�Ă���C���^�[�o���̎��Ԃ����ׂă��Z�b�g
    /// </summary>
    public void OffEnemy()
    {
        _onEnemy = false;

        _gameTime = 0;
        _timeReset = 0;
    }
}
