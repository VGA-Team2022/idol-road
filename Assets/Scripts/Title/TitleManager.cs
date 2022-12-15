using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>タイトルシーンの処理を管理するクラス </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("フェードを行うクラス")] 
    FadeController _fadeController = default;
    [SerializeField, Tooltip("StoryCanvasをいれる場所")]
    GameObject _canvas = default;
    [SerializeField, Tooltip("遷移先のシーン名")]
    string _nextSceneName = "";
    /// <summary>入力を受け取るかどうか </summary>
    bool _isInput = false;

    bool _isPopUp;

    private void Start()
    {
        _fadeController.FadeIn(() => _isInput = true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isInput && _isPopUp == true)    //タップ
        {
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //フェードを開始する
        }

        if (Input.GetMouseButtonDown(0) && _isPopUp == false)
        {
            _canvas.SetActive(false);
            _isPopUp = true;
        }
    }

    public void OnButtomDown()
    {
        _canvas.SetActive(true);
        _isPopUp = false;
    }
}
