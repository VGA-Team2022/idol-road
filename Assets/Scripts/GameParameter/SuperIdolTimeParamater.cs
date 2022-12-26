using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SuperIdolTimeParamater : ScriptableObject
{
    [SerializeField, Header("ゲージがマックスになる回数")]
    private int _gaugeCountMax = 10;
    [SerializeField, Header("スーパーアイドルタイムの持続時間")]
    private float _timeEndSuperIdolTime = 15;
    [SerializeField, Header("成功時のスコア量")]
    private float _successScore = 5000;
    /// <summary>ゲージがマックスになる回数</summary>
    public int GaugeCountMax => _gaugeCountMax;
    /// <summary>スーパーアイドルタイムの持続時間</summary>
    public float TimeEndSuperIdolTime => _timeEndSuperIdolTime;
    /// <summary>成功時のスコア量</summary>
    public float SuccessScore => _successScore;
}
