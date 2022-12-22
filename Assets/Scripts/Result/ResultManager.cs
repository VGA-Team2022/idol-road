using UnityEngine;

/// <summary>リザルトシーンを管理するクラス </summary>
public class ResultManager : MonoBehaviour
{
    /// <summary>画面に反映させるプレイ結果の数 </summary>
    const int RESULT_COUNT = 4;

    [SerializeField, Header("リザルトUIを更新するクラス")]
    ResultUIController _resultUIController = default;

    /// <summary>リザルト関連のパラメーターのクラス </summary>
    ResultParameter _resultParameter => LevelManager.Instance.CurrentLevel.Result;
    /// <summary>プレイ結果 </summary>
    PlayResult _playResult => PlayResult.Instance;
    /// <summary>合計スコア </summary>
    int _score = 0;
    /// <summary>合計スコア </summary>
    public int Score { get => _score; }

    void Start()
    {
        JudgeResult();
        StartCoroutine(_resultUIController.ShowResult(ScoreCalculation()));
    }

    /// <summary>スコアを計算する</summary>
    /// <returns>計算結果 0=bad 1=good 2=perfect 4=score</returns>
    public int[] ScoreCalculation()
    {
        //判定それぞれのスコア値×判定した数
        var perfectValue = _resultParameter._addParfectScoreValue * _playResult.CountPerfect;
        var goodValue = _resultParameter._addGoodScoreValue * _playResult.CountGood;
        var badValue = _resultParameter._addBadScoreValue * _playResult.CountBad;

        _score += perfectValue + goodValue + badValue;  //スコア計算

        var result = new int[RESULT_COUNT] { badValue, goodValue, perfectValue, _score };  //画面に反映させる為の配列

        return result;
    }

    /// <summary>
    /// スコアの値から結果を判定する
    /// </summary>
    private void JudgeResult()
    {
        if (LevelManager.Instance.CurrentLevel.InGame.PlayerHp <= _playResult.CountMiss)    //失敗
        {
            ResultBad();
        }

        if (_score >= _resultParameter._superPerfectLine)
        {
            ResultSuperPerfect();
        }
        else if (_score >= _resultParameter._perfectLine)
        {
            ResultPerfect();
        }
        else
        {
            ResultGood();
        }
    }

    //Game Over
    public void ResultBad()
    {
        Debug.Log("Bad 悪");
        _resultUIController.ChangeResultImage(Result.Bad);
    }
    //普通
    public void ResultGood()
    {
        Debug.Log("Good 普通");
        _resultUIController.ChangeResultImage(Result.Good);
    }
    //良
    public void ResultPerfect()
    {
        Debug.Log("Perfect 神");
        _resultUIController.ChangeResultImage(Result.Perfect);
    }
    //神　全良
    public void ResultSuperPerfect()
    {
        Debug.Log("SuperPerfect 全能神");
        _resultUIController.ChangeResultImage(Result.SuperPerfect);
    }
}

public enum Result
{
    SuperPerfect,
    Perfect,
    Good,
    Bad
}

