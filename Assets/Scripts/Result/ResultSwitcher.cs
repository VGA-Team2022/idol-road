using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSwitcher : MonoBehaviour
{
    [SerializeField]
    ResultUIController _resultUIController = default;
    [SerializeField, Tooltip("��Փx�ʂ�ScritableObject��Data�̖��O������"),Header("�ォ��Easy,Normal,Hard��")]
    string[] _modeNameData;
    [SerializeField, Header("���ꂼ��̔��肩�瓾����X�R�A")]
    int _perfect;
    [SerializeField]
    int _good , _bad ;
    [SerializeField,Header("�f�o�b�O�p")]
    private bool _isDebug = false;
    [SerializeField, Tooltip("��Փx�𔻒肵��Resoursec�œǂݍ��ޕ���������")]
    bool _isEasy, _isNormal, _isHard;

    ResultParameter _resultData;
    ResultManager _result;
    [SerializeField,Tooltip("���v�X�R�A")]
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
        //if (_isEasy)// bool�ɂ���ēǂݍ���Data��������
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
    /// �X�R�A�̍��v�l���v�Z����
    /// </summary>
    private void ScoreCalculation() 
    {
        //���肻�ꂼ��̃X�R�A�l�~���肵����
        _score += _perfect * _result.CountPerfect;
        _score += _good * _result.CountGood;
        _score += _bad * _result.CountBad;
    }

    /// <summary>
    /// �X�R�A�̒l���猋�ʂ𔻒肷��
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
    public void ResultPerfect()
    {
        Debug.Log("Perfect �_");
        _resultUIController.ChangeResultImage(Result.Perfect);
    }
    //�_�@�S��
    public void ResultSuperPerfect()
    {
        Debug.Log("SuperPerfect �S�\�_");
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

