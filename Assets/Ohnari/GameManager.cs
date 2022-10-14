using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    /// <summary>�Q�[���}�l�[�W���[�̃C���X�^���X</summary>
    public static GameManager instance;
    [SerializeField]
    [Header("�|�����t�@�����J�E���g")] int _killFunAmount;
    /// <summary>��������</summary>
    [SerializeField] 
    [Header("��������")]float _countTime = 60;
    [SerializeField]
    [Header("�J�E���g�_�E��")] Text _countDownText;
    /// <summary>�Q�[�����n�߂邩�ۂ�</summary>
    bool isStarted;
    /// <summary>�|�����t�@�����J�E���g����v���p�e�B</summary>
    public int KillFunAmount { get => _killFunAmount; set => _killFunAmount = value; }
    /// <summary>�������Ԃ̃v���p�e�B</summary>
    public float CountTime { get => _countTime; set => _countTime = value; }
    private void Awake()
    {
        //�V�[���J�ڂ��Ă��I�u�W�F�N�g���j�󂳂�Ȃ��悤�ɂ���B
        if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if(_countDownText ==null)
        {
            Debug.LogError($"Text{_countDownText}���Ȃ���");
        }
        isStarted = false;
    }
    private void Start()
    {
        StartCoroutine(CountDown());
    }
    void Update()
    {
        if(isStarted)
        {
            _countTime -= Time.deltaTime;
            if (_countTime <= 0)
            {
                _countTime = 0;
            }
            Debug.Log("�Q�[�����n�܂���");
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
                isStarted = true;
            }
            yield return null;
        }
    }
    /// <summary>�t�@����|�������𐔂���֐�</summary>
    public void KillFun(int kill)
    {
        _killFunAmount += kill;
    }
    
}
