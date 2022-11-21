using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeColor : MonoBehaviour
{
    [SerializeField,Header("タイマー関連の変数")] 
    float _span = 5.0f;
    /// <summary>透明管理のタイマー</summary>
    float _time = 0;
    /// <summary>無色になっていくフラグ</summary>
    bool _isFade;
    /// <summary>透明になる秒数のプロパティ</summary>
    public float Span { get => _span; set => _span = value; }
    private void Start()
    {
        _isFade = false;
    }
    private void Update()
      {
        _time += Time.deltaTime;
        if (_isFade&&_time<_span)
        {          
            float alpha = 1.0f - _time / _span;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = alpha;
            GetComponent<SpriteRenderer>().color = color;
        }
      }
      /// <summary>アニメーション終了時に呼び出される関数</summary>
      public void FadeCharacter()
      {
          _isFade = true;
      }
}
