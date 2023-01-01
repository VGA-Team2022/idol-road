
/// <summary>�e�G�̃p�����[�^�[��ێ��E�ݒ���s���N���X </summary>
public class CurrentEnemyParameter
{
    /// <summary>�^����_���[�W </summary>
    int _addDamageValue = 0;

    /// <summary>�����ɂȂ�܂ł̑��x</summary>
    float _fadeSpeed = 0f;

    /// <summary>�������Ă��鑬�x</summary>
    float _moveSpeed = 0f;

    /// <summary>�l���ł���A�C�h���p���[��</summary>
    int _addIdolPowerValue = 0;

    /// <summary>���Y������̕b�� 0=���v���� 1=Bad 2=Good 3=Perfect 4=Out</summary>
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

    /// <summary>�p�����[�^�[��ݒ肷��R���X�g���N�^ </summary>
    /// <param name="state">�Q�[���̏��</param>
    /// <param name="enemyType">�G�̎��</param>
    public CurrentEnemyParameter(IState state, EnemyType enemyType)
    {
        var parameters = LevelManager.Instance.CurrentLevel.EnemyParameters;    //�e�G�̃p�����[�^�[���X�g�@0=���ʁ@1=��2  2=��3  4=�{�X

        if(enemyType == EnemyType.Boss)     //�{�X�ł���΍ŏ��ɐݒ肷��
        {
            SetNormalParameter(parameters, 3);
            return;
        }


        if (state is BossTime)      //�{�X�X�e�[�W�ł���΃{�X�X�e�[�W�p�̃p�����[�^�[��ݒ肷��
        {
            switch (enemyType)
            {
                case EnemyType.Nomal:
                    SetBossTimeParameter(parameters, 0);
                    break;
                case EnemyType.Wall2:
                    SetBossTimeParameter(parameters, 1);
                    break;
                case EnemyType.Wall3:
                    SetBossTimeParameter(parameters, 2);
                    break;
              
            }
        }
        else
        {
            switch (enemyType)      //�ʏ펞�̃p�����[�^�[��ݒ�
            {
                case EnemyType.Nomal:
                    SetNormalParameter(parameters, 0);
                    break;
                case EnemyType.Wall2:
                    SetNormalParameter(parameters, 1);
                    break;
                case EnemyType.Wall3:
                    SetNormalParameter(parameters, 2);
                    break;
            }
        }
    }

    /// <summary>�ʏ펞�̃p�����[�^�[��ݒ� </summary>
    void SetNormalParameter(EnemyParameter[] parameter, int index)
    {
        _addDamageValue = parameter[index].AddDamageValue;
        _fadeSpeed = parameter[index].FadeSpeed;
        _moveSpeed = parameter[index].MoveSpeed;
        _addIdolPowerValue = parameter[index].AddIdolPowerValue;
        _rhythmTimes = parameter[index].RhythmTimes;
    }

    /// <summary>�{�X�X�e�[�W�p�p�����[�^�[��ݒ� </summary>
    void SetBossTimeParameter(EnemyParameter[] parameter, int index)
    {
        _addDamageValue = parameter[index].AddDamageValueBoss;
        _fadeSpeed = parameter[index].FadeSpeedBoss;
        _moveSpeed = parameter[index].MoveSpeedBoss;
        _addIdolPowerValue = parameter[index].AddIdolPowerValueBoss;
        _rhythmTimes = parameter[index].RhythmTimesBoss;
    }
}
