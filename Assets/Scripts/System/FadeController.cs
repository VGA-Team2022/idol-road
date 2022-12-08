using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>フェード関連の処理を行うクラス</summary>
public class FadeController : MonoBehaviour
{
    [SerializeField, Header("フェードインにかかる時間")]
    float _fadeInTime = 0f;
    [SerializeField, Header("フェードアウトにかかる時間")]
    float _fadeOutTime = 0f;
    [SerializeField, Tooltip("フェードを行うパネル")]
    Image _panel = default;

    /// <summary>フェードインを行う処理</summary>
    /// <param name="action">フェードイン終了時処理</param>
    public bool FadeIn(Action action = null)
    {
        _panel.DOFade(1f, _fadeInTime)
            .OnComplete(() => action?.Invoke());

        return true;
    }

    /// <summary>フェードアウトを行う処理</summary>
    /// <param name="action">フェードアウト終了時処理</param>
    public bool FadeOut(Action action = null)
    {
        _panel?.DOFade(0f, _fadeOutTime)
            .OnComplete(() => action?.Invoke());

        return true;
    }
}
