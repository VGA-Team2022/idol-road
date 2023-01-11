using UnityEngine;

/// <summary>�A�C�h���^�C����Ԃ̏���</summary>
public class IdolTime : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        manager.IdolTime.StartSuperIdolTime();
        manager.ChangeTimeElapsing(false);
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        manager.ChangeTimeElapsing(true);
        manager.UIController.UpdateIdolPowerGauge(0);
        manager.UIController.DisactiveIdolPowerGauge();
    }

    public void OnUpdate(GameManager manager)
    {
       
    }
}
