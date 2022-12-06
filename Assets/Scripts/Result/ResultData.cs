using UnityEngine;

/// <summary>���U���g�����̏�������ScriptableObject���쐬����N���X</summary>
[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order =1)]
public class ResultData : ScriptableObject
{
    //TODO:�e�����ł�bad��,good��,perfect���̂������l���`����

    [SerializeField, Header("Perfect�ɂȂ����")]
    int _badP;
    [SerializeField]
    int _goodP;
    [SerializeField]
    int _perfectP;
    [SerializeField, Header("Good�ɂȂ����")]
    int _badG;
    [SerializeField]
    int _goodG;
    [SerializeField]
    int _perfectG;
    [SerializeField, Header("Bad�ɂȂ����")]
    int _badB;
    [SerializeField]
    int _goodB;
    [SerializeField]
    int _perfectB;
}
