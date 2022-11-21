using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;



public class SuperIdolTime : MonoBehaviour
{
    /// <summary>スーパーアイドルタイム中のゲージがマックスになるまでの回数</summary>
    [SerializeField]
    private int _gaugeCountMax = 10;
    /// <summary>スーパーアイドルタイムの持続時間</summary>
    [SerializeField]
    private float _timeEndSuperIdolTime = 15;
    /// <summary>スーパーアイドルタイムの経過時間</summary>
    [SerializeField]
    private float _elapsed = 0;
    /// <summary>一タップで溜まるゲージの変化にかかる時間</summary>
    [SerializeField]
    private float _gaugePlusTime = 0.5f;
    [SerializeField]
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
    [SerializeField]
    private GameObject _gaugeObject = default;
    /// <summary> ゲームマネージャー</summary>
    [SerializeField]
    private GameManager _manager = default;

    /// <summary>スーパーアイドルタイム中のタップされた回数</summary>
    private int _gaugeCount = 0;
    /// <summary></summary>
    private float _gaugeLength = 0;
    

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
            _gaugeLength = (float)_gaugeCount / _gaugeCountMax;
            if (_imageGauge != null) {
                //ネットで調べて参考にした、ゲージの値をなめらかに変える処理
                var sequence = DOTween.Sequence();
                sequence.Append(_imageGauge.DOFillAmount(_gaugeLength, _gaugePlusTime));
                //GaugeAdvance(_gaugeLength);
            }
            if(_gaugeLength > 1)
            {
                _gaugeLength = 1;
                _isGaugeMax = true;
                //Debug.Log("Full");
            }
            //Debug.Log($"max:{_gaugeCountMax},count:{_gaugeCount},gauge:{_gaugeLength}");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if (Input.GetButtonDown("Fire1"))
        {
            GaugeCount++;
        }
        if(_isGaugeMax && _isTimeMax)
        {
            EndSuperIdolTime();
            _isGaugeMax= false;
            _gaugeCount = 0;
        }
    }

    private void FixedUpdate()
    {
        _elapsed += Time.deltaTime;
        if(_elapsed > _timeEndSuperIdolTime)
        {
            _isTimeMax = true;
            Debug.Log("終了");
        }
        Debug.Log($"{(int)_elapsed}秒");
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
                .Append(_imageExplosion.transform.DOScale(Vector3.zero, 1f));
        Debug.Log("bakuhatu");
        _gaugeObject.SetActive(false);
    }
}
