using UnityEngine;

/// <summary>�Q�[���X�^�[�g��Ԃ̏���</summary>
public class GameStart : IState
{
    public void OnEnter(GameManager manager, IState previousState)
    {
        Debug.Log("�Q�[���J�n��ԂɑJ�ڂ���");
    }

    public void OnExit(GameManager manager, IState nextState)
    {
        Debug.Log("�Q�[���J�n��Ԃ���ʂ���");
    }

    public void OnUpdate(GameManager manager)
    {
        
    }
}
