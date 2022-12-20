using UnityEngine;

/// <summary>���U���g�����̏�������ScriptableObject���쐬����N���X</summary>
[CreateAssetMenu]
public class ResultParameter : ScriptableObject
{
    /// <summary>���U���g��ʂł̃t�@���Z���t�̐�</summary>
    const int FAN_SCRIPTS_SIZE = 5;

    [SerializeField, Header("SuperPerfect�ɂȂ����")]
    public int _superPerfectLine;

    [SerializeField, Header("Perfect�ɂȂ����")]
    public int _perfectLine;

    [SerializeField, Header("Good�ɂȂ����")]
    public int _goodLine;

    [SerializeField, Header("Bad���Ɋl���o����X�R�A")]
    public int _addBadScoreValue;

    [SerializeField, Header("Good���Ɋl���o����X�R�A")]
    public int _addGoodScoreValue;

    [SerializeField, Header("Perfect���Ɋl���o����X�R�A")]
    public int _addParfectScoreValue;

    [SerializeField, Header("�R���{�̔{��"), Range(1, 10)]
    public float _comboValue;

    [ElementNames(new string[] {"1��", "2��", "3��" , "4��" , "5��" })]
    [SerializeField, Header("���U���g��ʂł̃t�@���Z���t")]
    public string[] _fanScripts = new string[FAN_SCRIPTS_SIZE];
}
