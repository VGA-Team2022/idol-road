using UnityEngine;

/// <summary>各レベルの情報を保持するクラス</summary>
public class LevelManager
{
    private static LevelManager _instance = new LevelManager();
    public static LevelManager Instance => _instance;

    /// <summary>現在のレベル </summary>
    GameParameter _currentLevel = Resources.Load<GameParameter>("NomalLevel");
    
    GameParameter _easy => Resources.Load<GameParameter>("EasyLevel");

    GameParameter _nomal => Resources.Load<GameParameter>("NomalLevel");

    GameParameter _Hard => Resources.Load<GameParameter>("HardLevel");

    /// <summary>現在のレベル </summary>
    public GameParameter CurrentLevel { get => _currentLevel; }

    /// <summary>レベルを変更する </summary>
    /// <param name="nextLevel">変更後のレベル</param>
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

/// <summary>難易度</summary>
public enum GameLevel
{
    Easy = 0,
    Nomal = 1,
    Hard = 2,
}
