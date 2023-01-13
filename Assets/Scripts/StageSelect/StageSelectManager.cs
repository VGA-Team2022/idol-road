using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>ステージ選択シーンに関する処理を行うクラス </summary>
public class StageSelectManager : MonoBehaviour
{
    #region　変数
    [SerializeField, Header("次の遷移先のシーン名")]
    string _nextSceneName = "";
    [SerializeField, Header("ステージセレクトによって変化するイメージ")]
    Image _stageImage = default;
    [SerializeField, Header("フェード行うクラス")]
    FadeController _fadeController = default;
    [ElementNames(new string[] { "簡単", "普通", "難しい" })]
    [SerializeField, Header("各ステージのイラスト"), Tooltip("0=簡単, 1=普通, 2=難しい")]
    Sprite[] _stageSprites = default;
    [ElementNames(new string[] { "簡単", "普通", "難しい" })]
    [SerializeField, Header("遊び方を表示するキャンバス")]
    Canvas _playUiCanvas = default;
    [SerializeField, Header("スタートボタンのアニメーター")]
    Animator _startAnimator = default;

    /// <summary>遷移をしているか true=開始している</summary>
    bool _isTransition = false;

    /// <summary>現在選択されているボタン </summary>
    Button _currentSelectedButton = default;

    /// <summary>選択されているステージ選択ボタンのアニメーター </summary>
    Animator _currentButtonAnimator = default;
    #endregion

    void Start()
    {
        if (!AudioManager.Instance.CheckPlayingBGM(14))     //タイトルBGMが再生されていなければ再生する
        {
            AudioManager.Instance.PlayBGM(14);
        }

        _fadeController.FadeIn();
        _stageImage.sprite = _stageSprites[0];              //初期イラストを設定
    }

    /// <summary>操作説明・遊び方を表示する処理</summary>
    void PlayUiButton(Button selectButton)
    {
        if (_isTransition) { return; }

        if (selectButton == _currentSelectedButton)
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

    /// <summary>
    /// 難易度を選択する
    /// ボタンから呼び出す
    /// </summary>
    /// <param name="index"></param>
    public void LevelSelect(int index)
    {
        if (_isTransition) { return; }

        LevelManager.Instance.SelectLevel((GameLevel)index);    //レベルを変更
        AudioManager.Instance.PlaySE(32);

        _stageImage.sprite = _stageSprites[index];
    }

    /// <summary>
    /// タイトルシーンに戻る
    /// ボタンから呼び出す
    /// </summary>
    public void TitleChange()
    {
        if (_isTransition) { return; }

        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(0));
        _isTransition = true;
    }

    /// <summary>
    /// ゲームシーンに遷移する時の処理 
    /// ボタンから呼び出す
    /// </summary>
    public void TransitionGameScene()
    {
        if (_isTransition) { return; }

        AudioManager.Instance.PlaySE(7);
        _isTransition = true;
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));
        AudioManager.Instance.StopBGM(14);
    }

    /// <summary>
    /// レベル選択ボタンが押された時のアニメーションを再生する
    /// ボタンから呼び出す
    /// </summary>
    public void StartSelectLevelAnime(Animator selectAnimator)
    {
        if (_currentButtonAnimator == null)     //初めて難易度が選択された時の処理
        {
            _startAnimator.gameObject.SetActive(true);
            _startAnimator.Play("Open");

            _currentButtonAnimator = selectAnimator;
            _currentButtonAnimator.Play("Select");
            return;
        }

        if (selectAnimator == _currentButtonAnimator) { return; }   //同じ難易度のボタンが押されたら何もしない

        _currentButtonAnimator.Play("Deselect");
        _currentButtonAnimator = selectAnimator;
        _currentButtonAnimator.Play("Select");
    }

}
