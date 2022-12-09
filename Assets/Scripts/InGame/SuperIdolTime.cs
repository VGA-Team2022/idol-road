using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Video;
using System.Net;
using Unity.Burst;

public class SuperIdolTime : MonoBehaviour
{
    [SerializeField,Tooltip("スーパーアイドルタイム中のゲージがマックスになるまでの回数")]
    private int _gaugeCountMax = 10;
    [SerializeField,Tooltip("スーパーアイドルタイムの持続時間")]
    private float _timeEndSuperIdolTime = 15;
    [SerializeField,Tooltip("スーパーアイドルタイムの経過時間")]
    private float _elapsed = 0;
    [SerializeField,Tooltip("一タップで溜まるゲージの変化にかかる時間")]
    private float _gaugePlusTime = 0.5f;

    [SerializeField,Tooltip("")]
    private float _imageLange = 5.62f;
    /// <summary>時間が過ぎているかの判定</summary>
    [SerializeField]
    private bool _isGaugeMax = false;
    /// <summary>ゲージが溜まりきっているかの判定</summary>
    [SerializeField]
    private bool _isTimeMax = false;
    [SerializeField,Tooltip("ゲージの画像")]
    private Image _imageGauge = default;
    [SerializeField, Tooltip("爆発の画像")]
    private Image _imageExplosion = default;
    [SerializeField, Tooltip("フェード用のパネル")]
    private Image _fadePanel = default;
    [SerializeField,Tooltip("スーパーアイドルタイム画面のUIの親オブジェクト")]
    private GameObject _superIdolTimeObject = default;
    [SerializeField, Tooltip("スーパーアイドルタイム画面の背景絵")]
    private GameObject _backGroundPanel = default;
    [SerializeField, Tooltip("通常画面のCanvas")]
    private GameObject _normalCanvas = default;
    [SerializeField,Tooltip("通常時のプレイヤー")]
    private GameObject _normalPlayer = default;
    [SerializeField, Tooltip("ゲージに合わせて出てくるファンの下段")]
    private GameObject _BackDownFans = default;
    [SerializeField, Tooltip("下段のファンが出てくる値")]
    private float _downFansValue = 0.9f;
    [SerializeField, Tooltip("ゲージに合わせて出てくるファンの中段")]
    private GameObject _BackMiddleFans = default;
    [SerializeField, Tooltip("中段のファンが出てくる値")]
    private float _middleFansValue = 0.6f;
    [SerializeField, Tooltip("ゲージに合わせて出てくるファンの上段")]
    private GameObject _BackUpFans = default;
    [SerializeField, Tooltip("上段のファンが出てくる値")]
    private float _upFansValue = 0.3f;
    [SerializeField, Tooltip("背景に流すビデオのプレーヤー")]
    private VideoPlayer _videoPlayer = default;
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
        _videoPlayer.gameObject.SetActive(true);
        _videoPlayer.Play();
        _normalCanvas.gameObject.SetActive(false);
        _normalPlayer.gameObject.SetActive(false);
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
        var sequence = DOTween.Sequence();
        sequence.Append(_imageExplosion.transform.DOScale(new Vector3(_imageLange, _imageLange, _imageLange), 0.5f))
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f))
                .Append(_fadePanel.DOFade(1,0.5f))
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
        _normalCanvas.SetActive(true);
        _normalPlayer.SetActive(true);
        _superIdolTimeObject.SetActive(false);
        _fadePanel.DOFade(0, 0.25f);
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
        _superIdolTimeObject.SetActive(true);
        isSuperIdolTime = true;
        _videoPlayer.gameObject.SetActive(false);
        _backGroundPanel.gameObject.SetActive(true);
        //Debug.Log("endvideo");
    }
}
