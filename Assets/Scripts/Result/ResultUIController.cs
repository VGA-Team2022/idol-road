using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

/// <summary>リザルトシーンのUIを管理・更新するクラス</summary>
public class ResultUIController : MonoBehaviour
{
    [SerializeField, Header("背景(キャラクター)")]
    Image _backGround = default;

    [ElementNames(new string[] { "神", "良", "普通", "悪" })]
    [SerializeField, Header("評価別背景(キャラクター)"), Tooltip("0=神 1=良 2=普通 3=悪")]
    Sprite[] _backGroundSprites = default;

    [ElementNames(new string[] { "評価", "Bad", "Good", "Perfect", "合計スコア" })]
    [SerializeField, Header("結果を表示するテキスト"), Tooltip("0=評価, 1=Bad, 2=Good, 3=Perfect, 4=合計スコア")]
    TextMeshProUGUI[] _resultValueText = default;

    [SerializeField, Header("リザルト表示切替"), ElementNames(new string[] { "評価", "みんなのコメント" })]
    Transform[] _showResultParent = default;

    [SerializeField, Header("フェードイン関連"), Tooltip("ボタンイメージ"), ElementNames(new string[] { "評価切り替え", "ステージセレクト", "リトライ" })]
    Image[] _fadeImageButton = default;

    [SerializeField, Tooltip("テキスト"), ElementNames(new string[] { "評価切り替え", "ステージセレクト", "リトライ" })]
    TextMeshProUGUI[] _fadeTextColor = default;

    [SerializeField, Tooltip("値の増加時間")]
    float _increseTime = 1.0f;

    [SerializeField, Tooltip("UIを表示させるまでの時間"), ElementNames(new string[] { "評価表示", "スコア表示", "ボタンのフェードイン", "テキストのフェードイン" })]
    float _showResultSpan = 1.0f, _scoreResultSpan = 2.0f, _buttonShowSpan = 1.0f, _textShowSpan = 2.0f;

    /// <summary>評価画面が表示されているかどうか</summary>
    bool _isValue = true;

    /// <summary>結果によって背景を変更する </summary>
    /// <param name="result">プレイ結果</param>
    public void ChangeResultImage(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _backGround.sprite = _backGroundSprites[3];
                _resultValueText[0].text = "Result:Bad";
                break;
            case Result.Good:
                _backGround.sprite = _backGroundSprites[2];
                _resultValueText[0].text = "Result:Good";
                break;
            case Result.Perfect:
                _backGround.sprite = _backGroundSprites[1];
                _resultValueText[0].text = "Result:Perfect";
                break;
            case Result.SuperPerfect:
                _backGround.sprite = _backGroundSprites[0];
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
            .OnUpdate(() => _resultValueText[1].text = $"Bad:{result[0]}");
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[2].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[1] = r, result[1], _increseTime)
            .OnUpdate(() => _resultValueText[2].text = $"Good:{result[1]}");
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[3].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[2] = r, result[2], _increseTime)
            .OnUpdate(() => _resultValueText[3].text = $"Perfect:{result[2]}");
        yield return new WaitForSeconds(_scoreResultSpan);

        _resultValueText[4].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[3] = r, result[3], _increseTime)
             .OnUpdate(() => _resultValueText[4].text = $"Score:{result[3]}");
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
            _isValue = false;
        }
        //されていなかったら
        else if (!_isValue)
        {
            //評価画面を出す
            _showResultParent[0].gameObject.SetActive(true);
            //みんなのコメントを非表示に
            _showResultParent[1].gameObject.SetActive(false);
            _isValue = true;
        }
    }
    /// <summary>難易度セレクトシーンに戻るかリトライするか</summary>
    /// <param name="index">シーン番号</param>
    public void ReturnModeSelectAndRetry(int index)
    {
        SceneManager.LoadScene(index);
    }
}
