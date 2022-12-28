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

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("���U���g��ʂł�Bad���̃t�@���Z���t")]
    public string[] _fanScriptsBad = new string[FAN_SCRIPTS_SIZE];

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("���U���g��ʂł�Good���̃t�@���Z���t")]
    public string[] _fanScriptsGood = new string[FAN_SCRIPTS_SIZE];

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("���U���g��ʂł�Excellent���̃t�@���Z���t")]
    public string[] _fanScriptsExcellent = new string[FAN_SCRIPTS_SIZE];

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("���U���g��ʂł�Perfect���̃t�@���Z���t")]
    public string[] _fanScriptsPerfect = new string[FAN_SCRIPTS_SIZE];
}
