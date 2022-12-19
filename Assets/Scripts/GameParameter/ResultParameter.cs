using UnityEngine;

/// <summary>���U���g�����̏�������ScriptableObject���쐬����N���X</summary>
[CreateAssetMenu]
public class ResultParameter : ScriptableObject
{
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
}
