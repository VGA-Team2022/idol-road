using UnityEngine;

/// <summary>�G�l�~�[�̃p�����[�^�[��ݒ肷��N���X </summary>
[CreateAssetMenu]
public class EnemyParameter : ScriptableObject
{
    [SerializeField, Header("----------------------------�ʏ펞-------------------------------"),Header("�^����_���[�W")]
    int _addDamageValue = 0;

    [SerializeField, Header("�������Ă��鑬�x")]
    float _moveSpeed = 0f;

    [SerializeField, Header("�����ɂȂ�܂ł̑��x")]
    float _fadeSpeed = 0f;

    [SerializeField, Header("�l���o����A�C�h���p���[��")]
    int _addIdolPowerValue = 0;

    [SerializeField, Header("���Y������̕b��"), ElementNames(new string[] {"���v����", "Bad", "Good", "Perfect", "Out"})]
    float[] _rhythmTimes = new float[5];

    [SerializeField, Header("----------------------------�{�X��̎�----------------------------"), Header("�^����_���[�W")]
    int _addDamageValueBoss = 0;

    [SerializeField, Header("�������Ă��鑬�x")]
    float _moveSpeedBoss = 0f;

    [SerializeField, Header("�����ɂȂ�܂ł̑��x")]
    float _fadeSpeedBoss = 0f;

    [SerializeField, Header("�l���o����A�C�h���p���[��")]
    int _addIdolPowerValueBoss = 0;

    [SerializeField, Header("���Y������̕b��"), ElementNames(new string[] { "���v����", "Bad", "Good", "Perfect", "Out" })]
    float[] _rhythmTimesBoss = new float[5];

    /// <summary>�������Ă��鑬�x</summary>
    public float MoveSpeed => _moveSpeed;

    /// <summary>�����ɂȂ�܂ł̑��x</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>���Y������̕b�� 0=���v���� 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;
   
    /// <summary>�^����_���[�W </summary>
    public int AddDamageValue => _addDamageValue;
   
    /// <summary>�l���ł���A�C�h���p���[��</summary>
    public int AddIdolPowerValue => _addIdolPowerValue;


    //========== �ȉ� �{�X�X�e�[�W�p�p�����[�^�[�v���p�e�B ===========

    /// <summary>�_���[�W�� </summary>
    public int AddDamageValueBoss => _addDamageValueBoss;

    /// <summary>�ړ����x </summary>
    public float MoveSpeedBoss => _moveSpeedBoss;

    /// <summary>�t�F�[�h���x </summary>
    public float FadeSpeedBoss => _fadeSpeedBoss;

    /// <summary>���Y������</summary>
    public float[] RhythmTimesBoss => _rhythmTimesBoss;

    /// <summary>�A�C�h���p���[�̊l���� </summary>
    public int AddIdolPowerValueBoss => _addIdolPowerValueBoss;


}
