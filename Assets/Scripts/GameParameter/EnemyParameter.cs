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
    int _addDamageValueToBossBattle = 0;

    [SerializeField, Header("�������Ă��鑬�x")]
    float _moveSpeedToBossBattle = 0f;

    [SerializeField, Header("�����ɂȂ�܂ł̑��x")]
    float _fadeSpeedToBossBattle = 0f;

    [SerializeField, Header("�l���o����A�C�h���p���[��")]
    int _addIdolPowerValueToBossBattle = 0;

    [SerializeField, Header("���Y������̕b��"), ElementNames(new string[] { "���v����", "Bad", "Good", "Perfect", "Out" })]
    float[] _rhythmTimesToBossBattle = new float[5];
    
    /// <summary>�������Ă��鑬�x</summary>
    public float MoveSpped => _moveSpeed;

    /// <summary>�{�X��̂Ƃ��̌������Ă��鑬�x</summary>
    public float MoveSpeedToBossBattle => _moveSpeedToBossBattle;

    /// <summary>�����ɂȂ�܂ł̑��x</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>�{�X��̂Ƃ��̓����ɂȂ�܂ł̑��x</summary>
    public float FadeSpeedToBossBattle => _fadeSpeedToBossBattle;

    /// <summary>���Y������̕b�� 0=���v���� 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;

    /// <summary>�{�X�펞�̃��Y������̕b�� 0=���v���� 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimesToBossBattle => _rhythmTimesToBossBattle;

    /// <summary>�^����_���[�W </summary>
    public int AddDamageValue => _addDamageValue;

    /// <summary>�{�X��̂Ƃ��̗^����_���[�W</summary>
    public int AddDamageValueToBossBattle => _addDamageValueToBossBattle;
    /// <summary>�l���ł���A�C�h���p���[��</summary>
    public int AddIdolPowerValue => _addIdolPowerValue;
    /// <summary>�{�X��̂Ƃ��̊l���o����A�C�h���p���[�� </summary>
    public int AddIdolPowerValueToBossBattle => _addIdolPowerValueToBossBattle;
}
