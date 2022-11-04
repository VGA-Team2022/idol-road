using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("�|�����t�@�����J�E���g")]
    int _killFunAmount;
    [SerializeField, Header("�A�C�h���p���[")]
    int _idlePower;
    [SerializeField, Header("Max�A�C�h���p���[")]
    int _maxIdlePower = 100;
    [SerializeField, Header("�A�C�h����Hp")]
    int _idleHp;
    [SerializeField, Header("�A�C�h����MaxHp")]
    int _maxIdleHp;
    /// <summary>��������</summary>
    [SerializeField, Header("��������")] 
    float _countTime = 60;
    [SerializeField, Header("�J�E���g�_�E��")]
    Text _countDownText;
    [SerializeField, Header("�|�����G��\������e�L�X�g")]
    Text _funCountText;
    [SerializeField]
    StageScroller _stageScroller = default;
    /// <summary>���ݑΏۂ̓G </summary>
    Enemy _currentEnemy = default;
    /// <summary>�Q�[�����n�߂邩�ۂ�</summary>
    bool _isStarted;
    /// <summary>�A�C�h���^�C���̔��������Bool�^</summary>
    bool _isIdleTime;
    /// <summary>�Q�[�����I��������ۂ�</summary>
    bool _isGameEnd;
    /// <summary>�Q�[�����I��������ۂ�</summary>
    bool _gameEnd;
    /// <summary>�|�����t�@�����J�E���g����v���p�e�B</summary>
    public int KillFunAmount { get => _killFunAmount; set => _killFunAmount = value; }
    /// <summary>�������Ԃ̃v���p�e�B</summary>
    public float CountTime { get => _countTime; set => _countTime = value; }
    /// <summary>���ݑΏۂ̓G</summary>
    public Enemy CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }
    public StageScroller Scroller { get => _stageScroller;  }
    /// <summary>�Q�[���t���O�̃v���p�e�B</summary>
    public bool GameEnd { get => _gameEnd; set => _gameEnd = value; }

    private void Awake()
    {
        if(_countDownText ==null)
        {
            Debug.LogError($"Text{_countDownText}���Ȃ���");
        }        
    }
    private void Start()
    {
        _isStarted = false;
        _isIdleTime = false;
        _gameEnd = false;
        _idleHp = _maxIdlePower;
    }
    void Update()
    {
        if(_isStarted)
        {
            _countTime -= Time.deltaTime;
            if (_countTime <= 0)
            {
                _countTime = 0;
            }
        }
    }
    /// <summary>�J�E���g�_�E���R���[�`��</summary>
    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1.0f);
        for (int i = 3;i>=0;i--)
        {          
            if(i>0)
            {
                _countDownText.text = i.ToString();
                yield return new WaitForSeconds(1.0f);
            }
            else if(i==0)
            {
                _countDownText.text = "Start!";
                yield return new WaitForSeconds(1.0f);
                _countDownText.gameObject.SetActive(false);
                _isStarted = true;
            }
            yield return null;
        }
    }
    /// <summary>�t�@����|�������𐔂���֐�</summary>
    public void KillFun(int kill)
    {
        _killFunAmount += kill;
        _funCountText.text = _killFunAmount.ToString();
        Debug.Log("hit");
    }
    /// <summary>�{�^���������ƌĂяo�����J�E���g�_�E���̋@�\</summary>
    public void CountDownButton()
    {
        StartCoroutine(CountDown());
    }   
    /// <summary>�A�C�h���p���[����������֐�</summary>
    public void IncreseIdlePower(int power)
    {
        _idlePower += power;
    }
    /// <summary>�A�C�h���^�C���𔻒f���邽�߂̊֐�</summary>
    public void IdleTime()
    {
        if(_idlePower<_maxIdlePower)
        {
            _isIdleTime = false;
        }
        else if(_idlePower>=_maxIdlePower)
        {
            _isIdleTime = true;
            _idlePower = 0;
        }    
    }
}
