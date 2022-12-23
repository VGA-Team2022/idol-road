using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>ƒQ[ƒ€ƒGƒ“ƒhó‘Ô‚Ìˆ—</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        //TODO:‘«‰¹‚ğ~‚ß‚é
        AudioManager.Instance.StopBGM(10);
        manager.FadeCanvas.FadeOut(3f ,() => SceneManager.LoadScene("ResultScene"));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
