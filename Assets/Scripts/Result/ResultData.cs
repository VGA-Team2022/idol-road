using UnityEngine;

/// <summary>リザルト条件の情報を持つScriptableObjectを作成するクラス</summary>
[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order =1)]
public class ResultData : ScriptableObject
{
    //TODO:各条件でのbad数,good数,perfect数のしきい値を定義する

    [SerializeField, Header("Perfectになる条件")]
    int _badP;
    [SerializeField]
    int _goodP;
    [SerializeField]
    int _perfectP;
    [SerializeField, Header("Goodになる条件")]
    int _badG;
    [SerializeField]
    int _goodG;
    [SerializeField]
    int _perfectG;
    [SerializeField, Header("Badになる条件")]
    int _badB;
    [SerializeField]
    int _goodB;
    [SerializeField]
    int _perfectB;
}
