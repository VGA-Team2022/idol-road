using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSwitcher : MonoBehaviour
{
    ResultManager _result;

    [SerializeField, Tooltip("Excellent(��)�ȏ�Ȃ邽�߂�Bad����̐��̋��e�l")]
    private int _bad_to_Excellent = 0;
    [SerializeField, Tooltip("Perfect(�_)�ɏ��i���邽�߂ɕK�v��Perfect�̐�")]
    private int _perfect_to_Perfect = 10;
    [SerializeField, Tooltip("Good(����)����Excellent(��)�ɏ��i���邽�߂ɕK�v��Perfect�̐�")]
    private int _perfect_to_Excellent = 10;

    [SerializeField, Tooltip("�f�o�b�O���锻�萔")]
    private int _debugCount = 25;
    [SerializeField, Tooltip("�f�o�b�O����SIT����Perfect���̃����_���͈�")]
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
        //�f�o�b�O�p
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
        //Bad������Ƃ�Good(����)�X�^�[�g
        if (_result.CountBad > _bad_to_Excellent)
        {
            //Good(����)
            if (perfects < _perfect_to_Excellent)
            {
                ResultGood();
            }
            //�p�[�t�F�N�g��臒l��葽�����Perfect(�_)�@���i
            else if (perfects >= _perfect_to_Perfect)
            {
                ResultPerfect();
                Debug.Log("���i");
            }
            //Excellent(��)�@���i
            else if (perfects < _perfect_to_Perfect && perfects >= _perfect_to_Excellent)
            {
                ResultExcellent();
                Debug.Log("���i");
            }
            
        }
        //Bad���Ȃ��Ƃ�Excellent(��)�X�^�[�g
        else if (_result.CountBad <= _bad_to_Excellent)
        {
            //Good���Ȃ��Ȃ�Perfect(�_)
            if (_result.CountGood == 0)
            {
                ResultPerfect();
            }
            //Good������Ȃ�
            else if (_result.CountGood > 0)
            {
                //�p�[�t�F�N�g��臒l��葽�����Perfect(�_) ���i
                if(perfects >= _perfect_to_Perfect)
                {
                    ResultPerfect();
                    Debug.Log("���i");
                }
                //���Ȃ����Excellent(��)
                else if(perfects < _perfect_to_Perfect)
                {
                    ResultExcellent();
                }
            }
            else
            {
                Debug.LogError($"Good�������Ȓl�ł��I:{_result.CountGood}");
            }
            

        }
        else
        {
            Debug.LogError($"Bad�������Ȓl�ł��I:{_result.CountBad}");
        }
    }
    //Game Over
    public void ResultBad()
    {
        Debug.Log("Bad ��");
    }
    //����
    public void ResultGood()
    {
        Debug.Log("Good ����");
    }
    //��
    public void ResultExcellent()
    {
        Debug.Log("Excellent ��");
    }
    //�_�@�S��
    public void ResultPerfect()
    {
        Debug.Log("Perfect �_");
    }
}

