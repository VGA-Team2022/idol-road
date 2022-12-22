using UnityEngine;
using UnityEngine.Playables;

/// <summary>�{�X�X�e�[�W��Ԃ̏���</summary>
public class BossTime : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        manager.Scroller.ScrollOperation(false);
        manager.WarningTape.gameObject.SetActive(true);
        manager.WarningTape.stopped += WarningTapeAnimEnd;
        manager.EnemyGenerator.SpawnBossEnemy();
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }

    /// <summary>�{�X�X�e�[�W�J�n���o�I�������� </summary>
    void WarningTapeAnimEnd(PlayableDirector director)
    {
        director.gameObject.SetActive(false);
    }
}
