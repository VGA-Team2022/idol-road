using UnityEngine;

/// <summary>エネミーを管理するクラス </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField, Tooltip("アニメーター")]
    Animator _anim = default;

    /// <summary>倒された時の処理 </summary>
    public void Dead()
    {
        _anim.SetTrigger("Explosion");
    }
}
