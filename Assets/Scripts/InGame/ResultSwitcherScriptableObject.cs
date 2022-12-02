using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data",menuName ="ScriptableObjects/ResultSwitcherScriptableObject",order =1)]
public class ResultSwitcherScriptableObject : ScriptableObject
{
    [SerializeField, Tooltip("Excellent(良)以上なるためのBad判定の数の許容値")]
    public int _bad_to_Excellent = 0;
    [SerializeField, Tooltip("Perfect(神)に昇格するために必要なPerfectの数")]
    public int _perfect_to_Perfect = 10;
    [SerializeField, Tooltip("Good(普通)からExcellent(良)に昇格するために必要なPerfectの数")]
    public int _perfect_to_Excellent = 10;
}
