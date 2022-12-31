using UnityEngine;

/// <summary>���j���[UI�𑀍삷��ׂ̃N���X </summary>
public class MenuController : MonoBehaviour
{
    /// <summary>�eUI��\������ׂ̃A�j���[�V������ </summary>
    const string OPEN_UI_ANIME_NAME = "Open";

    /// <summary>�eUI���\������ׂ̃A�j���[�V������ </summary>
    const string CLOSE_UI_ANIME_NAME = "Close";

    [SerializeField, Header("�`���[�g���A���p�A�j���[�^�[")]
    Animator _tutorialAnimator = default;

    [SerializeField, Header("�N���W�b�g�p�A�j���[�^�[")]
    Animator _creditAnimator = default;

    [SerializeField, Header("���j���[�p�A�j���[�^�[")]
    Animator _menuAnim = default;
 
    /// <summary>���j���[UI�𑀍삷��֐� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void MenuOperator(bool flag)
    {
        if (flag)   
        {

            _menuAnim.Play(OPEN_UI_ANIME_NAME);          //���j���[��\��
        }
        else
        {
            _menuAnim.Play(CLOSE_UI_ANIME_NAME);        //���j���[���\��
        }

        AudioManager.Instance.PlaySE(7);�@
    }


    /// <summary>�V�ѕ�UI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _tutorialAnimator.Play(OPEN_UI_ANIME_NAME);
            
        }
        else
        {
            _tutorialAnimator.Play(CLOSE_UI_ANIME_NAME);
           
        }

        AudioManager.Instance.PlaySE(7);�@
    }

    /// <summary>�N���W�b�gUI�𑀍삷�� </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void CreditOperator(bool flag)
    {
        if (flag)
        {
            _creditAnimator.Play(OPEN_UI_ANIME_NAME);
        }
        else
        {
            _creditAnimator.Play(CLOSE_UI_ANIME_NAME);
        }

        AudioManager.Instance.PlaySE(7);�@
    }
}
