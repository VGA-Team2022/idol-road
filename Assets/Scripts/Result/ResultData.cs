using UnityEngine;

/// <summary>リザルト条件の情報を持つScriptableObjectを作成するクラス</summary>
[CreateAssetMenu(fileName = "ResultData",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order = 0)]
public class ResultData : ScriptableObject
{
    //TODO:各条件でのbad数,good数,perfect数のしきい値を定義する

    [SerializeField, Header("Perfectになる条件")]
    public int _badP;
    [SerializeField]
    public int _goodP;
    [SerializeField]
    public int _perfectP;
    [SerializeField, Header("Goodになる条件")]
    public int _badG;
    [SerializeField]
    public int _goodG;
    [SerializeField]
    public int _perfectG;
    [SerializeField, Header("Badになる条件")]
    public int _badB;
    [SerializeField]
    public int _goodB;
    [SerializeField]
    public int _perfectB;
}
