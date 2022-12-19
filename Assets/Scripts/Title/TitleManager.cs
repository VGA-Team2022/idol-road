using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>タイトルシーンの処理を管理するクラス </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("フェードを行うクラス")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("遷移先のシーン名")]
    string _nextSceneName = "";

    /// <summary>ポップアップが表示されているか　true=表示</summary>
    bool _isPopUp = false;
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

    /// <summary>
    /// ストーリーやクレジットのポップアップを開く
    /// ボタンで呼び出す
    /// </summary>
    public void OpenPopUp(GameObject popup)
    {
        popup.SetActive(true);
        _isPopUp = true;
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>
    /// ストーリーやクレジットのポップアップを閉じる 
    /// ボタンで呼び出す
    /// </summary>
    public void ClosePopUp(GameObject popup)
    {
        popup.SetActive(false);
        _isPopUp = false;
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>シーンを切り替える</summary>
    public void ChangeScene()
    {
        if (_isPopUp) { return; }

        _isChangeScene = true;
        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //フェードを開始する
    }
}
