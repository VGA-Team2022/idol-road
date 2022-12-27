using UnityEngine;

/// <summary>���j���[UI�𑀍삷��ׂ̃N���X </summary>
[RequireComponent(typeof(Animator))]
public class MenuController : MonoBehaviour
{
    Animator _anim =>GetComponent<Animator>();

    /// <summary>���j���[��\������ </summary>
    public void MenuOpen()
    {
        _anim.Play("Open");
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>���j���[���\���ɂ��� </summary>
    public void MenuClose()
    {
        _anim.Play("Close");
        AudioManager.Instance.PlaySE(7);
    }
}
