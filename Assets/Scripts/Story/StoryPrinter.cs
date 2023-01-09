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
    [SerializeField, Header("表示モード")]
    ShowMode _currentMode = ShowMode.Title;
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
    [SerializeField, Tooltip("スキップするボタン")]
    Button _skipButton = default;
    [SerializeField, Tooltip("強制表示時に使用するNextテキスト")]
    TMP_Text _nextText = default;

    /// <summary>表示したテキストの添え字 </summary>
    int _currnetTextIndex = 0;
    /// <summary>演出が終了したかどうか </summary>
    bool _endAnimation = false;
    /// <summary>ストーリー表示のコルーチンを終了させる為の変数 </summary>
    IEnumerator _showStroyEnumerator = default;

    Animator _anim => GetComponent<Animator>();

    /// <summary>
    /// アニメーションさせた値をリセットする
    /// 再度アニメーションさせる為
    /// </summary>
    void ResetValue()
    {
        Array.ForEach(_storyTexts, s => s.alpha = 0);
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

        _endAnimation = true;
    }

    /// <summary>テキストをフェードさせて表示する </summary>
    public void ShowText()
    {
        _storyTexts[_currnetTextIndex].DOFade(1, _showTextTime)
            .OnComplete(() => _currnetTextIndex++);
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
            Initialize();
            _anim.Play("Open");
            _showStroyEnumerator = ShowStory();
            StartCoroutine(_showStroyEnumerator);
        }
        else
        {
            _anim.Play("Close");
            StopCoroutine(_showStroyEnumerator);

            if (!_endAnimation)
            {
                _storyTexts[_currnetTextIndex].DOComplete();
            }

            Array.ForEach(_storyTexts, s => s.alpha = 0);
            _currnetTextIndex = 0;
            _endAnimation = false;
        }

        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>閉じるボタンを押した時に実行したい処理を追加する関数 </summary>
    /// <param name="action"></param>
    public void CloseButtonAddListener(UnityAction action)
    {
        _closeButton.onClick.AddListener(action);
        _closeButton.onClick.AddListener(() => _closeButton.onClick.RemoveAllListeners());  //一度だけ実行させる為に、ボタンが押されたら登録されている処理を全て削除する
    }

    /// <summary>各モードの初期化処理を行う </summary>
    public void Initialize()
    {
        if (_currentMode == ShowMode.Title)
        {
            _skipButton.gameObject.SetActive(false);
            _nextText.gameObject.SetActive(false);
        }
        else
        {
            _closeButton.gameObject.SetActive(false);
            _nextText.color = new Color(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// ストーリーの演出を無くし、すぐにテキストを表示させる
    /// ボタンから呼び出す
    /// </summary>
    public void EndAnimation()
    {
        if (_endAnimation) { return; }  //アニメーションが終了していたら何もしない

        StopCoroutine(_showStroyEnumerator);
        _storyTexts[_currnetTextIndex].DOComplete();
        Array.ForEach(_storyTexts, s => s.alpha = 1);
    }

    /// <summary>表示させる場所によって表示の仕方を変える </summary>
    enum ShowMode
    {
        /// <summary>タイトルで表示 </summary>
        Title,
        /// <summary>ゲーム内(強制表示) </summary>
        InGame,
    }
}
