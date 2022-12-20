using UnityEngine;

/// <summary>敵のイラストを変更する</summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpriteChange : MonoBehaviour
{
    [ElementNames(new string[] {"地雷", "JK", "イケメン", "メガネ"})]
    [SerializeField, Tooltip("通常ファンのスプライトを管理する変数")]
    Sprite[] _nomalEnemySprites = default;
    [SerializeField, Tooltip("ボス戦でのファンのスプライトを管理する変数")]
    Sprite _bossEnemy = default;

    /// <summary>sprite表示用</summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    /// <summary>通常ファンのイラストを切り替える</summary>
    public EnemyNomalSprites ChangeNomalEnemySprite()
    {
        var rand = Random.Range(0, _nomalEnemySprites.Length);
        _spriteRenderer.sprite = _nomalEnemySprites[rand];

        return (EnemyNomalSprites)rand;
    }

    /// <summary>ファンのスプライトをボス戦用に変更させる関数</summary>
    public void EnemyBossMethod()
    {
        _spriteRenderer.sprite = _bossEnemy;
    }
}

/// <summary>敵のイラスト(通常ファン)</summary>
public enum EnemyNomalSprites
{
    /// <summary>地雷ファン </summary>
    Mine = 0,
    /// <summary>JKファン</summary>
    JK = 1,
    /// <summary>イケメンファン </summary>
    Handsome = 2,
    /// <summary>メガネファン </summary>
    Glasses = 3,
}
