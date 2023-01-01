using UnityEngine;

/// <summary>�{�X�X�e�[�W�ł̒ʏ�t�@���p�����[�^�[ </summary>
[CreateAssetMenu]
public class BossStageParameter : ScriptableObject
{
    [SerializeField, Header("----------------------------�{�X��̎�----------------------------"), Header("�^����_���[�W")]
    int _addDamageValue = 0;

    [SerializeField, Header("�������Ă��鑬�x")]
    float _moveSpeed = 0f;

    [SerializeField, Header("�����ɂȂ�܂ł̑��x")]
    float _fadeSpeed = 0f;

    [SerializeField, Header("�l���o����A�C�h���p���[��")]
    int _addIdolPowerValue = 0;

    [SerializeField, Header("���Y������̕b��"), ElementNames(new string[] { "���v����", "Bad", "Good", "Perfect", "Out" })]
    float[] _rhythmTimes = new float[5];

    /// <summary>�������Ă��鑬�x</summary>
    public float MoveSpped => _moveSpeed;

    /// <summary>�����ɂȂ�܂ł̑��x</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>���Y������̕b�� 0=���v���� 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;

    /// <summary>�^����_���[�W </summary>
    public int AddDamageValue => _addDamageValue;

    /// <summary>�l���ł���A�C�h���p���[��</summary>
    public int AddIdolPowerValue => _addIdolPowerValue;
}
