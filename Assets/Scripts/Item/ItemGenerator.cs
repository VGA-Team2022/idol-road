using UnityEngine;

/// <summary>アイテムを生成するクラス</summary>
public class ItemGenerator : MonoBehaviour
{
    [SerializeField, Header("右生成位置・到着位置")]
    Transform[] _rightPoints = default;
    [SerializeField, Header("左生成位置・到着位置")]
    Transform[] _leftPoints = default;
    [SerializeField, Header("生成間隔")]
    float _generateTime = 5f;
    [SerializeField, Header("アイテムを親オブジェクト")]
    Transform _itemParentObj = default;
    [SerializeField, Header("生成元プレハブ")] 
    IdolPowerItem _itemPrefab = default;
    [SerializeField, Header("ゲームマネージャー")]
    GameManager _gameManager = default;
    /// <summary>生成されたアイテム </summary>
    IdolPowerItem _generateItem = default;
    float _time = 0f;
    /// <summary>生成をするかどうか </summary>
    bool _isGenerate = false;

    private void Update()
    {
        if (_isGenerate)
        {
            _time += Time.deltaTime;

            if (_generateTime <= _time)
            {
                Generate();
                _time = 0f;
            }
        }    
    }

    /// <summary>アイテムを生成する </summary>
    void Generate()
    {
        var direction = (GenerateDirection)Random.Range(0, 2);
        
        if (direction == GenerateDirection.Right)
        {
            _generateItem = Instantiate(_itemPrefab, _rightPoints[0].position, Quaternion.identity);
            _generateItem.GameManager = _gameManager;
            _generateItem.Move(_rightPoints[1].position, SetItemParent);
        }
        else 
        {
            _generateItem = Instantiate(_itemPrefab, _leftPoints[0].position, Quaternion.identity);
            _generateItem.GameManager = _gameManager;
            _generateItem.Move(_leftPoints[1].position, SetItemParent);
        }
    }

    /// <summary>アイテムをスクロールさせるためにステージの子オブジェクトにする </summary>
    void SetItemParent()
    {
        _generateItem.transform.SetParent(_itemParentObj, true);
    }

    /// <summary>ジェネレーターを起動、停止させる関数 </summary>
    public void GeneratorOperation()
    {
        _isGenerate = !_isGenerate;

        if (!_isGenerate)
        {
            _time = 0f;
        }
    }

    public enum GenerateDirection
    {
        /// <summary>右に生成 </summary>
        Right = 0,
        /// <summary>左に生成 </summary>
        Left = 1,
    }
}
