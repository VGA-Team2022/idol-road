using UnityEngine;

/// <summary>�e���x���̏����܂Ƃ߂�N���X</summary>
[CreateAssetMenu]
public class GameParameter : ScriptableObject
{
    [SerializeField, Header("�C���Q�[���p�����[�^�[")]
    InGameParameter _inGame = default;

    [SerializeField, Header("���U���g�p�����[�^�[")]
    ResultParameter _result = default;

    [SerializeField, Header("�t�@���̐�������")]
    EnemyOrderParameter _enemyOrder = default;

    [SerializeField, Header("�A�C�e���̐�������")]
    ItemParameter _itemParameter = default;

    [SerializeField, Header("�X�[�p�[�A�C�h���^�C���̃p�����[�^�[")]
    SuperIdolTimeParamater _idolTimeParamater = default;

    [SerializeField, Header("�e�G�l�~�[�̃p�����[�^�["), ElementNames(new string[] { "�ʏ�", "�ǃt�@��2", "�ǃt�@��3", "�{�X" })]
    EnemyParameter[] _enemyParameters = new EnemyParameter[4];

    [SerializeField, Header("�{�X�X�e�[�W�ł̊e�G�p�����[�^�["), ElementNames(new string[] { "�ʏ�", "�ǃt�@��2", "�ǃt�@��3" })]
    BossStageParameter[] _bossStageParameters = new BossStageParameter[3];

    /// <summary>�C���Q�[���p�����[�^�[ </summary>
    public InGameParameter InGame => _inGame;

    /// <summary>���U���g�p�����[�^�[ </summary>
    public ResultParameter Result => _result;

    /// <summary>�t�@���̐������� </summary>
    public EnemyOrderParameter EnemyOrder => _enemyOrder;

    /// <summary>�A�C�e���̐������� </summary>
    public ItemParameter ItemParameter => _itemParameter;

    /// <summary>�X�[�p�[�A�C�h���^�C���̃p�����[�^�[</summary>
    public SuperIdolTimeParamater IdolTimeParamater => _idolTimeParamater;

    /// <summary>�e�G�l�~�[�̃p�����[�^�[ 0=�ʏ� 1=�ǃt�@��2 2=�ǃt�@��3  3=�{�X</summary>
    public EnemyParameter[] EnemyParameters => _enemyParameters;

    /// <summary>�{�X�X�e�[�W�ł̊e�G�p�����[�^�[ </summary>
    public BossStageParameter[] BossStageParameter => _bossStageParameters;
}
