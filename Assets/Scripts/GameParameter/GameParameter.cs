using UnityEngine;

/// <summary>�e���x���̏����܂Ƃ߂�N���X</summary>
[CreateAssetMenu]
public class GameParameter : ScriptableObject
{
    [SerializeField, Header("�C���Q�[���p�����[�^�[")]
    InGameParameter _inGame = default;

    [SerializeField, Header("���U���g�p�����[�^�[")]
    ResultParameter _result = default;

    [SerializeField, Header("��������")]
    EnemyOrderParameter _enemyOrder = default;

    [SerializeField, Header("�e�G�l�~�[�̃p�����[�^�["), ElementNames(new string[] { "�ʏ�", "�ǃt�@��2", "�ǃt�@��3", "�{�X" })]
    EnemyParameter[] _enemyParameters = new EnemyParameter[4];

    /// <summary>�C���Q�[���p�����[�^�[ </summary>
    public InGameParameter InGame => _inGame;

    /// <summary>���U���g�p�����[�^�[ </summary>
    public ResultParameter Result => _result;

    /// <summary>�������� </summary>
    public EnemyOrderParameter EnemyOrder => _enemyOrder;

    /// <summary>�e�G�l�~�[�̃p�����[�^�[ 0=�ʏ� 1=�ǃt�@��2 2=�ǃt�@��3  3=�{�X</summary>
    public EnemyParameter[] EnemyParameters => _enemyParameters;
}
