using UnityEngine;

/// <summary></summary>
public class SpriteChange : MonoBehaviour
{
    [SerializeField, Tooltip("�ʏ�t�@���̃X�v���C�g���Ǘ�����ϐ�")]
    Sprite[]_normalEnemys = default;
   
    /// <summary>�G�̃X�v���C�g�������_���ɕύX������֐�</summary>
    public void EnemyRandomMethod(SpriteRenderer fanSpriteRenderer)
    {
        var rand = Random.Range(0, _normalEnemys.Length);
        fanSpriteRenderer.sprite = _normalEnemys[rand];
    }
}
