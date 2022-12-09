using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectController : MonoBehaviour
{
    [SerializeField, Header("ステージセレクトのボタン"), ElementNames(new string[] { "チュートリアル", "簡単", "普通", "難しい" })]
    Button[] _stageSelectButton = default;
    [SerializeField,Header("ステージセレクトによって変化するイメージ")]
    Image _selectColor = default;
    [SerializeField]
    FadeController _fadeController = default;
    void Start()
    {
        GameStateEndMethod();
        //チュートリアルを押した時
        _stageSelectButton[0].onClick.AddListener(() => _selectColor.color = Color.red);
        //簡単を押した時
        _stageSelectButton[1].onClick.AddListener(() => _selectColor.color = Color.blue);
        //普通を押した時
        _stageSelectButton[2].onClick.AddListener(() => _selectColor.color = Color.yellow);
        //難しいを押した時
        _stageSelectButton[3].onClick.AddListener(() => _selectColor.color = Color.green);
    }
    /// <summary>タイトルからのシーン遷移が終わったときに呼び出される関数</summary>
    public void GameStateEndMethod()
    {
        _fadeController.FadeOut(() => _fadeController.gameObject.SetActive(true));
    }
}
