using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : MonoBehaviour
{
    [SerializeField, Tooltip("ファンが動く方向"),Header("ファン関係")]
    Vector3 _enemySpped;
    [SerializeField, Tooltip("リズム判定をするための時間(デバッグ用)"), Header("リズム関係")]
    float _time = default;
    [SerializeField, Tooltip("リズム判定の秒数")]
    float _perfect, _good, _bad , _out;

    /// <summary>とりあえずボタンで判定確認できるようにするもの</summary>
    bool _knockDownEnemy;

    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_enemySpped);　//ファンを前に動かす（仮）

        _knockDownEnemy = false;
    }
    /// <summary> リズム判定するもの</summary>
    void Update()
    {
        if(_knockDownEnemy) 
        {
            _time -= Time.deltaTime;
            if(_time <= _out)
            {
                Debug.Log("out");
            }
            else if (_time <= _perfect)
            {
                Debug.Log($"perfect { _time:F1}");
            }
            else if(_time <= _good)
            {
                Debug.Log($"good { _time:F1}");
            }
            else if(_time <= _bad)
            {
                Debug.Log($"bad { _time:F1}");
            }
        }
    }

    public void DebugOnBottom()
    {
        _knockDownEnemy = true;
    }
}
