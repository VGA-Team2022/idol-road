using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager 
{
    private static ResultManager _instance = new ResultManager();
    public static ResultManager Instance => _instance;
        
    

    //�A�N�V�����𐬌��x�ʂɊi�[
    private int _countBad;
    private int _countGood;
    private int _countPerfect;
    private int _countMiss;
    //�S�[���n�_�܂ł̋���
    private float _distance;
    //�X�[�p�[�A�C�h���^�C�����̃^�b�v��
    private int _countSuperIdleTimePerfect;
    public int CountBad
    {
        get => _countBad;
        set => _countBad = value;
    }
    public int CountGood 
    {
        get => _countGood; 
        set => _countGood = value;
    }
    public int CountPerfect
    {
        get => _countPerfect;
        set => _countPerfect = value;
    }
    public float Distance
    {
        get => _distance; 
        set => _distance = value;
    }

    public int CountSuperIdleTimePerfect
    {
        get => _countSuperIdleTimePerfect; 
        set => _countSuperIdleTimePerfect = value;
    }
    public int CountMiss { get => _countMiss; set => _countMiss = value; }
}
