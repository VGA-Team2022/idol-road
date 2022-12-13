using UnityEngine;

/// <summary>�G�̃C���X�g��ύX����</summary>
public class EnemySpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("�ʏ�t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite[]_normalEnemySprites = default;

    [SerializeField , Tooltip("�{�X��ł̃t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite _bossEnemy = default;

    /// <summary>�t�@���̃X�v���C�g�������_���ɕύX������֐�</summary>
    public void EnemyRandomMethod(SpriteRenderer fanSpriteRenderer)
    {
        if (_normalEnemySprites.Length <= 0) { return; }

        var rand = Random.Range(0, _normalEnemySprites.Length);
        fanSpriteRenderer.sprite = _normalEnemySprites[rand];
    }

    /// <summary>�t�@���̃X�v���C�g���{�X��p�ɕύX������֐�</summary>
    public void EnemyBossMethod(SpriteRenderer fanSpriteRenderer) 
    {
        fanSpriteRenderer.sprite = _bossEnemy;
    }
}
