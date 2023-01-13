using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>�^�C�g���V�[���̏������Ǘ�����N���X </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("�t�F�[�h���s���N���X")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("�J�ڐ�̃V�[����")]
    string _nextSceneName = "";
    [SerializeField, Tooltip("�X�g�[���[�p�A�j���[�^�[")]
    Animator _storyAnimator = default;
    [SerializeField, Tooltip("�N���W�b�g�p�A�j���[�^�[")]
    Animator _creditAnimator = default;

    /// <summary>��Փx�I�����J�n�������ǂ���</summary>
    bool _isChangeScene = false;

    /// <summary>�|�b�v�A�b�v��\�������ǂ��� </summary>
    bool _isPopup = false;

    private void Start()
    {
        _fadeController.FadeIn(() =>
        {
            if (!_isChangeScene)
            {
                AudioManager.Instance.PlaySoundAfterExecution(Sources.VOICE, 14, () => AudioManager.Instance.PlayBGM(14, 0.5f));
            }
        });
    }

    public void StoryOpen()
    {
        if (_isChangeScene) { return; }

        _isPopup = true;
        _storyAnimator.Play("Open");
        AudioManager.Instance.PlaySE(7);
    }

    public void StoryClose()
    {
        if (_isChangeScene) { return; }

        _isPopup = false;
        _storyAnimator.Play("Close");
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditOpen()
    {
        if (_isChangeScene) { return; }

        _isPopup = true;
        _creditAnimator.Play("Open");
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditClose()
    {
        if (_isChangeScene) { return; }

        _isPopup = false;
        _creditAnimator.Play("Close");
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>�V�[����؂�ւ���</summary>
    public void ChangeScene()
    {
        if (_isPopup || _isChangeScene) { return; }

        _isChangeScene = true;
        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //�t�F�[�h���J�n����
    }

    /// <summary�A�v���P�[�V�����𗎂Ƃ� </summary>
    public void AppClose()
    {
        Application.Quit();
    }
}
