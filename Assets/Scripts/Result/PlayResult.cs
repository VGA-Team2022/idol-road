
/// <summary>�v���C���ʂ�ێ�����N���X </summary>
public class PlayResult
{
    private static PlayResult _instance = new PlayResult();
    public static PlayResult Instance => _instance;

    //�A�N�V�����𐬌��x�ʂɊi�[
    int _countBad;
    int _countGood;
    int _countPerfect;
    int _countMiss;

    /// <summary>�X�[�p�[�A�C�h���^�C�����̃^�b�v��</summary>
    int _valueSuperIdleTimePerfect;
    /// <summary>�ō��R���{�� </summary>
    int _highComboCount = 0;

    int _fullComboCount = 0;

    /// <summary>bad���擾������ </summary>
    public int CountBad
    {
        get => _countBad;
        set => _countBad = value;
    }

    /// <summary>Good���擾������ </summary>
    public int CountGood
    {
        get => _countGood;
        set => _countGood = value;
    }

    /// <summary>Perfect���擾������ </summary>
    public int CountPerfect
    {
        get => _countPerfect;
        set => _countPerfect = value;
    }

    /// <summary>���s(�_���[�W���󂯂�)��</summary>
    public int CountMiss
    {
        get => _countMiss;
        set => _countMiss = value;
    }

    /// <summary>�X�[�p�[�A�C�h���^�C�����̒ǉ��X�R�A </summary>
    public int ValueSuperIdleTimePerfect
    {
        get => _valueSuperIdleTimePerfect;
        set => _valueSuperIdleTimePerfect = value;
    }
    /// <summary>�ő�R���{���̃f�[�^</summary>
    public int HighComboCount
    {
        get => _highComboCount;
        set => _highComboCount = value;
    }
    /// <summary>�t���R���{�ɕK�v�ȃR���{��</summary>
    public int FullComboCount
    {
        get => _fullComboCount;
        set => _fullComboCount = value;
    }
    /// <summary>�v���C�f�[�^���폜����</summary>
    public void ResetResult()
    {
        _countBad = 0;
        _countGood = 0;
        _countPerfect = 0;
        _valueSuperIdleTimePerfect = 0;
        _highComboCount = 0;
        _countMiss = 0;
        foreach (var enemy in LevelManager.Instance.CurrentLevel.EnemyOrder.EnemyOrder)
        {
            if (enemy._enemyType != EnemyType.Wait) 
            {
                _fullComboCount++;
            } 
        }
    }
}
