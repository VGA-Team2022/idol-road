using UnityEngine;

/// <summary>ゲームスタート状態の処理</summary>
public class GameStart :  IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        Debug.Log("ゲーム開始状態に遷移した"); 
        manager.FadeCanvas.FadeOut(() => manager.ChangeGameState(GameManager._playingState));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        Debug.Log("ゲーム開始状態からぬける");
        manager.Scroller.ScrollOperation();
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
