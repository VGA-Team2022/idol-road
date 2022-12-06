using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager 
{
    private static ResultManager _instance = new ResultManager();
    public static ResultManager Instance => _instance;
        
    

    //アクションを成功度別に格納
    private int _countBad;
    private int _countGood;
    private int _countPerfect;
    private int _countMiss;
    //ゴール地点までの距離
    private float _distance;
    //スーパーアイドルタイム中のタップ回数
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
