using UnityEngine;

/// <summary>リザルトシーンを管理するクラス </summary>
public class ResultManager : MonoBehaviour
{
    /// <summary>画面に反映させるプレイ結果の数 </summary>
    const int RESULT_COUNT = 4;
    /// <summary>合計スコアが格納されている添え字 </summary>
    const int TOTAL_SCORE_INDEX = 3;

    [SerializeField, Header("リザルトUIを更新するクラス")]
    ResultUIController _resultUIController = default;
    [SerializeField, Header("フェードを行うクラス")]
    FadeController _fadeController = default;


    /// <summary>リザルト関連のパラメーターのクラス </summary>
    ResultParameter _resultParameter => LevelManager.Instance.CurrentLevel.Result;
    /// <summary>プレイ結果 </summary>
    PlayResult _playResult => PlayResult.Instance;
    /// <summary>各スコア </summary>
    int[] _scores = default;
   
    public FadeController FadeController { get => _fadeController; }

    void Start()
    {
        _scores = ScoreCalculation();
        JudgeResult();
        _fadeController.FadeIn(StartShowResultAnim);
    }

    /// <summary>スコアを計算する</summary>
    /// <returns>計算結果 0=bad 1=good 2=perfect 3=合計スコア</returns>
    public int[] ScoreCalculation()
    {
        //判定それぞれのスコア値×判定した数
        var perfectValue = _resultParameter._addParfectScoreValue * _playResult.CountPerfect;
        var goodValue = _resultParameter._addGoodScoreValue * _playResult.CountGood;
        var badValue = _resultParameter._addBadScoreValue * _playResult.CountBad;
        var totalScore = 0;

        totalScore += perfectValue + goodValue + badValue;  //スコア計算

        var result = new int[RESULT_COUNT] { badValue, goodValue, perfectValue, totalScore };  //画面に反映させる為の配列

        return result;
    }

    /// <summary>リザルトを表示するアニメーションを再生する</summary>
    void StartShowResultAnim()
    {
        StartCoroutine(_resultUIController.ShowResult(_scores));
    }

    /// <summary>
    /// スコアの値から結果を判定する
    /// </summary>
    private void JudgeResult()
    {
        if (LevelManager.Instance.CurrentLevel.InGame.PlayerHp <= _playResult.CountMiss)    //失敗
        {
            ResultBad();
            return;
        }

        if (_scores[TOTAL_SCORE_INDEX] >= _resultParameter._superPerfectLine)
        {
            ResultSuperPerfect();
        }
        else if (_scores[TOTAL_SCORE_INDEX] >= _resultParameter._perfectLine)
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

