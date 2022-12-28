using UnityEngine;

/// <summary>リザルト条件の情報を持つScriptableObjectを作成するクラス</summary>
[CreateAssetMenu]
public class ResultParameter : ScriptableObject
{
    /// <summary>リザルト画面でのファンセリフの数</summary>
    const int FAN_SCRIPTS_SIZE = 5;

    [SerializeField, Header("SuperPerfectになる条件")]
    public int _superPerfectLine;

    [SerializeField, Header("Perfectになる条件")]
    public int _perfectLine;

    [SerializeField, Header("Goodになる条件")]
    public int _goodLine;

    [SerializeField, Header("Bad時に獲得出来るスコア")]
    public int _addBadScoreValue;

    [SerializeField, Header("Good時に獲得出来るスコア")]
    public int _addGoodScoreValue;

    [SerializeField, Header("Perfect時に獲得出来るスコア")]
    public int _addParfectScoreValue;

    [SerializeField, Header("コンボの倍率"), Range(1, 10)]
    public float _comboValue;

    [ElementNames(new string[] { "地雷", "男１", "壁女", "男２", "強欲" })]
    [SerializeField, Header("リザルト画面でのBad時のファンセリフ")]
    public string[] _fanScriptsBad = new string[FAN_SCRIPTS_SIZE];

    [ElementNames(new string[] { "地雷", "男１", "壁女", "男２", "強欲" })]
    [SerializeField, Header("リザルト画面でのGood時のファンセリフ")]
    public string[] _fanScriptsGood = new string[FAN_SCRIPTS_SIZE];

    [ElementNames(new string[] { "地雷", "男１", "壁女", "男２", "強欲" })]
    [SerializeField, Header("リザルト画面でのExcellent時のファンセリフ")]
    public string[] _fanScriptsExcellent = new string[FAN_SCRIPTS_SIZE];

    [ElementNames(new string[] { "地雷", "男１", "壁女", "男２", "強欲" })]
    [SerializeField, Header("リザルト画面でのPerfect時のファンセリフ")]
    public string[] _fanScriptsPerfect = new string[FAN_SCRIPTS_SIZE];
}
