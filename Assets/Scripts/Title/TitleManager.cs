using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>タイトルシーンの処理を管理するクラス </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("フェードを行うクラス")] 
    FadeController _fadeController = default;
    [SerializeField, Tooltip("遷移先のシーン名")]
    string _nextSceneName = "";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))    //タップ
        {
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //フェードを開始する
        }
    }
}
