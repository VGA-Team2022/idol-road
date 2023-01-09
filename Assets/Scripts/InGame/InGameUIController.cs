using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>ゲームシーンで使用するUIの管理するクラス </summary>
public class InGameUIController : MonoBehaviour
{
    [SerializeField, Header("コンボ時に表示するイラストの順番"), ElementNames(new string[] {"1", "2", "3", "4"})]
    Sprite[] _comboSprites = default;
    [SerializeField, Tooltip("ゴールまでの距離を表示するスライダー")]
    Slider _goalSlider = default;
    [SerializeField, Tooltip("アイドルパワーを表示するImage")]
    Image _idolPowerGauge = default;
    [SerializeField, Tooltip("HPUIの親オブジェクト")]
    GridLayoutGroup _hpUIParent = default;
    [SerializeField, Tooltip("HPUIのプレハブ")]
    Image _hpPrefab = default;
    [SerializeField, Tooltip("コンボ数を表示するテキスト")]
    TMP_Text _comboText = default;
    [SerializeField, Tooltip("コンボイラストを表示するImage")]
    Image _comboImage = default;
    [SerializeField, Tooltip("コンボで使用するアニメーター"), ElementNames(new string[] {"背景", "イラスト"})]
    Animator[] _comboAnimators = default;

    /// <summary>現在の表示しているHpUIの配列</summary>
    Image[] _currentHpUIArray = default;
    /// <summary>表示するイラストの添え字</summary>
    int _currentComboIndex = 0;

    /// <summary>InGameUIの初期化処理</summary>
    /// <param name="maxHp">最大HP</param>
    /// <param name="gameTime">ゲームプレイ時間</param>
    /// <param name="maxIdlePower">最大アイドルパワー</param>
    public void InitializeInGameUI(int maxHp, float gameTime, float maxIdlePower)
    {
        _currentHpUIArray = new Image[maxHp];

        for (var i = 0; i < maxHp; i++)    //HPUIを最大体力分表示する
        {
            HpUIGenerator(i);
        }

        _goalSlider.maxValue = gameTime;          //スライダーの最大値をゲームプレイ時間と同じにする
        //_idolPowerGauge.maxValue = maxIdlePower;   //アイドルパワーゲージ最大値を変更する(Slider時の処理)
    }

    /// <summary>HPUIを生成し配列に追加する </summary>
    /// <param name="index">配列の添え字</param>
    public void HpUIGenerator(int index)
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
    public void UpdateHpUI(int currentHp)
    {
        _currentHpUIArray[currentHp].enabled = false;
    }

    /// <summary> アイドルパワーゲージを増減させる</summary>
    /// <param name="value">増減値(減少させる場合は負数を入れる)</param>
    public void UpdateIdolPowerGauge(float value)
    {
        _idolPowerGauge.fillAmount = value;
    }

    /// <summary>ゴールまでの距離を表示するUIを更新する</summary>
    public void UpdateGoalDistanceUI(float elapsedTime)
    {
        _goalSlider.value = elapsedTime;   //ゴールまでの距離をUIに表示する
    }

    /// <summary>コンボ数を表示するテキストを更新する </summary>
    public void UpdateComboText(int comboCount)
    {
        _comboText.text = comboCount.ToString();
    }

    /// <summary>コンボ用カットインのアニメーションを再生する</summary>
    public void PlayComboAnimation(Sprite sprite)
    {
        //_comboImage.sprite = _comboSprites[_currentComboIndex];     //イラストを変更
        //_currentComboIndex = (_currentComboIndex + 1) % _comboSprites.Length;   //イラストを循環させるため

        _comboImage.sprite = sprite;

        foreach (var anim in _comboAnimators)
        {
            anim.SetTrigger("Play");
        }
    }
}
