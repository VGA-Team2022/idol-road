using UnityEngine;

/// <summary>リザルト条件の情報を持つScriptableObjectを作成するクラス</summary>
[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order =1)]
public class ResultData : ScriptableObject
{
    [SerializeField, Tooltip("難易度")]
    string _modeName;
    [SerializeField, Tooltip("Bad")]
    int _bad;
    [SerializeField, Tooltip("Good")]
    int _good;
    [SerializeField, Tooltip("Perfect")]
    int _perfect;
    //TODO:各条件でのbad数,good数,perfect数のしきい値を定義する
}
