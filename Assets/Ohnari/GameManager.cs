using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>�Q�[���}�l�[�W���[�̃C���X�^���X</summary>
    public static GameManager instance;
    [SerializeField]
    [Header("�|�����t�@�����J�E���g")] int _killFunAmount;
    /// <summary>��������</summary>
    [SerializeField] 
    [Header("��������")]float _countTime = 60;
    /// <summary>�|�����t�@�����J�E���g����v���p�e�B</summary>
    public int KillFunAmount { get => _killFunAmount; set => _killFunAmount = value; }
    /// <summary>�������Ԃ̃v���p�e�B</summary>
    public float CountTime { get => _countTime; set => _countTime = value; }
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        _countTime -= Time.deltaTime;
        if(_countTime <=0)
        {
            _countTime = 0;
        }
    }
    public void KillFun(int kill)
    {
        _killFunAmount += kill;
    }
}
