using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>リザルトシーンを管理するクラス </summary>
public class ResultManager : MonoBehaviour
{
    #region
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
    /// <summary>１プレイの結果</summary>
    Result _currentResult = Result.Good;
    /// <summary>各スコア 0=bad 1=good 2=perfect 3=合計スコア</summary>
    int[] _scores = default;
    /// <summary>再生しているBGMのID </summary>
    int _playBgmID = 0;
    /// <summary>遷移をしているか true=開始している</summary>
    bool _isTransition = false;


    public FadeController FadeController { get => _fadeController; }

    /// <summary>結果 </summary>
    public Result CurrentResult { get => _currentResult; }

    /// <summary>各スコア 0=bad 1=good 2=perfect 3=合計スコア</summary>
    public int[] Scores { get => _scores; }
    #endregion

    void Start()
    {
        _scores = ScoreCalculation();
        JudgeResult();

        _fadeController.FadeIn(() =>
        {
            _resultUIController.StartCommentAnime();
            PlayIdolVoice();
        });
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

        totalScore += perfectValue + goodValue + badValue + _playResult.ValueSuperIdleTimePerfect;  //スコア計算
        var result = new int[RESULT_COUNT] { badValue, goodValue, perfectValue, totalScore };  //画面に反映させる為の配列

        return result;
    }

    /// <summary>
    /// リザルトを表示するアニメーションを再生する
    /// ボタンから呼び出す
    /// </summary>
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

    /// <summary>結果によって再生するボイスを変更する </summary>
    public void PlayIdolVoice()
    {
        switch (_currentResult)
        {
            case Result.Bad:
                AudioManager.Instance.PlayVoice(15);
                AudioManager.Instance.PlayBGM(12);
                _playBgmID = 12;
                break;
            case Result.Good:
                AudioManager.Instance.PlayVoice(19);
                AudioManager.Instance.PlayBGM(13);
                _playBgmID = 13;
                break;
            case Result.Perfect:
                AudioManager.Instance.PlayVoice(18);
                AudioManager.Instance.PlayBGM(13);
                _playBgmID = 13;
                break;
            case Result.SuperPerfect:
                AudioManager.Instance.PlayVoice(17);
                AudioManager.Instance.PlayBGM(13);
                _playBgmID = 13;
                break;
        }
    }


    //Game Over
    public void ResultBad()
    {
        _currentResult = Result.Bad;
        _resultUIController.SetupUI(_currentResult);
    }

    //普通
    public void ResultGood()
    {
        _currentResult = Result.Good;
        _resultUIController.SetupUI(_currentResult);

    }

    //良
    public void ResultPerfect()
    {
        _currentResult = Result.Perfect;
        _resultUIController.SetupUI(_currentResult);

    }

    //神　全良
    public void ResultSuperPerfect()
    {
        _currentResult = Result.SuperPerfect;
        _resultUIController.SetupUI(_currentResult);
    }

    /// <summary>
    /// 難易度セレクトシーンに戻るかリトライするか
    /// ボタンから呼び出す
    /// </summary>
    /// <param name="index">シーン番号</param>
    public void ReturnModeSelectAndRetry(int index)
    {
        if (_isTransition) { return; }

        _isTransition = true;
        _fadeController.FadeOut(() => { SceneManager.LoadScene(index); });

        if (_currentResult == Result.Bad) //リトライ用ボイスを再生する
        {
            AudioManager.Instance.PlayVoice(16);
        }

        AudioManager.Instance.PlaySE(7);
        AudioManager.Instance.StopBGM(_playBgmID);
    }

}

public enum Result
{
    SuperPerfect,
    Perfect,
    Good,
    Bad
}

