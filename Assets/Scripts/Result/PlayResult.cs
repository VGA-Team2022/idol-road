
/// <summary>プレイ結果を保持するクラス </summary>
public class PlayResult
{
    private static PlayResult _instance = new PlayResult();
    public static PlayResult Instance => _instance;

    //アクションを成功度別に格納
    int _countBad;
    int _countGood;
    int _countPerfect;
    int _countMiss;

    /// <summary>スーパーアイドルタイム中のタップ回数</summary>
    int _valueSuperIdleTimePerfect;
    /// <summary>最高コンボ数 </summary>
    int _highComboCount = 0;


    /// <summary>badを取得した回数 </summary>
    public int CountBad
    {
        get => _countBad;
        set => _countBad = value;
    }

    /// <summary>Goodを取得した回数 </summary>
    public int CountGood
    {
        get => _countGood;
        set => _countGood = value;
    }

    /// <summary>Perfectを取得した回数 </summary>
    public int CountPerfect
    {
        get => _countPerfect;
        set => _countPerfect = value;
    }

    /// <summary>失敗(ダメージを受けた)回数</summary>
    public int CountMiss
    {
        get => _countMiss;
        set => _countMiss = value;
    }

    /// <summary>スーパーアイドルタイム時の追加スコア </summary>
    public int ValueSuperIdleTimePerfect
    {
        get => _valueSuperIdleTimePerfect;
        set => _valueSuperIdleTimePerfect = value;
    }

    /// <summary>プレイデータを削除する</summary>
    public void ResetResult()
    {
        _countBad = 0;
        _countGood = 0;
        _countPerfect = 0;
        _valueSuperIdleTimePerfect = 0;
        _highComboCount = 0;
        _countMiss = 0;
    }
}
