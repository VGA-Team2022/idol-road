using UnityEngine;

/// <summary></summary>
public class EnemySpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("�ʏ�t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite[]_normalEnemys = default;

    [SerializeField , Tooltip("�{�X��ł̃t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite _bossEnemy = default;

    /// <summary>�t�@���̃X�v���C�g�������_���ɕύX������֐�</summary>
    public void EnemyRandomMethod(SpriteRenderer fanSpriteRenderer)
    {
        var rand = Random.Range(0, _normalEnemys.Length);
        fanSpriteRenderer.sprite = _normalEnemys[rand];
    }

    /// <summary>�t�@���̃X�v���C�g���{�X��p�ɕύX������֐�</summary>
    public void EnemyBossMethod(SpriteRenderer fanSpriteRenderer) 
    {
        fanSpriteRenderer.sprite = _bossEnemy;
    }
}
