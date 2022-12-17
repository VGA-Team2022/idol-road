using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>ゲームエンド状態の処理</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        Debug.Log("ゲーム終了");
        manager.FadeCanvas.FadeOut(() => SceneManager.LoadScene("ResultScene"));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
