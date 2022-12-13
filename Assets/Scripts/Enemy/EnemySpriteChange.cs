using UnityEngine;

/// <summary>敵のイラストを変更する</summary>
public class EnemySpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("通常ファンのスプライトを管理する変数")]
    Sprite[]_normalEnemySprites = default;

    [SerializeField , Tooltip("ボス戦でのファンのスプライトを管理する変数")]
    Sprite _bossEnemy = default;

    /// <summary>ファンのスプライトをランダムに変更させる関数</summary>
    public void EnemyRandomMethod(SpriteRenderer fanSpriteRenderer)
    {
        if (_normalEnemySprites.Length <= 0) { return; }

        var rand = Random.Range(0, _normalEnemySprites.Length);
        fanSpriteRenderer.sprite = _normalEnemySprites[rand];
    }

    /// <summary>ファンのスプライトをボス戦用に変更させる関数</summary>
    public void EnemyBossMethod(SpriteRenderer fanSpriteRenderer) 
    {
        fanSpriteRenderer.sprite = _bossEnemy;
    }
}
