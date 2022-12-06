
public interface IState
{
    /// <summary>���̃X�e�[�g�ɑJ�ڎ��̏��� </summary>
    /// <param name="previousState">�J�ڑO�̏��</param>
    /// <param name="manager">�C���Q�[�����Ǘ�����N���X</param>
    public void OnEnter(GameManager manager, IState previousState);

    /// <summary>�X�e�[�g���̏���</summary>
    /// <param name="manager">�C���Q�[�����Ǘ�����N���X</param>
    public void OnUpdate(GameManager manager);

    /// <summary>�ʂ̃X�e�[�g�ɑJ�ڂ��鎞�̏��� </summary>
    /// <param name="nextState">�J�ڌ�̏��</param>
    /// <param name="manager">�C���Q�[�����Ǘ�����N���X</param>
    public void OnExit(GameManager manager, IState nextState);
}

