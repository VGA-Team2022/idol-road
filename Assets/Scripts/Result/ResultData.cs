using UnityEngine;

/// <summary>���U���g�����̏�������ScriptableObject���쐬����N���X</summary>
[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order =1)]
public class ResultData : ScriptableObject
{
    [SerializeField, Tooltip("��Փx")]
    string _modeName;
    [SerializeField, Tooltip("Bad")]
    int _bad;
    [SerializeField, Tooltip("Good")]
    int _good;
    [SerializeField, Tooltip("Perfect")]
    int _perfect;
    //TODO:�e�����ł�bad��,good��,perfect���̂������l���`����
}
