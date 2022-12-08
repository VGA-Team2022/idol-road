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
    [SerializeField, Header("Excellent")]
    public int _badE;
    [SerializeField]
    public int _goodE;
    [SerializeField, Header("Good�ɂȂ����")]
    public int _badG;
    [SerializeField, Header("Bad�ɂȂ����")]
    public int _badB;
    [SerializeField, Header("���U���g���ς����ʏ���")]
    public int _specialExsellent;
    [SerializeField]
    public int _spesialPerfect;
}
