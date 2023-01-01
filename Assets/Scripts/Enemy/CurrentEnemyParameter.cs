
/// <summary>各敵のパラメーターを保持・設定を行うクラス </summary>
public class CurrentEnemyParameter
{
    /// <summary>与えるダメージ </summary>
    int _addDamageValue = 0;

    /// <summary>透明になるまでの速度</summary>
    float _fadeSpeed = 0f;

    /// <summary>向かってくる速度</summary>
    float _moveSpeed = 0f;

    /// <summary>獲得できるアイドルパワー量</summary>
    int _addIdolPowerValue = 0;

    /// <summary>リズム判定の秒数 0=合計時間 1=Bad 2=Good 3=Perfect 4=Out</summary>
    float[] _rhythmTimes = new float[5];


    /// <summary>向かってくる速度</summary>
    public float MoveSpped => _moveSpeed;

    /// <summary>透明になるまでの速度</summary>
    public float FadeSpeed => _fadeSpeed;

    /// <summary>リズム判定の秒数 0=合計時間 1=Bad 2=Good 3=Perfect 4=Out</summary>
    public float[] RhythmTimes => _rhythmTimes;

    /// <summary>与えるダメージ </summary>
    public int AddDamageValue => _addDamageValue;

    /// <summary>獲得できるアイドルパワー量</summary>
    public int AddIdolPowerValue => _addIdolPowerValue;

    /// <summary>パラメーターを設定するコンストラクタ </summary>
    /// <param name="state">ゲームの状態</param>
    /// <param name="enemyType">敵の種類</param>
    public CurrentEnemyParameter(IState state, EnemyType enemyType)
    {
        var parameters = LevelManager.Instance.CurrentLevel.EnemyParameters;    //各敵のパラメーターリスト　0=普通　1=壁2  2=壁3  4=ボス

        if(enemyType == EnemyType.Boss)     //ボスであれば最初に設定する
        {
            SetNormalParameter(parameters, 3);
            return;
        }


        if (state is BossTime)      //ボスステージであればボスステージ用のパラメーターを設定する
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
            switch (enemyType)      //通常時のパラメーターを設定
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

    /// <summary>通常時のパラメーターを設定 </summary>
    void SetNormalParameter(EnemyParameter[] parameter, int index)
    {
        _addDamageValue = parameter[index].AddDamageValue;
        _fadeSpeed = parameter[index].FadeSpeed;
        _moveSpeed = parameter[index].MoveSpeed;
        _addIdolPowerValue = parameter[index].AddIdolPowerValue;
        _rhythmTimes = parameter[index].RhythmTimes;
    }

    /// <summary>ボスステージ用パラメーターを設定 </summary>
    void SetBossTimeParameter(EnemyParameter[] parameter, int index)
    {
        _addDamageValue = parameter[index].AddDamageValueBoss;
        _fadeSpeed = parameter[index].FadeSpeedBoss;
        _moveSpeed = parameter[index].MoveSpeedBoss;
        _addIdolPowerValue = parameter[index].AddIdolPowerValueBoss;
        _rhythmTimes = parameter[index].RhythmTimesBoss;
    }
}
