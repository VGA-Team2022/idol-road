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

    [SerializeField, Header("�t���R���{�Ŋl���ł���X�R�A")]
    public int _addFullComboScoreValue;

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("=========���U���g��ʂł�Bad���̃t�@���Z���t=========")]
    public string[] _fanScriptsBad = new string[FAN_SCRIPTS_SIZE];
    [SerializeField, Header("�����̃e�L�X�g�ŕ\��������e")]
    public string _idolResultScriptBad = "";
    [SerializeField, Header("�����̃A�C�h���̃Z���t")]
    public string _idolScriptBad = "";

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("=========���U���g��ʂł�Good���̃t�@���Z���t=========")]
    public string[] _fanScriptsGood = new string[FAN_SCRIPTS_SIZE];
    [SerializeField, Header("�����̃e�L�X�g�ŕ\��������e")]
    public string _idolResultScriptGood = "";
    [SerializeField, Header("�����̃A�C�h���̃Z���t")]
    public string _idolScriptGood = "";

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("=========���U���g��ʂł�Excellent���̃t�@���Z���t=========")]
    public string[] _fanScriptsExcellent = new string[FAN_SCRIPTS_SIZE];
    [SerializeField, Header("�����̃e�L�X�g�ŕ\��������e")]
    public string _idolResultScriptExcellent = "";
    [SerializeField, Header("�����̃A�C�h���̃Z���t")]
    public string _idolScriptExcellent = "";

    [ElementNames(new string[] { "�n��", "�j�P", "�Ǐ�", "�j�Q", "���~" })]
    [SerializeField, Header("=========���U���g��ʂł�Perfect���̃t�@���Z���t=========")]
    public string[] _fanScriptsPerfect = new string[FAN_SCRIPTS_SIZE];
    [SerializeField, Header("�����̃e�L�X�g�ŕ\��������e")]
    public string _idolResultScriptPerfect = "";
    [SerializeField, Header("�����̃A�C�h���̃Z���t")]
    public string _idolScriptPerfect = "";
}
