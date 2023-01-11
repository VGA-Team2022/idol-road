using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;
using System.Net;
using Unity.Burst;
using System.Linq;
using UnityEngine.Rendering;

public class SuperIdolTime : MonoBehaviour
{
    SuperIdolTimeParamater _paramater => LevelManager.Instance.CurrentLevel.IdolTimeParamater;
    PlayResult _result => PlayResult.Instance;
    [SerializeField, Header("GameManager")]
    GameManager _manager;
    [SerializeField,Header("FadeController")]
    FadeController _fadeController;
    [SerializeField, Header("スーパーアイドルタイムの経過時間")]
    private float _elapsed = 0;
    [SerializeField, Header("一タップで溜まるゲージの変化にかかる時間")]
    private const float _gaugePlusTime = 0.5f;

    [SerializeField, Header("爆発の画像の最大拡大サイズ")]
    private float _imageLange = 5.62f;
    [SerializeField,Header("ゲージが満タンかどうか")]
    private bool _isGaugeMax = false;

    [SerializeField, Header("通常時のオブジェクト"), ElementNames(new string[] {
        "EnemySpawner","CenterSpawnPoint","RightSpawnPoint","LeftSpawnPoint","BossSpawnPoint",
        "ItemGanarator","LeftGeneratePoint","LeftArrivalPoint","RightGeneratePoint","RightArrivalPoint",
        "BackGround","Player","StageObject","BlowingSpawnPosition","InGameCanvas"
    })]
    private GameObject[] _normalObjects = default;

    //スーパーアイドルタイム時のオブジェクト
    [SerializeField, Header("ゲージのImage")]
    private Image _imageGauge = default;
    [SerializeField, Header("爆発のImage")]
    private Image _imageExplosion = default;
    [SerializeField, Header("フェード用のパネル")]
    private Image _fadePanel = default;
    [SerializeField, Header("スーパーアイドルタイムの後面のUI")]
    private GameObject _superIdolTimeBackUI = default;
    [SerializeField, Header("スーパーアイドルタイムの前面のUI")]
    private GameObject _superIdolTimeFrontUI = default;
    [SerializeField, Header("スーパーアイドルタイム画面の背景絵")]
    private GameObject _backGroundPanel = default;
    [SerializeField, Header("下段のファン"),Tooltip("ゲージに合わせて出てくるファンの下段")]
    private GameObject _BackDownFans = default;
    [SerializeField, Header("下段のファンが出てくる値")]
    private float _downFansValue = 0.3f;
    [SerializeField, Header("中段のファン"), Tooltip("ゲージに合わせて出てくるファンの中段")]
    private GameObject _BackMiddleFans = default;
    [SerializeField, Header("中段のファンが出てくる値")]
    private float _middleFansValue = 0.6f;
    [SerializeField, Header("上段のファン"), Tooltip("ゲージに合わせて出てくるファンの下段")]
    private GameObject _BackUpFans = default;
    [SerializeField, Header("上段のファンが出てくる値")]
    private float _upFansValue = 0.9f;
    [SerializeField, Header("カットイン用ビデオプレーヤー")]
    private VideoPlayer _videoPlayer = default;
    [SerializeField,Header("アイドルの踊っているImage")]
    private Image _dancingIdolImage = default;
    [SerializeField,Header("キラキラのエフェクト")]
    private ParticleSystem _shiningParticle = default;
    /// <summary>スーパーアイドルタイム中のタップされた回数</summary>
    private int _gaugeCount = 0;
    /// <summary>ゲージの溜まり具合</summary>
    private float _gaugeLength = 0;
    private bool isDownEnemy = false;
    private bool isMiddleEnemy = false;
    private bool isUpEnemy = false;
    /// <summary> 入力受付判定</summary>
    private bool isSuperIdolTime = false;
    private IState _currentState = default;
    /// <summary>ゲージを増加させる</summary>
    public int GaugeCount
    {
        get => _gaugeCount;
        set
        {
            _gaugeCount = value;
            GaugeIncrease();
        }
    }
    public IState CurrentState
    {
        get => _currentState;
        set 
        {
            _currentState = value;
            Debug.Log(_currentState);
        }
    }
    public bool IsSuperIdolTime => isSuperIdolTime;
    
    private void OnEnable()
    {
        _superIdolTimeBackUI.SetActive(true);
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        foreach (var obj in _normalObjects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (isSuperIdolTime == true)
        //{
        //    //デバッグ用
        //    if (Input.GetButtonDown("Fire1"))
        //    {
        //        GaugeCount++;
        //    }
        //}
        _videoPlayer.loopPointReached += EndVideo;
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log(_manager.CurrentGameState);
        }
    }
    private void FixedUpdate()
    {
        if (isSuperIdolTime == true)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _paramater.TimeEndSuperIdolTime)
            {
                _elapsed = 0;
                CheckGauge();
                isSuperIdolTime = false;
                _manager.ChangeGameState(_currentState);
                Debug.Log(_currentState);
            }
        }
    }
    /// <summary>開始時に呼び出す処理</summary>
    public void StartSuperIdolTime()
    {
        _gaugeLength = 0;
        _elapsed = 0;
        _gaugeCount = 0;
        _imageGauge.fillAmount = 0;
        _superIdolTimeBackUI.SetActive(true);
        _videoPlayer.Play();
        foreach (var obj in _normalObjects)
        {
            obj.SetActive(false);
        }
    }
    /// <summary>終了時に呼び出す処理</summary>
    public void InitializationProcess()
    {
        _dancingIdolImage.gameObject.SetActive(false);
        _backGroundPanel.gameObject.SetActive(false);
        _BackDownFans.GetComponent<Animator>().Play("Idol");
        isDownEnemy = false;
        _BackMiddleFans.GetComponent<Animator>().Play("Idol");
        isMiddleEnemy = false;
        _BackUpFans.GetComponent<Animator>().Play("Idol");
        isUpEnemy = false; 
        _isGaugeMax = false;
    }
    /// <summary>スーパーアイドルタイムの終了時に処理を判断</summary>
    private void CheckGauge()
    {
        if (_isGaugeMax)
        {
            GorgeousEndProcess();
            _result.ValueSuperIdleTimePerfect += _paramater.SuccessScore;
        }
        else if (!_isGaugeMax)
        {
            PlainEndProcess();
        }
    }
    /// <summary>スーパーアイドルタイムのゲージをマックスで終了した際の豪華な処理</summary>
    private void GorgeousEndProcess()
    {
        //後ろのファンを飛ばす
        _BackDownFans.GetComponent<Animator>().Play("Burst");
        _BackMiddleFans.GetComponent<Animator>().Play("Burst");
        _BackUpFans.GetComponent<Animator>().Play("Burst");
        var sequence = DOTween.Sequence();
        sequence.Append(_imageExplosion.transform.DOScale(new Vector3(_imageLange, _imageLange, _imageLange), 0.5f))
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f))
                .OnComplete(() => { SwitchDisplayObject(); });
    }
    /// <summary>スーパーアイドルタイムのゲージが溜まり切らなかった際の簡素な処理</summary>
    private void PlainEndProcess()
    {
        SwitchDisplayObject();
    }
    /// <summary>終了時の切り替え処理</summary>
    private void SwitchDisplayObject()
    {
        _superIdolTimeFrontUI.SetActive(false);
        _shiningParticle.gameObject.SetActive(false);
        _fadeController.FadeOut(() => 
        {
            _superIdolTimeBackUI.SetActive(false);
            _videoPlayer.gameObject.SetActive(true);
            InitializationProcess();
            this.gameObject.SetActive(false);
            foreach (var obj in _normalObjects)
            {
                obj.SetActive(true);
                var enemies = FindObjectsOfType<EnemyBase>();
                foreach (var enemy in enemies)
                {
                    enemy.GetComponent<EnemyBase>().Destroyed();
                }
            }
            _fadeController.FadeIn();
        });
    }
    /// <summary>タップゲージの増加に関する処理</summary>
    private void GaugeIncrease()
    {
        _gaugeLength = (float)_gaugeCount / _paramater.GaugeCountMax;

        if (_imageGauge != null)
        {
            //ゲージの値をなめらかに変える処理
            var sequence = DOTween.Sequence();
            sequence.Append(_imageGauge.DOFillAmount(_gaugeLength, _gaugePlusTime));
        }
        if (_gaugeLength > _downFansValue && isDownEnemy == false)
        {
            isDownEnemy = true;
            _BackDownFans.GetComponent<Animator>().Play("SlideIn");
        }
        if (_gaugeLength > _middleFansValue && isMiddleEnemy == false)
        {
            isMiddleEnemy = true;
            _BackMiddleFans.GetComponent<Animator>().Play("SlideIn");
        }
        if (_gaugeLength > _upFansValue && isUpEnemy == false)
        {
            isUpEnemy = true;
            _BackUpFans.GetComponent<Animator>().Play("SlideIn");
        }
        if (_gaugeLength > 1)
        {
            _gaugeLength = 1;
            _isGaugeMax = true;
        }
    }
    /// <summary>カットインビデオが終わった瞬間の処理</summary>
    /// <param name="vp"></param>
    private void EndVideo(VideoPlayer vp)
    {
        _superIdolTimeFrontUI.SetActive(true);
        isSuperIdolTime = true;
        _videoPlayer.gameObject.SetActive(false);
        _dancingIdolImage.gameObject.SetActive(true);
        _backGroundPanel.gameObject.SetActive(true);
        _shiningParticle.gameObject.SetActive(true);
        _shiningParticle.Play();
    }
}
