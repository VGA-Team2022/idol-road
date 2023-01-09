using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>ゲームエンド状態の処理</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        //TODO:足音を止める
        AudioManager.Instance.StopBGM(10);
        AudioManager.Instance.PlaySE(33);
        manager.FadeCanvas.FadeOut(3f, () =>
        {
            AudioManager.Instance.PlaySoundAfterExecution(Sources.SE, 31, () => SceneManager.LoadScene("ResultScene"));
        });
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
