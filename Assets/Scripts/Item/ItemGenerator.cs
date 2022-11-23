using UnityEngine;

/// <summary>アイテムを生成するクラス</summary>
public class ItemGenerator : MonoBehaviour
{
    [Tooltip("値が高ければ高いほど出現します")]
    [SerializeField, Header("各アイテムの出現確率"), ElementNames(new string[] { "ぬいぐるみ", "花束", "プレゼント", "お金" }), Range(0f, 10f)]
    float[] _itemWeights = default;
    [SerializeField, Header("生成間隔")]
    float _generateTime = 5f;
    [SerializeField, Header("右生成位置・到着位置")]
    Transform[] _rightPoints = default;
    [SerializeField, Header("左生成位置・到着位置")]
    Transform[] _leftPoints = default;
    [SerializeField, Header("ゲームマネージャー")]
    GameManager _gameManager = default;
    [SerializeField, Header("生成するアイテム"), ElementNames(new string[] { "ぬいぐるみ", "花束", "プレゼント", "お金" })]
    IdolPowerItem[] _items = default;

    /// <summary>重みの総和 </summary>
    float _totalWeight = 0f;

    float _timer = 0f;
    /// <summary>生成をするかどうか </summary>
    bool _isGenerate = false;

    private void Start()
    {   
        foreach (var weight in _itemWeights)    //重みの総和を計算する
        {
            _totalWeight += weight;
        }
    }

    private void Update()
    {
        if (_isGenerate)
        {
            _timer += Time.deltaTime;

            if (_generateTime <= _timer)
            {
                Generate();
                _timer = 0f;
            }
        }
    }

    /// <summary>アイテムを生成する </summary>
    void Generate()
    {
        var direction = (GenerateDirection)Random.Range(0, System.Enum.GetNames(typeof(GenerateDirection)).Length);

        if (direction == GenerateDirection.Right)
        {
            var item = Instantiate(_items[GetSelectItemIndex()], _rightPoints[0].position, Quaternion.identity);
            item.GameManager = _gameManager;
            item.Move(_rightPoints[1].position);
        }
        else
        {
            var item = Instantiate(_items[GetSelectItemIndex()], _leftPoints[0].position, Quaternion.identity);
            item.GameManager = _gameManager;
            item.Move(_leftPoints[1].position);
        }

        AudioManager.Instance.PlaySE(15);
    }

    /// <summary>生成するアイテムの添え字を取得する </summary>
    /// <returns>生成するアイテムの添え字</returns>
    int GetSelectItemIndex()
    {
        var rand = Random.Range(0, _totalWeight); 
        var currnetWeight = 0f;     //現在の重さ

        for (var i = 0; i < _itemWeights.Length; i++)   // 乱数値が属する要素を先頭から順に選択
        {
            currnetWeight += _itemWeights[i];   // 現在要素までの重みの総和を求める

            if (rand < currnetWeight)   // 乱数値が現在要素の範囲内かチェック
            {
                return i;
            }
        }

        return _itemWeights.Length - 1;     // 乱数値が重みの総和以上なら末尾要素とする
    }

    /// <summary>ジェネレーターを起動、停止させる関数 </summary>
    public void GeneratorOperation()
    {
        _isGenerate = !_isGenerate;
    }

    public enum GenerateDirection
    {
        /// <summary>右に生成 </summary>
        Right = 0,
        /// <summary>左に生成 </summary>
        Left = 1,
    }
}
