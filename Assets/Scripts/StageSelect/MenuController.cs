using UnityEngine;

/// <summary>���j���[UI�𑀍삷��ׂ̃N���X </summary>
[RequireComponent(typeof(Animator))]
public class MenuController : MonoBehaviour
{
    [SerializeField, Header("�`���[�g���A���p�A�j���[�^�[")]
    Animator _tutorialAnimator = default;

    [SerializeField, Header("�N���W�b�g�p�A�j���[�^�[")]
    Animator _creditAnimator = default;

    /// <summary>���j���[�p�A�j���[�^�[ </summary>
    Animator _menuAnim =>GetComponent<Animator>();

    /// <summary>���j���[UI�𑀍삷��֐� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void MenuOperator(bool flag)
    {
        if (flag)   
        {

            _menuAnim.Play("MenuOpen");          //���j���[��\��
        }
        else
        {
            _menuAnim.Play("MenuClose");        //���j���[���\��
        }

        AudioManager.Instance.PlaySE(7);�@
    }


    /// <summary>�V�ѕ�UI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _tutorialAnimator.Play("TutorialOpen");
            
        }
        else
        {
            _tutorialAnimator.Play("TutorialClose");
           
        }

        AudioManager.Instance.PlaySE(7);�@
    }

    /// <summary>�N���W�b�gUI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void CreditOperator(bool flag)
    {
        if (flag)
        {
            _creditAnimator.Play("CreditOpen");
        }
        else
        {
            _creditAnimator.Play("CreditClose");
        }

        AudioManager.Instance.PlaySE(7);�@
    }
}
