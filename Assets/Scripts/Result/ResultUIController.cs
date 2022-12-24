using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    [SerializeField, Header("スコアを表示するまでの時間")]
    float _scoreResultSpan = 2.0f;
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


    [ElementNames(new string[] { "神", "良", "普通", "悪" })]
    [SerializeField, Header("評価別背景(キャラクター)"), Tooltip("0=神 1=良 2=普通 3=悪")]
    Sprite[] _backGroundSprites = default;

    [ElementNames(new string[] { "評価", "Bad", "Good", "Perfect", "合計スコア" })]
    [SerializeField, Header("結果を表示するテキスト"), Tooltip("0=評価, 1=Bad, 2=Good, 3=Perfect, 4=合計スコア")]
    TextMeshProUGUI[] _resultValueText = default;

    [SerializeField, Header("リザルト表示切替"), ElementNames(new string[] { "評価", "みんなのコメント" })]
    Transform[] _showResultParent = default;

    [SerializeField, Header("ボタン表示切替"), ElementNames(new string[] { "評価", "みんなのコメント" })]
    Sprite[] _buttonSprites = default;

    [SerializeField, Header("項目名切替"), ElementNames(new string[] { "評価", "みんなのコメント" })]
    string[] _pageName = default;


    [SerializeField, Header("フェードイン関連"), Tooltip("ボタンイメージ"), ElementNames(new string[] { "評価切り替え", "ステージセレクト", "リトライ" })]
    Image[] _fadeImageButton = default;

    [SerializeField, Tooltip("テキスト"), ElementNames(new string[] { "評価切り替え", "ステージセレクト", "リトライ" })]
    TextMeshProUGUI[] _fadeTextColor = default;

    [SerializeField, Tooltip("ファンのコメントのText"), ElementNames(new string[] { "ファン1", "ファン2", "ファン3", "ファン4", "ファン5" })]
    TextMeshProUGUI[] _fanCommentTexts = default;

    /// <summary>評価画面が表示されているかどうか</summary>
    bool _isValue = true;

    /// <summary>遷移をしているか true=開始している</summary>
    bool _isTransition = false;
    #endregion
    public void Start()
    {
        ReflectFansComment();
        SetCommonUI(0);
    }

    /// <summary>結果によって背景を変更する </summary>
    /// <param name="result">プレイ結果</param>
    public void ChangeResultImage(Result result)
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
    /// <param name="result">0=bad 1=good 2=perfect 4=score</param>
    public IEnumerator ShowResult(int[] result)
    {
        int firstValue = 0;

        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[1].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[0] = r, result[0], _increseTime)
            .OnUpdate(() => _resultValueText[1].text = result[0].ToString());
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[2].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[1] = r, result[1], _increseTime)
            .OnUpdate(() => _resultValueText[2].text = result[1].ToString());
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[3].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[2] = r, result[2], _increseTime)
            .OnUpdate(() => _resultValueText[3].text = result[2].ToString());
        yield return new WaitForSeconds(_scoreResultSpan);

        _resultValueText[4].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[3] = r, result[3], _increseTime)
             .OnUpdate(() => _resultValueText[4].text = result[3].ToString());
        yield return new WaitForSeconds(_scoreResultSpan);

        for (int i = 0; i < _fadeImageButton.Length; i++)
        {
            _fadeImageButton[i].gameObject.SetActive(true);
            _fadeImageButton[i].DOFade(1.0f, _buttonShowSpan);
            _fadeTextColor[i].DOFade(1.0f, _textShowSpan);
        }

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
            SetCommonUI(1);
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
            SetCommonUI(0);

            _isValue = true;
        }

        AudioManager.Instance.PlaySE(7);
    }
    /// <summary>難易度セレクトシーンに戻るかリトライするか</summary>
    /// <param name="index">シーン番号</param>
    public void ReturnModeSelectAndRetry(int index)
    {
        if (_isTransition) { return; }

        _isTransition = true;
        _resultManager.FadeController.FadeOut(() => { SceneManager.LoadScene(index); });

        if (_resultManager.CurrentResult == Result.Bad)
        {
            AudioManager.Instance.PlayVoice(16);
        }

        AudioManager.Instance.PlaySE(7);
    }
    /// <summary>コメントを反映させる</summary>
    public void ReflectFansComment()
    {
        for (int i = 0; i < LevelManager.Instance.CurrentLevel.Result._fanScripts.Length; i++)
        {
            _fanCommentTexts[i].text = LevelManager.Instance.CurrentLevel.Result._fanScripts[i];
        }
    }
    public void SetCommonUI(int num)
    {
        _buttonImage.sprite = _buttonSprites[num];
        _pageText.text = _pageName[num];
    }
}
