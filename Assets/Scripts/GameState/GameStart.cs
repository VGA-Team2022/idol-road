using UnityEngine;

/// <summary>ゲームスタート状態の処理</summary>
public class GameStart :  IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        manager.FadeCanvas.FadeIn(() => manager.ChangeGameState(GameManager._playingState));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        manager.Scroller.ScrollOperation();
        AudioManager.Instance.PlayBGM(10, 0.5f);
        AudioManager.Instance.PlaySE(21, 0.1f);
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
