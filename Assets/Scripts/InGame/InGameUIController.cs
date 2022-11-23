using UnityEngine;
using UnityEngine.UI;

/// <summary>ゲームシーンで使用するUIの管理するクラス </summary>
[RequireComponent(typeof(GameManager))]
public class InGameUIController : MonoBehaviour
{
    [SerializeField, Header("ゴールまでの距離を表示するスライダー")]
    Slider _goalSlider = default;
    [SerializeField, Header("アイドルパワーを表示するスライダー")]
    Slider _idolPowerGauge = default;
    [SerializeField, Header("HPUIの親オブジェクト")]
    GridLayoutGroup _hpUIParent = default;
    [SerializeField, Header("HPUIのプレハブ")]
    Image _hpPrefab = default;
    /// <summary>現在の表示しているHpUIの配列</summary>
    Image[] _currentHpUIArray = default;
    GameManager _gameManager => GetComponent<GameManager>();

    private void Start()
    {
        _currentHpUIArray = new Image[_gameManager.MaxIdleHp];

        for (var i = 0; i < _gameManager.MaxIdleHp; i++)    //HPUIを最大体力分表示する
        {
            HpUIGenerator(i);
        }

        _goalSlider.maxValue = _gameManager.CountTime;          //スライダーの最大値をゲームプレイ時間と同じにする
        _idolPowerGauge.maxValue = _gameManager.MaxIdlePower;   //アイドルパワーゲージ最大値を変更する

        //関数を登録
        _gameManager.OnReduceHpUI += ReduseHpUI;
        _gameManager.OnChangeIdolPowerGauge += ChangeIdolPowerGauge;
    }

    private void Update()
    {
        _goalSlider.value = _gameManager.ElapsedTime;   //ゴールまでの距離をUIに表示する
    }

    /// <summary>HPUIを生成し配列に追加する </summary>
    /// <param name="index">配列の添え字</param>
    void HpUIGenerator(int index)
    {
        var hpUI = Instantiate(_hpPrefab);
        var liveHaart = hpUI.transform.GetChild(0).GetComponent<Image>();   //子オブジェクトにある残ライフのイラストを取得する
        hpUI.transform.SetParent(_hpUIParent.transform);

        hpUI.transform.localScale = new Vector3(1, 1, 1);   //初期の大きさを決める
        liveHaart.rectTransform.sizeDelta = new Vector2(_hpUIParent.cellSize.x, _hpUIParent.cellSize.y);    //残ライフの大きさを調整する
            
        _currentHpUIArray[index] = liveHaart;
    }

    /// <summary>現在の体力とHPUIを合わせる</summary>
    /// <param name="currentHp">現在の体力</param>
    void ReduseHpUI(int currentHp)
    {
        _currentHpUIArray[currentHp].enabled = false;
    }

    /// <summary> アイドルパワーゲージを増減させる</summary>
    /// <param name="value">増減値(減少させる場合は負数を入れる)</param>
    void ChangeIdolPowerGauge(int value)
    {
        _idolPowerGauge.value = value;
    }
}
