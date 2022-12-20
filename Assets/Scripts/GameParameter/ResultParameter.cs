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

    [ElementNames(new string[] {"1つ目", "2つ目", "3つ目" , "4つ目" , "5つ目" })]
    [SerializeField, Header("リザルト画面でのファンセリフ")]
    public string[] _fanScripts = new string[FAN_SCRIPTS_SIZE];
}
