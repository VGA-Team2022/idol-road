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

public class SuperIdolTime : MonoBehaviour
{
    [SerializeField, Header("ゲージがマックスになる回数")]
    private int _gaugeCountMax = 10;
    [SerializeField, Header("スーパーアイドルタイムの持続時間")]
    private float _timeEndSuperIdolTime = 15;
    [SerializeField, Header("スーパーアイドルタイムの経過時間")]
    private float _elapsed = 0;
    [SerializeField, Header("一タップで溜まるゲージの変化にかかる時間")]
    private float _gaugePlusTime = 0.5f;

    [SerializeField, Header("爆発の画像の最大拡大サイズ")]
    private float _imageLange = 5.62f;
    /// <summary>時間が過ぎているかの判定</summary>
    [SerializeField,Header("ゲージが満タンかどうか")]
    private bool _isGaugeMax = false;
    /// <summary>ゲージが溜まりきっているかの判定</summary>
    [SerializeField,Header("持続時間を超えたかどうか")]
    private bool _isTimeMax = false;

    [SerializeField, Header("通常時のオブジェクト"), ElementNames(new string[] {
        "EnemySpawner","CenterSpawnPoint","RightSpawnPoint","LeftSpawnPoint","BossSpawnPoint",
        "ItemGanarator","LeftGeneratePoint","LeftArrivalPoint","RightGeneratePoint","RightArrivalPoint",
        "BackGround","Player","StageObject","BlowingSpawnPosition","InGameCanvas",
        "WarningTimeLine"
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
    private float _downFansValue = 0.9f;
    [SerializeField, Header("中段のファン"), Tooltip("ゲージに合わせて出てくるファンの中段")]
    private GameObject _BackMiddleFans = default;
    [SerializeField, Header("中段のファンが出てくる値")]
    private float _middleFansValue = 0.6f;
    [SerializeField, Header("上段のファン"), Tooltip("ゲージに合わせて出てくるファンの下段")]
    private GameObject _BackUpFans = default;
    [SerializeField, Header("上段のファンが出てくる値")]
    private float _upFansValue = 0.3f;
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
    /// <summary>ゲージの最大値</summary>
    public int GaugeCountMax
    {
        get => _gaugeCountMax;
        set => _gaugeCountMax = value;
    }

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
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        _superIdolTimeBackUI.SetActive(true);
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        foreach(var obj in _normalObjects)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSuperIdolTime == true)
        {
            //デバッグ用
            if (Input.GetButtonDown("Fire1"))
            {
                GaugeCount++;
            }
            if (_isGaugeMax && _isTimeMax)
            {
                EndSuperIdolTime();
                _isGaugeMax = false;
                _gaugeCount = 0;
            }
        }
        _videoPlayer.loopPointReached += EndVideo;
    }

  

    private void FixedUpdate()
    {
        if (isSuperIdolTime == true)
        {
            _elapsed += Time.deltaTime;
            if (_elapsed > _timeEndSuperIdolTime)
            {
                _isTimeMax = true;
                //Debug.Log("終了");
            }
            //Debug.Log($"{(int)_elapsed}秒");
        }
    }

    /// <summary>
    /// 鈴木先生のサンプルを参照した、ゲージの値をなめらかに変える関数
    /// </summary>
    /// <param name="value"></param>
    void GaugeAdvance(float value)
    {
        DOTween.To(() => _imageGauge.fillAmount,
            x => _imageGauge.fillAmount = x,
            value,
            _gaugePlusTime);
    }
    public void EndSuperIdolTime()
    {
        _superIdolTimeFrontUI.SetActive(false);
        var sequence = DOTween.Sequence();
        sequence.Append(_imageExplosion.transform.DOScale(new Vector3(_imageLange, _imageLange, _imageLange), 0.5f))
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f))
                .OnComplete(() => { SwitchDisplayObject(); });
        isSuperIdolTime = false;
        //後ろのファンを飛ばす
        _BackDownFans.GetComponent<Animator>().Play("Burst");
        _BackMiddleFans.GetComponent<Animator>().Play("Burst");
        _BackUpFans.GetComponent<Animator>().Play("Burst");
        this.gameObject.SetActive(false);
    }
    public void SwitchDisplayObject()
    {
        //_normalCanvas.SetActive(true);
        //_normalPlayer.SetActive(true);
        //_normalRoad.SetActive(true);
        //_normalFan.SetActive(true);
        foreach (var obj in _normalObjects)
        {
            obj.SetActive(true);
        }
        _superIdolTimeBackUI.SetActive(false);
        _fadePanel.DOFade(0, 0.5f);
    }
    public void GaugeIncrease()
    {
        _gaugeLength = (float)_gaugeCount / _gaugeCountMax;
        if (_imageGauge != null)
        {
            //ネットで調べて参考にした、ゲージの値をなめらかに変える処理
            var sequence = DOTween.Sequence();
            sequence.Append(_imageGauge.DOFillAmount(_gaugeLength, _gaugePlusTime));
            //GaugeAdvance(_gaugeLength);
        }
        if (_gaugeLength > _downFansValue && isDownEnemy == false)
        {
            isDownEnemy = true;
            _BackDownFans.GetComponent<Animator>().Play("SlideIn");
            Debug.Log("Down");
        }
        if (_gaugeLength > _middleFansValue && isMiddleEnemy == false)
        {
            isMiddleEnemy = true;
            _BackMiddleFans.GetComponent<Animator>().Play("SlideIn");
            Debug.Log("Middle");
        }
        if (_gaugeLength > _upFansValue && isUpEnemy == false)
        {
            isUpEnemy = true;
            _BackUpFans.GetComponent<Animator>().Play("SlideIn");
            Debug.Log("Up");
        }
        if (_gaugeLength > 1)
        {
            _gaugeLength = 1;
            _isGaugeMax = true;
            //Debug.Log("Full");
            
        }
        //Debug.Log($"max:{_gaugeCountMax},count:{_gaugeCount},gauge:{_gaugeLength}");
    }
    private void EndVideo(VideoPlayer vp)
    {

        _superIdolTimeFrontUI.SetActive(true);
        isSuperIdolTime = true;
        _videoPlayer.gameObject.SetActive(false);
        //_danceIdolPlayer.gameObject.SetActive(true);
        //_danceIdolPlayer.Play();
        _dancingIdolImage.gameObject.SetActive(true);
        _backGroundPanel.gameObject.SetActive(true);
        _shiningParticle.gameObject.SetActive(true);
        _shiningParticle.Play();
        //Debug.Log("endvideo");
    }
}
