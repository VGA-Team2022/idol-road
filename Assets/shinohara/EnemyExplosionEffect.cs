using UnityEngine;

/// <summary>�G(�t�@��)�̔����G�t�F�N�g���Ǘ�����N���X </summary>
public class EnemyExplosionEffect : MonoBehaviour
{
    /// <summary>
    /// �A�j���[�V�����Đ��I�����ɌĂ΂�A���g���폜����
    /// </summary>
    public void ThisDestroy()
    {
        Destroy(gameObject);
    }
}
