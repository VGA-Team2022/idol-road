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
    [SerializeField, Header("Excellent")]
    public int _badE;
    [SerializeField]
    public int _goodE;
    [SerializeField, Header("Goodになる条件")]
    public int _badG;
    [SerializeField, Header("Badになる条件")]
    public int _badB;
    [SerializeField, Header("リザルトが変わる特別条件")]
    public int _specialExsellent;
    [SerializeField]
    public int _spesialPerfect;
}
