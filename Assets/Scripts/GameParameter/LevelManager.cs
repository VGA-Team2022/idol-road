using UnityEngine;

/// <summary>�e���x���̏���ێ�����N���X</summary>
public class LevelManager
{
    private static LevelManager _instance = new LevelManager();
    public static LevelManager Instance => _instance;

    /// <summary>���݂̃��x�� </summary>
    GameParameter _currentLevel = Resources.Load<GameParameter>("NomalLevel");
    
    GameParameter _easy => Resources.Load<GameParameter>("EasyLevel");

    GameParameter _nomal => Resources.Load<GameParameter>("NomalLevel");

    GameParameter _Hard => Resources.Load<GameParameter>("HardLevel");

    /// <summary>���݂̃��x�� </summary>
    public GameParameter CurrentLevel { get => _currentLevel; }

    /// <summary>���x����ύX���� </summary>
    /// <param name="nextLevel">�ύX��̃��x��</param>
    public void SelectLevel(GameLevel nextLevel)
    {
        switch (nextLevel)
        {
            case GameLevel.Easy:
                _currentLevel = _easy;
                break;
            case GameLevel.Nomal:
                _currentLevel = _nomal;
                break;
            case GameLevel.Hard:
                _currentLevel = _Hard;
                break;
        }
    }
}

/// <summary>��Փx</summary>
public enum GameLevel
{
    Easy = 0,
    Nomal = 1,
    Hard = 2,
}
