using UnityEngine;

/// <summary>敵(ファン)の爆発エフェクトを管理するクラス </summary>
public class EnemyExplosionEffect : MonoBehaviour
{
    /// <summary>
    /// アニメーション再生終了時に呼ばれ、自身を削除する
    /// </summary>
    public void ThisDestroy()
    {
        Destroy(gameObject);
    }
}
