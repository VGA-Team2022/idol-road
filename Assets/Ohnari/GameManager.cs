using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    /// <summary>ゲームマネージャーのインスタンス</summary>
    public static GameManager instance;
    [SerializeField]
    [Header("倒したファンをカウント")] int _killFunAmount;
    /// <summary>制限時間</summary>
    [SerializeField] 
    [Header("制限時間")]float _countTime = 60;
    [SerializeField]
    [Header("カウントダウン")] Text _countDownText;
    /// <summary>ゲームを始めるか否か</summary>
    bool isStarted;
    /// <summary>倒したファンをカウントするプロパティ</summary>
    public int KillFunAmount { get => _killFunAmount; set => _killFunAmount = value; }
    /// <summary>制限時間のプロパティ</summary>
    public float CountTime { get => _countTime; set => _countTime = value; }
    private void Awake()
    {
        //シーン遷移してもオブジェクトが破壊されないようにする。
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
            Debug.LogError($"Text{_countDownText}がないよ");
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
            Debug.Log("ゲームが始まった");
        }
        
    }
    /// <summary>カウントダウンコルーチン</summary>
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
    /// <summary>ファンを倒した数を数える関数</summary>
    public void KillFun(int kill)
    {
        _killFunAmount += kill;
    }
    
}
