using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("通常ファンのスプライトを管理する変数")]
    Sprite[]_normalEnemys = default;
    [Tooltip("ファンをランダムに生成させるための変数")]
    int _rand = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>敵のスプライトをランダムに変更させる関数</summary>
    public void EnemyRandomMethod()
    {
        _rand = Random.Range(0, _normalEnemys.Length);
        GetComponent<SpriteRenderer>().sprite = _normalEnemys[_rand];
        Debug.Log(_normalEnemys[_rand]);
    }
}
