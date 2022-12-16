using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>タイトルシーンの処理を管理するクラス </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("フェードを行うクラス")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("Storyを表示するUI")]
    Canvas _storyCanvas = default;
    [SerializeField, Tooltip("遷移先のシーン名")]
    string _nextSceneName = "";
    /// <summary>入力を受け取るかどうか </summary>
    bool _isInput = false;
    /// <summary>ポップアップが表示されているか　true=表示</summary>
    bool _isPopUp = false;

    private void Start()
    {
        _fadeController.FadeIn(() => _isInput = true);
    }

    /// <summary>
    /// ストーリーやクレジットのポップアップを開く
    /// ボタンで呼び出す
    /// </summary>
    public void OpenPopUp(GameObject popup)
    {
        popup.SetActive(true);
        _isPopUp = true;
    }

    /// <summary>
    /// ストーリーやクレジットのポップアップを閉じる 
    /// ボタンで呼び出す
    /// </summary>
    public void ClosePopUp(GameObject popup)
    {
        popup.SetActive(false);
        _isPopUp = false;
    }

    /// <summary>シーンを切り替える</summary>
    public void ChangeScene()
    {
        if (_isPopUp  || !_isInput) { return; }
        
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //フェードを開始する
    }
}
