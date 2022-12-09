using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultSwitcher : MonoBehaviour
{
    [SerializeField]
    ResultUIController _resultUIController = default;
    [SerializeField, Tooltip("難易度別のScritableObjectのDataの名前を入れる"),Header("上からEasy,Normal,Hardで")]
    string[] _modeNameData;
    [SerializeField,Header("デバッグ用")]
    private bool _isDebug = false;
    [SerializeField, Tooltip("難易度を判定してResoursecで読み込む物をかえる")]
    bool _isEasy, _isNormal, _isHard;
    ResultData _resultData;
    ResultManager _result;
    void Start()
    {
        _result = ResultManager.Instance;
        JudgeMode();
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
        if (_isEasy)// boolによって読み込むDataをかえる
        {
            _resultData = Resources.Load<ResultData>(_modeNameData[0]);
        }
        else if (_isNormal)
        {
            _resultData = Resources.Load<ResultData>(_modeNameData[1]);
        }
        else if (_isHard)
        {
            _resultData = Resources.Load<ResultData>(_modeNameData[2]);
        }
    }
    public void JudgeResult()
    {
        if (_result.CountBad >= _resultData._badB) //GameOver
        {
            ResultBad();
        }
        else if(_result.CountPerfect >= _resultData._perfectP && _result.CountGood == _resultData._goodP && _result.CountBad == _resultData._badP) //すべてPerfectだった場合
        {
            ResultPerfect();
        }
        else if(_result.CountGood >= _resultData._goodE && _result.CountBad == _resultData._badE)//Badが0でありGoodが１つでもあった場合(Excellent)
        { 
            ResultExcellent();
        }
        else if(_result.CountBad <= _resultData._badB)//上記にあてはまらなかった場合最低保証のGood
        {
            if(_result.CountPerfect >= _resultData._spesialPerfect)//特定のPerfectを超えていた場合Perfect判定にする
            {
                ResultPerfect();
                Debug.Log("SpecialPerfect");
            }
            else if(_result.CountPerfect >= _resultData._specialExsellent)//特定のPerfectを超えて場合Excellent判定にする
            {
                ResultExcellent();
                Debug.Log("SpesialExcellent");
            }
            else
            {
                ResultGood();
            }
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
    public void ResultExcellent()
    {
        Debug.Log("Excellent 良");
        _resultUIController.ChangeResultImage(Result.Excellent);
    }
    //神　全良
    public void ResultPerfect()
    {
        Debug.Log("Perfect 神");
        _resultUIController.ChangeResultImage(Result.Perfect);
    }
}

public enum Result
{
    Perfect,
    Excellent,
    Good,
    Bad
}

