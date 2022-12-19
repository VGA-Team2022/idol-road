using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>ステージ選択シーンに関する処理を行うクラス </summary>
public class StageSelectController : MonoBehaviour
{
    #region　変数
    [SerializeField, Header("次の遷移先のシーン名")]
    string _nextSceneName = "";
    [SerializeField, Header("ステージセレクトによって変化するイメージ")]
    Image _stageImage = default;
    [SerializeField, Header("フェード行うクラス")]
    FadeController _fadeController = default;
    [ElementNames(new string[] { "チュートリアル", "簡単", "普通", "難しい" })]
    [SerializeField, Header("各ステージのイラスト"), Tooltip("0=チュートリアル, 1=簡単, 2=普通, 3=難しい")]
    Sprite[] _stageSprites = default;
    [ElementNames(new string[] {"簡単", "普通", "難しい" })]
    [SerializeField, Header("ステージセレクトのボタン"), Tooltip("1=簡単, 2=普通, 3=難しい")]
    Button[] _stageSelectButtons = default;
    [SerializeField, Header("遊び方を表示するボタン")]
    Button _playingUIButton = default;
    [SerializeField , Header("遊び方を表示するキャンバス")]
    Canvas _playUiCanvas = default;

    /// <summary>現在選択されているボタン </summary>
    Button _currentSelectedButton = default;
    #endregion

    void Start()
    {
        _fadeController.FadeIn();

        for (var i = 0; i < _stageSelectButtons.Length; i++)
        {
            var button = _stageSelectButtons[i];    //一度ローカル変更に代入しないとエラーが出る
            var index = i;
            _stageSelectButtons[i].onClick.AddListener(() => TransitionGameScene(button, index));
        }

        _currentSelectedButton = _playingUIButton;    //初期ボタンを設定
        _playingUIButton.onClick.AddListener(() => PlayUiButton(_playingUIButton));
        _stageImage.sprite = _stageSprites[0];              //初期イラストを設定
    }

    /// <summary>ゲームシーンに遷移する時の処理 </summary>
    void TransitionGameScene(Button selectButton, int index)
    {
        if (selectButton == _currentSelectedButton)     //選択ゲームシーンに遷移する
        {
            AudioManager.Instance.PlaySE(7);
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));
        }
        else
        {
            if (_playUiCanvas.enabled)
            {
                _playUiCanvas.enabled = false;
            }

            //ステージを選択する
            _currentSelectedButton = selectButton;
            _stageImage.sprite = _stageSprites[index];
            LevelManager.Instance.SelectLevel((GameLevel)index);    //レベルを変更
            AudioManager.Instance.PlaySE(32);
        }
    }

    void PlayUiButton(Button selectButton)
    {
        if (selectButton == _currentSelectedButton)     //選択ゲームシーンに遷移する
        {
            AudioManager.Instance.PlaySE(7);
            if (!_playUiCanvas.enabled) 
            {
                _playUiCanvas.enabled = true;
            }
        }
        else
        {
            //ステージを選択する
            _currentSelectedButton = selectButton;
            _stageImage.sprite = _stageSprites[0];
            AudioManager.Instance.PlaySE(32);
        }
    }
}
