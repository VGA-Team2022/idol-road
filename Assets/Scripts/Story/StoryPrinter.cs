using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>ストーリーを表示する為のクラス </summary>
[RequireComponent(typeof(Animator))]
public class StoryPrinter : MonoBehaviour
{
    [SerializeField, Header("テキストを表示するまでにかかる秒数")]
    float _showTextTime = 2f;
    [SerializeField, Header("次のテキストを表示するまでの待機時間")]
    float _textWaitTime = 2f;
    [SerializeField, Header("フェードを行うまでの待機時間")]
    float _fadeWaitTime = 2f;
    [SerializeField, Tooltip("表示するストーリーのテキスト配列")]
    TMP_Text[] _storyTexts = default;
    [SerializeField, Tooltip("キャンバスを閉じる為のボタン")]
    Button _closeButton = default;

    /// <summary>表示したテキストの添え字 </summary>
    int _currnetTextIndex = 0;
    /// <summary>ストーリー表示のコルーチンを終了させる為の変数 </summary>
    IEnumerator _showStroyEnumerator = default;

    Animator _anim => GetComponent<Animator>();
    TMP_Text _closeButtonText => _closeButton.transform.GetChild(0).GetComponent<TMP_Text>();

    /// <summary>
    /// アニメーションさせた値をリセットする
    /// 再度アニメーションさせる為
    /// </summary>
    void ResetValue()
    {
        Array.ForEach(_storyTexts, s => s.alpha = 0);
        _closeButton.gameObject.SetActive(false);
        _closeButtonText.alpha = 0;
        _closeButton.image.color = new Color(1, 1, 1, 0);
        _currnetTextIndex = 0;
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

        _closeButton.gameObject.SetActive(true);
        _closeButton.image.DOFade(1, 2);
        _closeButtonText.DOFade(1, 2);
    }

    /// <summary>テキストをフェードさせて表示する </summary>
    public void ShowText()
    {
        _storyTexts[_currnetTextIndex].DOFade(1, _showTextTime);
        _currnetTextIndex++;
    }

    /// <summary>
    /// ストーリーUIを操作する
    /// ボタンから呼び出す
    /// </summary>
    /// <param name="flag">true=表示 false=非表示</param>
    public void StroyOperator(bool flag)
    {
        if (flag)
        {
            _anim.Play("Open");
            _showStroyEnumerator = ShowStory();
            StartCoroutine(_showStroyEnumerator);
        }
        else
        {
            _anim.Play("Close");
            ResetValue();
        }

        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>
    /// すべてのテキストを一気に表示する
    /// ボタンから呼び出す
    /// </summary>
    public void Skip()
    {
        StopCoroutine(_showStroyEnumerator);
        _closeButton.gameObject.SetActive(true);
        Array.ForEach(_storyTexts, s => s.alpha = 1);
        _closeButtonText.alpha = 1;
        _closeButton.image.color = new Color(1, 1, 1, 1);
    }

    /// <summary>閉じるボタンを押した時に実行したい処理を追加する関数 </summary>
    /// <param name="action"></param>
    public void CloseButtonAddListener(UnityAction action)
    {
        _closeButton.onClick.AddListener(action);
        _closeButton.onClick.AddListener(() => _closeButton.onClick.RemoveAllListeners());  //一度だけ実行させる為に、ボタンが押されたら登録されている処理を全て削除する
    }
}
