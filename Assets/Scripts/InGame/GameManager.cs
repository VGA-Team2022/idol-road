using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [SerializeField, Header("倒したファンをカウント")]
    int _killFunAmount;
    [SerializeField, Header("アイドルパワー")]
    int _idlePower;
    [SerializeField, Header("Maxアイドルパワー")]
    int _maxIdlePower = 100;
    [SerializeField, Header("アイドルのHp")]
    int _idleHp;
    [SerializeField, Header("アイドルのMaxHp")]
    int _maxIdleHp;
    [SerializeField, Header("コンボを数える変数")]
    int _comboAmount;
    [SerializeField, Header("イラストが変わるタイミング")]
    int _comboIllustChange = 5;
    /// <summary>制限時間</summary>
    [SerializeField, Header("制限時間")] 
    float _countTime = 60;
    [SerializeField, Header("カウントダウン")]
    Text _countDownText;
    [SerializeField, Header("倒した敵を表示するテキスト")]
    Text _funCountText;
    [SerializeField]
    StageScroller _stageScroller = default;
    [SerializeField,Header("キャラが透明になっていく経過を管理するクラス")]
    FadeColor _fadeColor;
    [SerializeField,Header("コンボが続いたときに表示させるSprite")]
    GameObject _comboSpriteChara = default;
    /// <summary>現在対象の敵 </summary>
    Enemy _currentEnemy = default;
    /// <summary>ゲームを始めるか否か</summary>
    bool _isStarted;
    /// <summary>アイドルタイムの判定をするBool型</summary>
    bool _isIdleTime;
    /// <summary>ゲームが終わったか否か</summary>
    bool _gameEnd;
    /// <summary>倒したファンをカウントするプロパティ</summary>
    public int KillFunAmount { get => _killFunAmount; set => _killFunAmount = value; }
    /// <summary>アイドルパワーのプロパティ</summary>
    public int IdlePower { get => _idlePower; set => _idlePower = value; }
    /// <summary>アイドルMaxPowerのプロパティ</summary>
    public int MaxIdleHp { get => _maxIdleHp; set => _maxIdleHp = value; }
    /// <summary>コンボ数を管理するプロパティ</summary>
    public int ComboAmount { get => _comboAmount; set => _comboAmount = value; }
    /// <summary>制限時間のプロパティ</summary>
    public float CountTime { get => _countTime; set => _countTime = value; }
    /// <summary>現在対象の敵</summary>
    public Enemy CurrentEnemy { get => _currentEnemy; set => _currentEnemy = value; }
    public StageScroller Scroller { get => _stageScroller;  }
    /// <summary>ゲームフラグのプロパティ</summary>
    public bool GameEnd { get => _gameEnd; set => _gameEnd = value; }
    /// <summary>アイドルタイムフラグのプロパティ</summary>
    public bool IsIdleTime { get => _isIdleTime; }
    private void Awake()
    {
        if(_countDownText ==null)
        {
            Debug.LogError($"Text{_countDownText}がないよ");
        }        
    }
    private void Start()
    {
        _isStarted = false;
        _isIdleTime = false;
        _gameEnd = false;
        _idleHp = _maxIdleHp;
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
                _isStarted = true;
            }
            yield return null;
        }
    }
    /// <summary>ファンを倒した数を数える関数</summary>
    public void KillFun(int kill)
    {
        _killFunAmount += kill;
        _funCountText.text = _killFunAmount.ToString();
        Debug.Log("hit");
    }
    /// <summary>ボタンを押すと呼び出されるカウントダウンの機能</summary>
    public void CountDownButton()
    {
        StartCoroutine(CountDown());
    }   
    /// <summary>アイドルパワーが増加する関数</summary>
    public void IncreseIdlePower(int power)
    {
        _idlePower += power;
    }
    /// <summary>アイドルタイムを判断するための関数</summary>
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
    /// <summary>コンボ数を管理する関数</summary>
    public void ComboAmountTotal()
    {
        _comboAmount++;
        if(_comboAmount == _comboIllustChange)
        {
            IllustDisPlay();
            _comboIllustChange += 5;
        }
        Debug.Log(_comboAmount);
    }
    /// <summary>イラストを表示させる関数</summary>
    public void IllustDisPlay()
    {
        GameObject obj = Instantiate(_comboSpriteChara, transform);
        Destroy(obj, _fadeColor.Span);
    }
}
