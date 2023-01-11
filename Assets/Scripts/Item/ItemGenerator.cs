using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>アイテムを生成するクラス</summary>
public class ItemGenerator : MonoBehaviour
{
    [SerializeField, Header("右生成位置・到着位置")]
    Transform[] _rightPoints = default;
    [SerializeField, Header("左生成位置・到着位置")]
    Transform[] _leftPoints = default;
    [SerializeField, Header("ゲームマネージャー")]
    GameManager _gameManager = default;
    [SerializeField, Header("生成するアイテム"), ElementNames(new string[] { "ぬいぐるみ", "花束", "プレゼント", "お金" })]
    IdolPowerItem[] _items = default;

    ItemParameter _itemParameter => LevelManager.Instance.CurrentLevel.ItemParameter;

    /// <summary>重みの総和 </summary>
    float _totalWeight = 0f;

    /// <summary>各アイテムの出現確率</summary>
    float[] _itemWeights = default;

    /// <summary>生成間隔 </summary>
    float _generateTime = 5f;

    float _timer = 0f;
    /// <summary>生成をするかどうか </summary>
    bool _isGenerate = true;

    /// <summary>アイテムの生成順</summary>
    List<ItemInfo> _generatorItems = new List<ItemInfo>();

    /// <summary>次に生成するアイテムのindex</summary>
    int _generateIndex = 0;

    public bool IsGenerate
    {
        get { return _isGenerate; }
        set { _isGenerate = value; }   
    }
    private void Start()
    {
        if (_itemParameter.RandomGenerator || _itemParameter.GeneratorItems.Count == 0) 
        {
            _generateTime = _itemParameter.GeneratorInterval;
            _itemWeights = _itemParameter.ItemWeights;

            foreach (var weight in _itemWeights)    //重みの総和を計算する
            {
                _totalWeight += weight;
            }
        }
        else 
        {
            _generatorItems = _itemParameter.GeneratorItems;
            _generateTime = _generatorItems[_generateIndex]._generatorInterval;
        }
    }

    private void Update()
    {
        //生成間隔の値がマイナスだった場合生成しない
        if (_isGenerate && _generateTime > 0)
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
        if (_generatorItems.Count > 0)
        {
            ItemInfo itemInfo = _generatorItems[_generateIndex];

            var direction = _generatorItems[_generateIndex]._generateDirection;

            if (_generatorItems[_generateIndex]._generateDirection == ItemGenerateDirection.Random)
            {
                direction = (ItemGenerateDirection)Random.Range(0, System.Enum.GetNames(typeof(ItemGenerateDirection)).Length -1);
            }

            if (direction == ItemGenerateDirection.Right)
            {
                var item = Instantiate(_items[(int)itemInfo._item], _rightPoints[0].position, Quaternion.identity);
                item.GameManager = _gameManager;
                item.Move(_rightPoints[1].position);
            }
            else
            {
                var item = Instantiate(_items[(int)itemInfo._item], _leftPoints[0].position, Quaternion.identity);
                item.GameManager = _gameManager;
                item.Move(_leftPoints[1].position);
            }
            _generateIndex++;

            if (_generatorItems.Count > _generateIndex)
            {
                _generateTime = _generatorItems[_generateIndex]._generatorInterval;
                Debug.Log(_generateTime);
            }
            //マイナスにすることで生成しなくなる
            else { _generateTime = -1;  }
        }
        else
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
        }

        AudioManager.Instance.PlaySE(15, 0.5f);
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
