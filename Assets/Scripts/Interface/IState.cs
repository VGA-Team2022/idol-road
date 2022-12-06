
public interface IState
{
    /// <summary>このステートに遷移時の処理 </summary>
    /// <param name="previousState">遷移前の状態</param>
    /// <param name="manager">インゲームを管理するクラス</param>
    public void OnEnter(GameManager manager, IState previousState);

    /// <summary>ステート中の処理</summary>
    /// <param name="manager">インゲームを管理するクラス</param>
    public void OnUpdate(GameManager manager);

    /// <summary>別のステートに遷移する時の処理 </summary>
    /// <param name="nextState">遷移後の状態</param>
    /// <param name="manager">インゲームを管理するクラス</param>
    public void OnExit(GameManager manager, IState nextState);
}

