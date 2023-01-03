using UnityEngine;

/// <summary>���j���[UI�𑀍삷��ׂ̃N���X (�A�j���[�V�������Đ�����)</summary>
public class MenuController : MonoBehaviour
{
    [SerializeField, Header("�X�g�[���[��\������L�����o�X")]
    Animator _storyAnimator = default;
    [SerializeField, Header("�N���W�b�g��\������L�����o�X")]
    Animator _creditAnimator = default;
    [SerializeField, Header("�`���[�g���A���p�A�j���[�^�[")]
    Animator _tutorialAnimator = default;
    [SerializeField, Header("���j���[�p�A�j���[�^�[")]
    Animator _menuAnimator = default;

    ///====�ȉ��֐��̓{�^������Ăяo��====

    /// <summary>���j���[UI�𑀍삷��֐� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void MenuOperator(bool flag)
    {
        if (flag)   
        {

            _menuAnimator.Play("Open");          //���j���[��\��
        }
        else
        {
            _menuAnimator.Play("Close");        //���j���[���\��
        }

        AudioManager.Instance.PlaySE(7);�@
    }

    /// <summary>�V�ѕ�UI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _tutorialAnimator.Play("Open");
            
        }
        else
        {
            _tutorialAnimator.Play("Close");
           
        }

        AudioManager.Instance.PlaySE(7);�@
    }

    /// <summary>�N���W�b�gUI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void CreditOperator(bool flag)
    {
        if (flag)
        {
           _creditAnimator.Play("Open");
        }
        else
        {
            _creditAnimator.Play("Close");
        }

        AudioManager.Instance.PlaySE(7);�@
    }

    /// <summary>�X�g�[���[UI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void StroyOperator(bool flag)
    {
        if (flag)
        {
            _storyAnimator.Play("Open");
        }
        else
        {
            _storyAnimator.Play("Close");
        }

        AudioManager.Instance.PlaySE(7);
    }
}
