using UnityEngine;

/// <summary>���U���g�����̏�������ScriptableObject���쐬����N���X</summary>
[CreateAssetMenu(fileName = "ResultData",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order = 0)]
public class ResultData : ScriptableObject
{
    //TODO:�e�����ł�bad��,good��,perfect���̂������l���`����

    [SerializeField, Header("SuperPerfect�ɂȂ����")]
    public int _superPerfectScore;
    [SerializeField, Header("Perfect�ɂȂ����")]
    public int _perfectScore;
    [SerializeField, Header("Good�ɂȂ����")]
    public int _goodScore;
}
