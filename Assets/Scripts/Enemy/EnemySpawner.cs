using UnityEngine;

/// <summary>ファンを生成するクラス </summary>
public class EnemySpawner : MonoBehaviour
{
    [SerializeField, Tooltip("プレハブ")]
    Enemy _enemyPrefub = default;
    [SerializeField, Tooltip("スポーン地点")]
    GameObject _positionObject = default;
    [SerializeField, Tooltip("ゲームマネジャー")]
    GameManager _manager = default;
    [Tooltip("敵を出現させたい秒数")]
    float[] _timeInterval = new float[4] { 7.68f, 5.76f, 3.84f, 1.92f };

    /// <summary>生成間隔を決める添え字</summary>
    int _timeIntervalIndex = 0;
    /// <summary>秒数を数える変数</summary>
    float _generateTimer = 0;

    private void Start()
    {
        _timeIntervalIndex = Random.Range(0, _timeInterval.Length);
    }

    void Update()
    {
        if (_manager.CurrentEnemy == null)
        {
            _generateTimer += Time.deltaTime;

            if (_generateTimer >= _timeInterval[_timeIntervalIndex]) //_timeIntervalを超えるとInstantiateします
            {
                var enemy = Instantiate(_enemyPrefub, _positionObject.transform); //シリアライズで設定したオブジェクトの場所に出現します
                enemy.SetUp();

                //イベントを登録
                enemy.AddComboCount += _manager.ComboAmountTotal;
                enemy.StageScroll += _manager.Scroller.ScrollOperation;
                enemy.GiveDamage += _manager.GetDamage;

                _manager.CurrentEnemy = enemy;
                _manager.Scroller.ScrollOperation();    //ステージスクロールを止める
                _timeIntervalIndex = Random.Range(0, _timeInterval.Length);     //次の生成間隔を決める
                _generateTimer = 0;
            }
        }
    }

    /// <summary>
    /// ボタンに設定するパブリック関数
    /// falseになるとエネミーの出現が止まる
    /// 且つゲームの時間やエネミーが出てくるインターバルの時間をすべてリセット
    /// </summary>
    public void OffEnemy()
    {
        _generateTimer = 0;
    }
}
