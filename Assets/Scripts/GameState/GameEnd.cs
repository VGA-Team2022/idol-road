using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>ゲームエンド状態の処理</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        //TODO:足音を止める
        AudioManager.Instance.StopBGM(10);
        manager.FadeCanvas.FadeOut(3f ,() => SceneManager.LoadScene("ResultScene"));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
