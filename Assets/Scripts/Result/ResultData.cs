using UnityEngine;

/// <summary>���U���g�����̏�������ScriptableObject���쐬����N���X</summary>
[CreateAssetMenu(fileName = "ResultData",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order = 0)]
public class ResultData : ScriptableObject
{
    //TODO:�e�����ł�bad��,good��,perfect���̂������l���`����

    [SerializeField, Header("Perfect�ɂȂ����")]
    public int _badP;
    [SerializeField]
    public int _goodP;
    [SerializeField]
    public int _perfectP;
    [SerializeField, Header("Good�ɂȂ����")]
    public int _badG;
    [SerializeField]
    public int _goodG;
    [SerializeField]
    public int _perfectG;
    [SerializeField, Header("Bad�ɂȂ����")]
    public int _badB;
    [SerializeField]
    public int _goodB;
    [SerializeField]
    public int _perfectB;
}
