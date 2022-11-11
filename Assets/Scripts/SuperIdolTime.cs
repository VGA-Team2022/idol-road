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
    [SerializeField]
    private float _gaugePlusTime = 0.5f;
    [SerializeField]
    private Image _imageGauge = default;
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

    /// <summary>！！！ScreenInputクラスで呼び出すこと！！！</summary>
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

    }
}
