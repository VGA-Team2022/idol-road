using UnityEngine;

/// <summary>�G�l�~�[�̃p�����[�^�[��ݒ肷��N���X </summary>
[CreateAssetMenu]
public class EnemyParameter : ScriptableObject
{
    [SerializeField, Header("�^����_���[�W")]
    int _addDamageValue = 0;

    [SerializeField, Header("�������Ă��鑬�x")]
    float _moveSpeed = 0f;

    [SerializeField, Header("�����ɂȂ�܂ł̑��x")]
    float _fadeSpeed = 0f;

    [SerializeField, Header("���Y������̕b��"), ElementNames(new string[] {"���v����", "Bad", "Good", "Perfect", "Out"})]
    float[] _rhythmTimes = new float[5];

    /// <summary>�������Ă��鑬�x</summary>
    public float MoveSpped => _moveSpeed;

    /// <summary>�����ɂȂ�܂ł̑��x</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>���Y������̕b�� 0=���v���� 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;

    /// <summary>�^����_���[�W </summary>
    public int AddDamageValue => _addDamageValue;
}
