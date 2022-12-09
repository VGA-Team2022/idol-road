using UnityEngine;

/// <summary></summary>
public class EnemySpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("通常ファンのスプライトを管理する変数")]
    Sprite[]_normalEnemys = default;

    [SerializeField , Tooltip("ボス戦でのファンのスプライトを管理する変数")]
    Sprite _bossEnemy = default;

    /// <summary>ファンのスプライトをランダムに変更させる関数</summary>
    public void EnemyRandomMethod(SpriteRenderer fanSpriteRenderer)
    {
        var rand = Random.Range(0, _normalEnemys.Length);
        fanSpriteRenderer.sprite = _normalEnemys[rand];
    }

    /// <summary>ファンのスプライトをボス戦用に変更させる関数</summary>
    public void EnemyBossMethod(SpriteRenderer fanSpriteRenderer) 
    {
        fanSpriteRenderer.sprite = _bossEnemy;
    }
}
