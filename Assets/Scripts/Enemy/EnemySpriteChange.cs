using System;
using UnityEngine;

/// <summary>敵のイラストを変更する</summary>
[RequireComponent(typeof(SpriteRenderer))]
public class EnemySpriteChange : MonoBehaviour
{
    [ElementNames(new string[] { "地雷", "JK", "イケメン", "メガネ" })]
    [SerializeField, Tooltip("通常ファンのスプライトを管理する変数")]
    Sprite[] _nomalEnemySprites = default;
    [SerializeField, Tooltip("ボス戦でのファンのスプライトを管理する変数")]
    Sprite _bossEnemy = default;

    [ElementNames(new string[] { "地雷", "JK", "イケメン", "メガネ", "壁２女", "壁２男", "壁３女", "壁３男", "強欲" })]
    [Tooltip("0.地雷, 1.JK, 2.イケメン, 3.メガネ, 4.壁2男, 5.壁2女, 6.壁3男, 7.壁3女, 8.強欲")]
    [SerializeField, Header("反転用イラスト")]
    Sprite[] _reversalSprites = default;

    /// <summary>sprite表示用</summary>
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();

    /// <summary>通常ファンのイラストを切り替える</summary>
    public EnemySprites ChangeNomalEnemySprite()
    {
        var rand = UnityEngine.Random.Range(0, _nomalEnemySprites.Length);
        _spriteRenderer.sprite = _nomalEnemySprites[rand];

        return (EnemySprites)rand;
    }

    /// <summary>ファンのスプライトをボス戦用に変更させる関数</summary>
    public void EnemyBossMethod()
    {
        _spriteRenderer.sprite = _bossEnemy;
    }

    /// <summary>スポーンポイントが左側であればイラストを反転させる </summary>
    public void ReverseSprite(IState state)
    {
        if (state is BossTime)
        {
            _spriteRenderer.sprite = _reversalSprites[(int)EnemySprites.Boss];
            return;
        }

        foreach (EnemySprites type in Enum.GetValues(typeof(EnemySprites)))
        {
            if (ChangeReverseSprite(type))  //反転したらループからぬける
            {
                break;
            }
        }
    }

    /// <summary>自分のイラストを確認し、合っているイラスト（反転）に切り替える </summary>
    /// <param name="type">敵の種類</param>
    /// <returns>true=成功 false=失敗</returns>
    bool ChangeReverseSprite(EnemySprites type)
    {
        if (type.ToString() == _spriteRenderer.sprite.name)
        {
            _spriteRenderer.sprite = _reversalSprites[(int)type];
            return true;
        }

        return false;
    }
}

/// <summary>全ての敵のイラスト</summary>
public enum EnemySprites
{
    /// <summary>地雷ファン </summary>
    Mine = 0,
    /// <summary>JKファン</summary>
    JK = 1,
    /// <summary>イケメンファン </summary>
    Handsome = 2,
    /// <summary>メガネファン </summary>
    Glasses = 3,
    /// <summary>壁2男 </summary>
    Wall2Man = 4,
    /// <summary>壁2女 </summary>
    Wall2Woman = 5,
    /// <summary>壁3男 </summary>
    Wall3Man = 6,
    /// <summary>壁3女 </summary>
    Wall3Woman = 7,
    /// <summary>強欲 </summary>
    Boss = 8,
}
