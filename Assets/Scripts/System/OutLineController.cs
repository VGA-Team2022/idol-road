using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class OutLineController : MonoBehaviour
{
    [SerializeField, Header("点滅させるOutLineを持ったテキスト")]
    TextMeshProUGUI _text;

    [SerializeField , Header("OutLineの最大距離")]
    float _maxWidth = 0.55f;

    [SerializeField , Header("OutLineの最小距離")]
    float _minWidth = 0.20f;

    [SerializeField , Header("何秒かけて偏移させるか")]
    float _changeInterval = 1f;

    Tweener _tween;

    private void Start()
    {
        Initialize();   
    }

    public void Initialize() 
    {
        _text.outlineWidth = _maxWidth;
        FlushingStrat();
    }

    public void FlushingStrat() 
    {
        if(_text.outlineWidth == _maxWidth) 
        {
            _tween = DOTween.To(() => _text.outlineWidth, // 連続的に変化させる対象の値
                       x => _text.outlineWidth = x, // 変化させた値 x をどう処理するかを書く
                       _minWidth, // x をどの値まで変化させるか指示する
                       _changeInterval)
                       .OnComplete(()=>FlushingStrat());
        }
        else 
        {
            _tween = DOTween.To(() => _text.outlineWidth, // 連続的に変化させる対象の値
                       x => _text.outlineWidth = x, // 変化させた値 x をどう処理するかを書く
                       _maxWidth, // x をどの値まで変化させるか指示する
                       _changeInterval)
                       .OnComplete(() => FlushingStrat());
        }
    }

    public void FlushingStop() 
    {
        _tween.Kill();
    }
}
