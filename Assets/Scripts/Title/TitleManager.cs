using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>タイトルシーンの処理を管理するクラス </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("フェードを行うクラス")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("遷移先のシーン名")]
    string _nextSceneName = "";
    [SerializeField, Tooltip("ストーリー用アニメーター")]
    Animator _storyAnimator = default;
    [SerializeField, Tooltip("クレジット用アニメーター")]
    Animator _creditAnimator = default;

    /// <summary>難易度選択を開始したかどうか</summary>
    bool _isChangeScene = false;

    /// <summary>ポップアップを表示中かどうか </summary>
    bool _isPopup = false;

    private void Start()
    {
        _fadeController.FadeIn(() =>
        {
            if (!_isChangeScene)
            {
                AudioManager.Instance.PlaySoundAfterExecution(Sources.VOICE, 14, () => AudioManager.Instance.PlayBGM(14, 0.5f));
            }
        });
    }

    public void StoryOpen()
    {
        if (_isChangeScene) { return; }

        _isPopup = true;
        _storyAnimator.Play("Open");
        AudioManager.Instance.PlaySE(7);
    }

    public void StoryClose()
    {
        if (_isChangeScene) { return; }

        _isPopup = false;
        _storyAnimator.Play("Close");
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditOpen()
    {
        if (_isChangeScene) { return; }

        _isPopup = true;
        _creditAnimator.Play("Open");
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditClose()
    {
        if (_isChangeScene) { return; }

        _isPopup = false;
        _creditAnimator.Play("Close");
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>シーンを切り替える</summary>
    public void ChangeScene()
    {
        if (_isPopup || _isChangeScene) { return; }

        _isChangeScene = true;
        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //フェードを開始する
    }

    /// <summaryアプリケーションを落とす </summary>
    public void AppClose()
    {
        Application.Quit();
    }
}
