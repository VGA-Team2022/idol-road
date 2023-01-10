using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

/// <summary>リザルトシーンのUIを管理・更新するクラス</summary>
public class ResultUIController : MonoBehaviour
{
    #region
    [SerializeField, Header("ResultManager")]
    ResultManager _resultManager = default;

    [SerializeField, Header("各スコアのテキストを表示させるまで時間")]
    float _showResultSpan = 1.0f;
    [SerializeField, Header("ボタンを表示するまでの時間")]
    float _buttonShowSpan = 1.0f;
    [SerializeField, Header("テキストのフェードインにかかる時間")]
    float _textShowSpan = 2.0f;
    [SerializeField, Header("テキストのアニメーションにかかる時間")]
    float _increseTime = 1.0f;


    [SerializeField, Header("背景(キャラクター)")]
    Image _backGround = default;

    [SerializeField, Header("ボタンのImage")]
    Image _buttonImage = default;

    [SerializeField, Header("ページのText")]
    TextMeshProUGUI _pageText = default;

    [SerializeField, Header("コメント用のアニメーター")]
    Animator _commentAnim = default;

    [SerializeField, Tooltip("左側 リザルトを表示するテキスト")]
    TMP_Text _leftResultText = default;
    [SerializeField, Tooltip("左側 アイドルのセリフ")]
    TMP_Text _idolText = default;

    [ElementNames(new string[] { "神", "良", "普通", "悪" })]
    [SerializeField, Header("評価別背景(キャラクター)"), Tooltip("0=神 1=良 2=普通 3=悪")]
    Sprite[] _backGroundSprites = default;

    [ElementNames(new string[] { "評価", "Bad", "Good", "Perfect","Bonus", "合計スコア" })]
    [SerializeField, Header("結果を表示するテキスト"), Tooltip("0=評価, 1=Bad, 2=Good, 3=Perfect, 4=Bonus,5=合計スコア")]
    TextMeshProUGUI[] _resultValueText = default;

    [SerializeField, Header("リザルト表示切替"), ElementNames(new string[] { "評価", "みんなのコメント" })]
    Transform[] _showResultParent = default;

    [SerializeField, Header("ボタン表示切替"), ElementNames(new string[] { "評価", "みんなのコメント" })]
    Sprite[] _buttonSprites = default;

    [SerializeField, Header("項目名切替"), ElementNames(new string[] { "みんなのコメント", "評価" })]
    string[] _pageName = default;

    [SerializeField, Header("フェードイン関連"), Tooltip("ボタンイメージ"), ElementNames(new string[] { "評価切り替え", "ステージセレクト", "リトライ" })]
    Image[] _fadeImageButton = default;

    [SerializeField, Tooltip("テキスト"), ElementNames(new string[] { "ステージセレクト", "リトライ" })]
    TextMeshProUGUI[] _fadeTextColor = default;

    [SerializeField, Tooltip("ファンのコメントのText"), ElementNames(new string[] { "ファン1", "ファン2", "ファン3", "ファン4", "ファン5" })]
    TextMeshProUGUI[] _fanCommentTexts = default;

    /// <summary>評価画面が表示されているかどうか</summary>
    bool _isValue = false;

    /// <summary>初めてスコアを表示した</summary>
    bool _isFirstScoreWindow = true;

    ResultParameter _parameter => LevelManager.Instance.CurrentLevel.Result;
    #endregion

    /// <summary>コメントを反映させる</summary>
    void ReflectFansComment(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                for (int i = 0; i < _parameter._fanScriptsBad.Length; i++)
                {
                    _fanCommentTexts[i].text = _parameter._fanScriptsBad[i];
                }

                _leftResultText.text = _parameter._idolResultScriptBad;
                _idolText.text = _parameter._idolScriptBad;
                break;
            case Result.Good:
                for (int i = 0; i < _parameter._fanScriptsGood.Length; i++)
                {
                    _fanCommentTexts[i].text = _parameter._fanScriptsGood[i];
                }

                _leftResultText.text = _parameter._idolResultScriptGood;
                _idolText.text = _parameter._idolScriptGood;
                break;
            case Result.Perfect:
                for (int i = 0; i < _parameter._fanScriptsExcellent.Length; i++)
                {
                    _fanCommentTexts[i].text = _parameter._fanScriptsExcellent[i];
                }

                _leftResultText.text = _parameter._idolResultScriptExcellent;
                _idolText.text = _parameter._idolScriptExcellent;
                break;
            case Result.SuperPerfect:
                for (int i = 0; i < _parameter._fanScriptsPerfect.Length; i++)
                {
                    _fanCommentTexts[i].text = _parameter._fanScriptsPerfect[i];
                }

                _leftResultText.text = _parameter._idolResultScriptPerfect;
                _idolText.text = _parameter._idolScriptPerfect;
                break;
        }
    }

    /// <summary>画面左側に表示しているテキストを結果によって変更する </summary>
    /// <param name="result"></param>
    void SetIdolScript(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _leftResultText.text = _parameter._idolResultScriptBad;
                _idolText.text = _parameter._idolScriptBad;
                break;
            case Result.Good:
                _leftResultText.text = _parameter._idolResultScriptGood;
                _idolText.text = _parameter._idolScriptGood;
                break;
            case Result.Perfect:
                _leftResultText.text = _parameter._idolResultScriptExcellent;
                _idolText.text = _parameter._idolScriptExcellent;
                break;
            case Result.SuperPerfect:
                _leftResultText.text = _parameter._idolResultScriptPerfect;
                _idolText.text = _parameter._idolScriptPerfect;
                break;
        }
    }

    /// <summary>結果によって背景を変更する </summary>
    /// <param name="result">プレイ結果</param>
    void ChangeResultImage(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _backGround.sprite = _backGroundSprites[3];
                _resultValueText[0].text = "Bad";
                break;
            case Result.Good:
                _backGround.sprite = _backGroundSprites[2];
                _resultValueText[0].text = "Good";
                break;
            case Result.Perfect:
                _backGround.sprite = _backGroundSprites[1];
                _resultValueText[0].text = "Excellent!!";
                break;
            case Result.SuperPerfect:
                _backGround.sprite = _backGroundSprites[0];
                _resultValueText[0].text = "GOD!!";
                break;
        }
    }


    /// <summary>結果のテキストを表示する関数</summary>
    /// <param name="result">0=bad 1=good 2=perfect 3=ボーナス 4=score</param>
    public IEnumerator ShowResult(int[] result)
    {
        int firstValue = 0;

        yield return new WaitForSeconds(_showResultSpan);
        AudioManager.Instance.PlaySE(37);
        _resultValueText[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(_showResultSpan);

        AudioManager.Instance.PlaySE(37);
        _resultValueText[1].gameObject.SetActive(true);
        _resultValueText[1].text = result[0].ToString();

        yield return new WaitForSeconds(_showResultSpan);

        AudioManager.Instance.PlaySE(37);
        _resultValueText[2].gameObject.SetActive(true);
        _resultValueText[2].text = result[1].ToString();

        yield return new WaitForSeconds(_showResultSpan);

        AudioManager.Instance.PlaySE(37);
        _resultValueText[3].gameObject.SetActive(true);
        _resultValueText[3].text = result[2].ToString();

        yield return new WaitForSeconds(_showResultSpan);
        AudioManager.Instance.PlaySE(37);
        _resultValueText[4].gameObject.SetActive(true);
        _resultValueText[4].text = result[3].ToString();
        yield return new WaitForSeconds(_showResultSpan);

        AudioManager.Instance.PlaySE(36);
        _resultValueText[5].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[4] = r, result[4], 0.7f)
             .OnUpdate(() => _resultValueText[5].text = result[4].ToString());

        yield return new WaitForSeconds(_showResultSpan);

        for (int i = 0; i < _fadeImageButton.Length; i++)
        {
            _fadeImageButton[i].gameObject.SetActive(true);
            _fadeImageButton[i].DOFade(1.0f, _buttonShowSpan);
        }

        Array.ForEach(_fadeTextColor, t => t.DOFade(1.0f, _textShowSpan));

        yield return null;
    }
    /// <summary>リザルトUIを切り替える</summary>
    public void SwitchResultUi()
    {
        //ボタンを押したときにValueが表示されていたら
        if (_isValue)
        {
            //評価画面隠す
            _showResultParent[0].gameObject.SetActive(false);
            //みんなのコメントを表示
            _showResultParent[1].gameObject.SetActive(true);
            //共通UIの切り替え
            SetCommentUI(0);
            _commentAnim.Play("CommentAnimation");
            _isValue = false;

        }
        //されていなかったら
        else if (!_isValue)
        {
            //評価画面を出す
            _showResultParent[0].gameObject.SetActive(true);
            //みんなのコメントを非表示に
            _showResultParent[1].gameObject.SetActive(false);
            //共通UIの切り替え
            SetCommentUI(1);

            if (_isFirstScoreWindow)    //初めてスコアを表示したらアニメーションを再生する
            {
                _buttonImage.color = new Color(1, 1, 1, 0);
                _buttonImage.gameObject.SetActive(false);
                StartCoroutine(ShowResult(_resultManager.Scores));
                _isFirstScoreWindow = false;
            }

            _isValue = true;
        }

        AudioManager.Instance.PlaySE(7);
    }
  
    /// <summary>結果によって表示するUIを変更する </summary>
    public void SetupUI(Result result)
    {
        SetIdolScript(result);
        ChangeResultImage(result);
        ReflectFansComment(result);
    }

    /// <summary> 表示しているUIによってウィンドウ切り替えボタンのイラストを変更する </summary>
    public void SetCommentUI(int num)
    {
        _buttonImage.sprite = _buttonSprites[num];
        _pageText.text = _pageName[num];
    }

    /// <summary>ファンコメントを表示するアニメーションを再生する </summary>
    public void StartCommentAnime()
    {
        _commentAnim.gameObject.SetActive(true);
        _commentAnim.Play("CommentAnimation");
        _fadeImageButton[0].gameObject.SetActive(true);
        _fadeImageButton[0].DOFade(1.0f, _buttonShowSpan);
    }
}
