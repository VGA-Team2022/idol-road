using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>ストーリーを表示する為のクラス </summary>
public class StoryPrinter : MonoBehaviour
{
    [SerializeField, Header("テキストを表示するまでにかかる秒数")]
    float _showTextTime = 2f;
    [SerializeField, Header("次のテキストを表示するまでの待機時間")]
    float _textWaitTime = 2f;
    [SerializeField, Header("フェードを行うまでの待機時間")]
    float _fadeWaitTime = 2f;

    [SerializeField, Tooltip("フェード行うオブジェクト")]
    FadeController _fadeController = default;

    [SerializeField, Tooltip("表示するストーリーのテキスト配列")]
    TMP_Text[] _storyTexts = default;
    /// <summary>表示したテキストの添え字 </summary>
    int _currnetTextIndex = 0;

    /// <summary>シーン遷移を行ったかどうか </summary>
    bool _isTransition = false;

    private void OnEnable()
    {
        StartCoroutine(ShowStory());
    }

    /// <summary>ストーリーを表示する </summary>
    IEnumerator ShowStory()
    {
        for (var i = 0; i < _storyTexts.Length; i++)
        {
            yield return new WaitForSeconds(_textWaitTime);
            ShowText();
        }

        yield return new WaitForSeconds(_fadeWaitTime);

        if (!_isTransition)
        {
            _fadeController.FadeOut(() =>
            {
                gameObject.SetActive(false);
                _fadeController.FadeIn();
            });
            _isTransition = true;
        }
    }

    /// <summary>テキストをフェードさせて表示する </summary>
    public void ShowText()
    {
        _storyTexts[_currnetTextIndex].DOFade(1, _showTextTime);
        _currnetTextIndex++;
    }

    /// <summary>
    /// スキップ機能
    /// ボタンから呼び出す
    /// </summary>
    public void Skip()
    {
        if (_isTransition) { return; }

        _fadeController.FadeOut(() =>
        {
            gameObject.SetActive(false);
            _fadeController.FadeIn();
        });

        _isTransition = true;
    }


}
