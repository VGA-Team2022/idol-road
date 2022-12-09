using UnityEngine;

/// <summary>�Q�[���X�^�[�g��Ԃ̏���</summary>
public class GameStart :  IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        Debug.Log("�Q�[���J�n��ԂɑJ�ڂ���"); 
        manager.FadeCanvas.FadeOut(() => manager.ChangeGameState(GameManager._playingState));
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        Debug.Log("�Q�[���J�n��Ԃ���ʂ���");
        manager.Scroller.ScrollOperation();
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
