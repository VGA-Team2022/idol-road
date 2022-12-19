using UnityEngine;

/// <summary>�C���Q�[���p�����[�^�[�̐ݒ���s���N���X </summary>
[CreateAssetMenu]
public class InGameParameter : ScriptableObject
{
    [SerializeField, Header("��������")]
    float _gamePlayTime = 0f;

    [SerializeField, Header("�{�X�X�e�[�W���J�n���鎞��")]
    float _startBossTime = 0f;

    [SerializeField, Header("�v���C���[�̗̑�")]
    int _playerHp = 0;

    [SerializeField, Header("�A�C�h���p���[�̍ő�l")]
    int _idolPowerMaxValue = 0;

    /// <summary>��������</summary>
    public float GamePlayTime => _gamePlayTime;

    /// <summary>�{�X�X�e�[�W���J�n���鎞��</summary>
    public float StartBossTime => _startBossTime;

    /// <summary>�v���C���[�̗�</summary>
    public int PlayerHp => _playerHp;

    /// <summary>�A�C�h���p���[�̍ő�l </summary>
    public int IdolPowerMaxValue => _idolPowerMaxValue;
}
