using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResultSwitcher : MonoBehaviour
{
    [SerializeField]
    ResultUIController _resultUIController = default;
    [SerializeField, Tooltip("��Փx�ʂ�ScritableObject��Data�̖��O������"),Header("�ォ��Easy,Normal,Hard��")]
    string[] _modeNameData;
    [SerializeField,Header("�f�o�b�O�p")]
    private bool _isDebug = false;
    [SerializeField, Tooltip("��Փx�𔻒肵��Resoursec�œǂݍ��ޕ���������")]
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
        //�f�o�b�O�p
        if(Input.GetKeyDown(KeyCode.Space)&&_isDebug)
        {
            _result.CountBad = Random.Range(0,3);
            _result.CountGood = Random.Range(0, 3);
            _result.CountPerfect = Random.Range(0, 4);
            Debug.Log("Bad��" + _result.CountBad + "Good��" + _result.CountGood + "Perfect��" + _result.CountPerfect);
            JudgeResult();
        }
    }

    void JudgeMode()
    {
        if (_isEasy)// bool�ɂ���ēǂݍ���Data��������
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
        else if(_result.CountPerfect >= _resultData._perfectP && _result.CountGood == _resultData._goodP && _result.CountBad == _resultData._badP) //���ׂ�Perfect�������ꍇ
        {
            ResultPerfect();
        }
        else if(_result.CountGood >= _resultData._goodE && _result.CountBad == _resultData._badE)//Bad��0�ł���Good���P�ł��������ꍇ(Excellent)
        { 
            ResultExcellent();
        }
        else if(_result.CountBad <= _resultData._badB)//��L�ɂ��Ă͂܂�Ȃ������ꍇ�Œ�ۏ؂�Good
        {
            if(_result.CountPerfect >= _resultData._spesialPerfect)//�����Perfect�𒴂��Ă����ꍇPerfect����ɂ���
            {
                ResultPerfect();
                Debug.Log("SpecialPerfect");
            }
            else if(_result.CountPerfect >= _resultData._specialExsellent)//�����Perfect�𒴂��ďꍇExcellent����ɂ���
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
        Debug.Log("Bad ��");
        _resultUIController.ChangeResultImage(Result.Bad);
    }
    //����
    public void ResultGood()
    {
        Debug.Log("Good ����");
        _resultUIController.ChangeResultImage(Result.Good);
    }
    //��
    public void ResultExcellent()
    {
        Debug.Log("Excellent ��");
        _resultUIController.ChangeResultImage(Result.Excellent);
    }
    //�_�@�S��
    public void ResultPerfect()
    {
        Debug.Log("Perfect �_");
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

