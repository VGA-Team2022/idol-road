using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>ゲームエンド状態の処理</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        //TODO:足音を止める
        ScreenInput.IsInput = false;
        manager.RunStopBGM();
        AudioManager.Instance.PlaySE(33);

        if (manager.IsClear)
        {
            AudioManager.Instance.PlaySE(38);
        }

        manager.FadeCanvas.FadeOut(2f, () =>
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
