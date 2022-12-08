using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>フェード関連の処理を行うクラス</summary>
public class FadeController : MonoBehaviour
{
    /// <summary>α値の最大値 </summary>
    const float MAX_α_VALUE = 1f;
    /// <summary>α値の最小値 </summary>
    const float MIN_α_VALUE = 0f;

    [SerializeField, Header("フェードインにかかる時間")]
    float _fadeInTime = 0f;
    [SerializeField, Header("フェードアウトにかかる時間")]
    float _fadeOutTime = 0f;
    [SerializeField, Tooltip("フェードを行うパネル")]
    Image _panel = default;

    /// <summary>フェードインを行う処理</summary>
    /// <param name="action">フェードイン終了時処理</param>
    public void FadeIn(Action action = null)
    {
        _panel.DOFade(MAX_α_VALUE, _fadeInTime)
            .OnComplete(() => action?.Invoke());
    }

    /// <summary>フェードアウトを行う処理</summary>
    /// <param name="action">フェードアウト終了時処理</param>
    public void FadeOut(Action action = null)
    {
        _panel?.DOFade(MIN_α_VALUE, _fadeOutTime)
            .OnComplete(() => action?.Invoke());
    }
}
