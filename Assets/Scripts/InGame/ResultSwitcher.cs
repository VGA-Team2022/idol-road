using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSwitcher : MonoBehaviour
{
    ResultManager _result;

    [SerializeField, Tooltip("Excellent(良)以上なるためのBad判定の数の許容値")]
    private int _bad_to_Excellent = 0;
    [SerializeField, Tooltip("Perfect(神)に昇格するために必要なPerfectの数")]
    private int _perfect_to_Perfect = 10;
    [SerializeField, Tooltip("Good(普通)からExcellent(良)に昇格するために必要なPerfectの数")]
    private int _perfect_to_Excellent = 10;

    [SerializeField, Tooltip("デバッグする判定数")]
    private int _debugCount = 25;
    [SerializeField, Tooltip("デバッグするSIT中のPerfect数のランダム範囲")]
    private int _randomPerfectCount = 50;
    [SerializeField]
    private bool _isDebug = false;
    // Start is called before the first frame update
    void Start()
    {
        _result = ResultManager.Instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ用
        if(Input.GetKeyDown(KeyCode.Space)&&_isDebug)
        {
            _result.CountBad = Random.Range(0, 5);   
            _result.CountGood = Random.Range(0, _debugCount - _result.CountBad);
            _result.CountPerfect = _debugCount - (_result.CountBad+_result.CountGood);
            _result.CountSuperIdleTimePerfect = Random.Range(0,_randomPerfectCount);
            Debug.Log($"Bad:{_result.CountBad},Good:{_result.CountGood},Perfect:{_result.CountPerfect + _result.CountSuperIdleTimePerfect}(SITP:{_result.CountSuperIdleTimePerfect})");
            JudgeResult();
        }
    }

    public void JudgeResult()
    {
        var perfects = _result.CountPerfect + _result.CountSuperIdleTimePerfect;
        //BadがあるときGood(普通)スタート
        if (_result.CountBad > _bad_to_Excellent)
        {
            //Good(普通)
            if (perfects < _perfect_to_Excellent)
            {
                ResultGood();
            }
            //パーフェクトが閾値より多ければPerfect(神)　昇格
            else if (perfects >= _perfect_to_Perfect)
            {
                ResultPerfect();
                Debug.Log("昇格");
            }
            //Excellent(良)　昇格
            else if (perfects < _perfect_to_Perfect && perfects >= _perfect_to_Excellent)
            {
                ResultExcellent();
                Debug.Log("昇格");
            }
            
        }
        //BadがないときExcellent(良)スタート
        else if (_result.CountBad <= _bad_to_Excellent)
        {
            //GoodがないならPerfect(神)
            if (_result.CountGood == 0)
            {
                ResultPerfect();
            }
            //Goodがあるなら
            else if (_result.CountGood > 0)
            {
                //パーフェクトが閾値より多ければPerfect(神) 昇格
                if(perfects >= _perfect_to_Perfect)
                {
                    ResultPerfect();
                    Debug.Log("昇格");
                }
                //少なければExcellent(良)
                else if(perfects < _perfect_to_Perfect)
                {
                    ResultExcellent();
                }
            }
            else
            {
                Debug.LogError($"Goodが無効な値です！:{_result.CountGood}");
            }
            

        }
        else
        {
            Debug.LogError($"Badが無効な値です！:{_result.CountBad}");
        }
    }
    //Game Over
    public void ResultBad()
    {
        Debug.Log("Bad 悪");
    }
    //普通
    public void ResultGood()
    {
        Debug.Log("Good 普通");
    }
    //良
    public void ResultExcellent()
    {
        Debug.Log("Excellent 良");
    }
    //神　全良
    public void ResultPerfect()
    {
        Debug.Log("Perfect 神");
    }
}

