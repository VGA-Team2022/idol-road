using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>ボスステージ状態の処理</summary>
public class BossTime : IState
{
    private bool _isPlaying = false;
    public bool IsPlaying
    {
        get { return _isPlaying; }
        set { _isPlaying = value; }
    }
    public void OnEnter(GameManager manager, IState previousState)
    {
        manager.Scroller.ScrollOperation(false);
        manager.WarningTape.gameObject.SetActive(true);
        manager.WarningTape.stopped += WarningTapeAnimEnd;
        manager.EnemyGenerator.SpawnBossEnemy();
        manager.Taxi.gameObject.SetActive(true);
        _isPlaying= true;
        Debug.Log("Enter");
        AudioManager.Instance.PlaySE(26);
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }

    /// <summary>ボスステージ開始演出終了時処理 </summary>
    void WarningTapeAnimEnd(PlayableDirector director)
    {
        director.gameObject.SetActive(false);
        _isPlaying= false;
        Debug.Log("Invoked");
    }
}
