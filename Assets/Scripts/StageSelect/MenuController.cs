using UnityEngine;

/// <summary>メニューUIを操作する為のクラス </summary>
[RequireComponent(typeof(Animator))]
public class MenuController : MonoBehaviour
{
    Animator _anim =>GetComponent<Animator>();

    /// <summary>メニューを表示する </summary>
    public void MenuOpen()
    {
        _anim.Play("Open");
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>メニューを非表示にする </summary>
    public void MenuClose()
    {
        _anim.Play("Close");
        AudioManager.Instance.PlaySE(7);
    }
}
