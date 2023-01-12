using UnityEngine;

/// <summary>アイドルタイム状態の処理</summary>
public class IdolTime : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        manager.IdolTime.StartSuperIdolTime();
        manager.ChangeTimeElapsing(false);
        AudioManager.Instance.AISACChangeRun(true);
        AudioManager.Instance.PlayBGM(11);
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        manager.ChangeTimeElapsing(true);
        manager.UIController.UpdateIdolPowerGauge(0);
        manager.UIController.DisactiveIdolPowerGauge();
        AudioManager.Instance.AISACChangeRun(true);
        AudioManager.Instance.StopBGM(11);
    }

    public void OnUpdate(GameManager manager)
    {
       
    }
}
