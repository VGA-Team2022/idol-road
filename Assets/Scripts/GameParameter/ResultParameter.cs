using UnityEngine;

/// <summary>リザルト条件の情報を持つScriptableObjectを作成するクラス</summary>
[CreateAssetMenu]
public class ResultParameter : ScriptableObject
{
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
}
