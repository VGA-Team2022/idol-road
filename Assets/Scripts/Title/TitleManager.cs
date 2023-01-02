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
    [SerializeField, Tooltip("ストーリーを表示するキャンバス")]
    Canvas _storyCanvas = default;
    [SerializeField, Tooltip("クレジットを表示するキャンバス")]
    Canvas _creditCanvas = default;

    /// <summary>ストーリー用アニメーター </summary>
    Animator _storyAnimator => _storyCanvas.GetComponent<Animator>();
    /// <summary>クレジット用アニメーター </summary>
    Animator _creditAnimator => _creditCanvas.GetComponent<Animator>();

    /// <summary>難易度選択を開始したかどうか</summary>
    bool _isChangeScene = false;

    private void Start()
    {
        _fadeController.FadeIn(() =>
        {
            if (!_isChangeScene)
            {
                AudioManager.Instance.PlayVoice(14);
            }
        });
    }

    public void StoryOpen()
    {
        if (_isChangeScene) { return; }

        _storyAnimator.Play("In");
        _storyCanvas.enabled = true;
        AudioManager.Instance.PlaySE(7);
    }

    public void StoryClose()
    {
        if (_isChangeScene) { return; }

        _storyAnimator.Play("Out");
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditOpen()
    {
        if (_isChangeScene) { return; }

        _creditAnimator.Play("In");
        _creditCanvas.enabled = true;
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditClose()
    {
        if (_isChangeScene) { return; }

        _creditAnimator.Play("Out");
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>シーンを切り替える</summary>
    public void ChangeScene()
    {
        if (_isChangeScene) { return; }

        _isChangeScene = true;
        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //フェードを開始する
    }
}
