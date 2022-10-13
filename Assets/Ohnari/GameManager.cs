using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /// <summary>ゲームマネージャーのインスタンス</summary>
    public static GameManager instance;
    [SerializeField]
    [Header("倒したファンをカウント")] int _killFunAmount;
    /// <summary>制限時間</summary>
    [SerializeField] 
    [Header("制限時間")]float _countTime = 60;
    /// <summary>倒したファンをカウントするプロパティ</summary>
    public int KillFunAmount { get => _killFunAmount; set => _killFunAmount = value; }
    /// <summary>制限時間のプロパティ</summary>
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
