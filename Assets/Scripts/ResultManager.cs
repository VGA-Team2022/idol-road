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
    private int _countGreat;
    //�S�[���n�_�܂ł̋���
    private float _distance;
    //�X�[�p�[�A�C�h���^�C���̔�����
    private int _countSuperIdleTime;
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
    public int CountGreat
    {
        get => _countGreat;
        set => _countGreat = value;
    }
    public float Distance
    {
        get => _distance; 
        set => _distance = value;
    }

    public int CountSuperIdleTime
    {
        get => _countSuperIdleTime; 
        set => _countSuperIdleTime = value;
    }
}
