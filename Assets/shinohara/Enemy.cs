using UnityEngine;

/// <summary>�G�l�~�[���Ǘ�����N���X </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("�A�j���[�^�[")]
    Animator _anim = default;

    /// <summary>�|���ꂽ���̏��� </summary>
    public void Dead()
    {
        _anim.SetTrigger("Explosion");
    }
}
