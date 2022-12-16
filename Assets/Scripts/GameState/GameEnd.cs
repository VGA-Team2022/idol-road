using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�Q�[���G���h��Ԃ̏���</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        Debug.Log("�Q�[���I��");
        manager.FadeCanvas.FadeOut(() => SceneManager.LoadScene("ResultScene"));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
