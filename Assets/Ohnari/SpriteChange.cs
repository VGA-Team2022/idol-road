using UnityEngine;

/// <summary></summary>
public class SpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("通常ファンのスプライトを管理する変数")]
    Sprite[]_normalEnemys = default;
   
    /// <summary>敵のスプライトをランダムに変更させる関数</summary>
    public void EnemyRandomMethod(SpriteRenderer fanSpriteRenderer)
    {
        var rand = Random.Range(0, _normalEnemys.Length);
        fanSpriteRenderer.sprite = _normalEnemys[rand];
    }
}
