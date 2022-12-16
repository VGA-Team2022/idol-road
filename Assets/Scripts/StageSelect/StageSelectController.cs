using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>ステージ選択シーンに関する処理を行うクラス </summary>
public class StageSelectController : MonoBehaviour
{
    [SerializeField, Header("次の遷移先のシーン名")]
    string _nextSceneName = "";
    [SerializeField, Header("ステージセレクトによって変化するイメージ")]
    Image _stageImage = default;
    [SerializeField, Header("フェード行うクラス")]
    FadeController _fadeController = default;
    [ElementNames(new string[] { "チュートリアル", "簡単", "普通", "難しい" })]
    [SerializeField, Header("各ステージのイラスト"), Tooltip("0=チュートリアル, 1=簡単, 2=普通, 3=難しい")]
    Sprite[] _stageSprites = default;
    [ElementNames(new string[] { "チュートリアル", "簡単", "普通", "難しい" })]
    [SerializeField, Header("ステージセレクトのボタン"), Tooltip("0=チュートリアル, 1=簡単, 2=普通, 3=難しい")]
    Button[] _stageSelectButtons = default;

    /// <summary>現在選択されているボタン </summary>
    Button _currentSelectedButton = default;
    /// <summary>入力を受け取るかどうか</summary>
    bool _isInput = false;

    void Start()
    {
        _fadeController.FadeIn(() => _isInput = true);

        for (var i = 0; i < _stageSelectButtons.Length; i++)
        {
            var button = _stageSelectButtons[i];    //一度ローカル変更に代入しないとエラーが出る
            var index = i;
            _stageSelectButtons[i].onClick.AddListener(() => TransitionGameScene(button, index));
        }

        _currentSelectedButton = _stageSelectButtons[0];    //初期ボタンを設定
        _stageImage.sprite = _stageSprites[0];              //初期イラストを設定
    }

    /// <summary>ゲームシーンに遷移する時の処理 </summary>
    void TransitionGameScene(Button selectButton, int index)
    {
        if(!_isInput) { return; }

        if (selectButton == _currentSelectedButton)     //選択ゲームシーンに遷移する
        {
            AudioManager.Instance.PlaySE(7);
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));
        }
        else
        {
            //ステージを選択する
            _currentSelectedButton = selectButton;
            _stageImage.sprite = _stageSprites[index];
            AudioManager.Instance.PlaySE(32);
        }
    }
}
