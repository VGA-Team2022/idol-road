using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>���j���[UI�𑀍삷��ׂ̃N���X (�A�j���[�V�������Đ�����)</summary>
public class MenuController : MonoBehaviour
{
    [SerializeField, Header("�X�g�[���[�������\��������܂ł̑ҋ@����")]
    float _stroyWaitTime = 2f;
    [SerializeField, Header("�V�ѕ��������\��������܂ł̑ҋ@����")]
    float _tutorialWaitTime = 1f;

    [SerializeField, Header("�X�g�[���[��\������L�����o�X")]
    StoryPrinter _storyPrinter = default;
    [SerializeField, Header("�N���W�b�g��\������L�����o�X")]
    Animator _creditAnimator = default;
    [SerializeField, Header("�`���[�g���A���p�A�j���[�^�[")]
    TutorialManager _tutorialManager = default;
    [SerializeField, Header("���j���[�p�A�j���[�^�[")]
    Animator _menuAnimator = default;
    [SerializeField, Tooltip("�t�F�[�h�s���N���X")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("�ŏ��ɃX�g�[���[�ƗV�ѕ���\�����Ă���Ԃ̔w�i")]
    Image _firstBackGround = default;
    /// <summary>���߂ăQ�[�����v���C�������ǂ��� </summary>
    static bool _firstPlay = true;

    private IEnumerator Start()
    {
        if (!_firstPlay)
        {
            AudioManager.Instance.PlayBGM(14, 0.5f);
            _storyPrinter.CurrentMode = ShowMode.Title;
            _fadeController.FadeIn();
            yield break;
        }

        _firstBackGround.gameObject.SetActive(true);
        yield return new WaitForSeconds(_stroyWaitTime);
        _storyPrinter.StroyOperator(true);

        _storyPrinter.StorySkip += () => StartCoroutine(WaitTutorialUI());      //�X�L�b�v������ǉ�       
    }

    /// <summary>�V�ѕ��������\�������� </summary>
    /// <returns></returns>
    IEnumerator WaitTutorialUI()
    {
        yield return new WaitForSeconds(_tutorialWaitTime);
        _tutorialManager.TutorialOperator(true);
        _tutorialManager.CloseButtonAddListener(() =>
        {
            _firstPlay = false;
            _firstBackGround.gameObject.SetActive(false);
            _fadeController.FadeIn();
        });
    }

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

        AudioManager.Instance.PlaySE(7);
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

        AudioManager.Instance.PlaySE(7);
    }
}
