using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSwitcher : MonoBehaviour
{
    [SerializeField]
    ResultUIController _resultUIController = default;
    [SerializeField, Tooltip("難易度別のScritableObjectのDataの名前を入れる"),Header("上からEasy,Normal,Hardで")]
    string[] _modeNameData;
    [SerializeField, Header("それぞれの判定から得られるスコア")]
    int _perfect;
    [SerializeField]
    int _good , _bad ;
    [SerializeField,Header("デバッグ用")]
    private bool _isDebug = false;
    [SerializeField, Tooltip("難易度を判定してResoursecで読み込む物をかえる")]
    bool _isEasy, _isNormal, _isHard;

    ResultParameter _resultData;
    ResultManager _result;
    [SerializeField,Tooltip("合計スコア")]
    int _score;
    public int Score { get => _score; set => _score = value; }
 
    void Start()
    {
        _isEasy = true;
        _score = 0;
        _result = ResultManager.Instance;
        JudgeMode();
        ScoreCalculation();
        JudgeResult();

        AudioManager.Instance.PlayVoice(18);
    }

    void Update()
    {
        //デバッグ用
        if(Input.GetKeyDown(KeyCode.Space)&&_isDebug)
        {
            _result.CountBad = Random.Range(0,3);
            _result.CountGood = Random.Range(0, 3);
            _result.CountPerfect = Random.Range(0, 4);
            Debug.Log("Bad数" + _result.CountBad + "Good数" + _result.CountGood + "Perfect数" + _result.CountPerfect);
            JudgeResult();
        }
    }

    void JudgeMode()
    {
        //if (_isEasy)// boolによって読み込むDataをかえる
        //{
        //    _resultData = Resources.Load<ResultData>(_modeNameData[0]);
        //}
        //else if (_isNormal)
        //{
        //    _resultData = Resources.Load<ResultData>(_modeNameData[1]);
        //}
        //else if (_isHard)
        //{
        //    _resultData = Resources.Load<ResultData>(_modeNameData[2]);
        //}
    }

    /// <summary>
    /// スコアの合計値を計算する
    /// </summary>
    private void ScoreCalculation() 
    {
        //判定それぞれのスコア値×判定した数
        _score += _perfect * _result.CountPerfect;
        _score += _good * _result.CountGood;
        _score += _bad * _result.CountBad;
    }

    /// <summary>
    /// スコアの値から結果を判定する
    /// </summary>
    private void JudgeResult()
    {
        //if (_result.CountMiss >= _resultData._missCount)
        //{
        //    ResultBad();
        //}

        //if (_score >= _resultData._superPerfectScore)
        //{
        //    ResultSuperPerfect();
        //}
        //else if(_score >= _resultData._perfectScore)
        //{
        //    ResultPerfect();
        //}
        //else 
        //{ 
        //    ResultGood();
        //}
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

    public void Test()
    {
        SceneManager.LoadScene("Title");
    }
}

public enum Result
{
    SuperPerfect,
    Perfect,
    Good,
    Bad
}

