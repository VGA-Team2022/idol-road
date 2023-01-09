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
    /// <summary>強制表示時にスキップする為の処理 </summary>
    event Action _storySkip = default;
    /// <summary>ストーリー表示のコルーチンを終了させる為の変数 </summary>
    IEnumerator _showStroyEnumerator = default;

    Animator _anim => GetComponent<Animator>();
   
    /// <summary>強制表示時にスキップする為の処理 </summary>
    public event Action StorySkip
    {
        add { _storySkip += value; }
        remove { _storySkip -= value; }
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


            if (_currentMode == ShowMode.InGame)    //タイトル表示モードに切り替える
            {
                _currentMode = ShowMode.Title;
                _storySkip?.Invoke();
            }
        }

        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>各モードの初期化処理を行う </summary>
    public void Initialize()
    {
        if (_currentMode == ShowMode.Title)
        {
            _closeButton.gameObject.SetActive(true);
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
        if (!_endAnimation)
        {
            StopCoroutine(_showStroyEnumerator);
            _storyTexts[_currnetTextIndex].DOComplete();
            Array.ForEach(_storyTexts, s => s.alpha = 1);
            _endAnimation = true;
        }
        else if(_currentMode == ShowMode.InGame)    //タイトル表示モードに切り替える
        {
            _currentMode = ShowMode.Title;
            _storySkip?.Invoke();
            StroyOperator(false);
        }
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


