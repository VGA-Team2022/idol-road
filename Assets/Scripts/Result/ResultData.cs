using UnityEngine;

/// <summary>リザルト条件の情報を持つScriptableObjectを作成するクラス</summary>
[CreateAssetMenu(fileName = "ResultData",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order = 0)]
public class ResultData : ScriptableObject
{
    //TODO:各条件でのbad数,good数,perfect数のしきい値を定義する

    [SerializeField, Header("SuperPerfectになる条件")]
    public int _superPerfectScore;
    [SerializeField, Header("Perfectになる条件")]
    public int _perfectScore;
    [SerializeField, Header("Goodになる条件")]
    public int _goodScore;
}
