using UnityEngine;

/// <summary>ゲームスタート状態の処理</summary>
public class GameStart :  IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        Debug.Log("ゲーム開始状態に遷移した"); 
        manager.FadeCanvas.FadeIn(() => manager.ChangeGameState(GameManager._playingState));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        Debug.Log("ゲーム開始状態からぬける");
        manager.Scroller.ScrollOperation();
        AudioManager.Instance.PlayBGM(10, 0.1f);
        AudioManager.Instance.PlaySE(21, 0.1f);
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
