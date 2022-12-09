using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonFuncs : MonoBehaviour
{
    [SerializeField] string _nextScene;

    FadeManager2 _fadeManager2;

    public string NextScene { get; set; }

    void Start()
    {
        _fadeManager2 = GetComponent<FadeManager2>();
    }
    public void StartButton(string nextSceneName)
    {
        SceneManager.LoadScene(nextSceneName);
    }

    /// <summary>
    /// チュートリアルボタンを押した時に
    /// 処理するメソッド
    /// </summary>
    public void TutorialButton()
    {
        _fadeManager2.Mode = FadeMode.OutMode;
        _fadeManager2.ChangeScene(_nextScene);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
}
