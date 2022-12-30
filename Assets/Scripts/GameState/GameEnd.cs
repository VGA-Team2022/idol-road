using UnityEngine.SceneManagement;

/// <summary>�Q�[���G���h��Ԃ̏���</summary>
public class GameEnd : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        //TODO:�������~�߂�
        AudioManager.Instance.StopBGM(10);
        AudioManager.Instance.PlaySE(33);
        manager.FadeCanvas.FadeOut(3f, () =>
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
