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
    [SerializeField, Tooltip("�X�g�[���[��\������L�����o�X")]
    Canvas _storyCanvas = default;
    [SerializeField, Tooltip("�N���W�b�g��\������L�����o�X")]
    Canvas _creditCanvas = default;

    /// <summary>�X�g�[���[�p�A�j���[�^�[ </summary>
    Animator _storyAnimator => _storyCanvas.GetComponent<Animator>();
    /// <summary>�N���W�b�g�p�A�j���[�^�[ </summary>
    Animator _creditAnimator => _creditCanvas.GetComponent<Animator>();

    /// <summary>��Փx�I�����J�n�������ǂ���</summary>
    bool _isChangeScene = false;

    private void Start()
    {
        _fadeController.FadeIn(() =>
        {
            if (!_isChangeScene)
            {
                AudioManager.Instance.PlayVoice(14);
            }
        });
    }

    public void StoryOpen()
    {
        if (_isChangeScene) { return; }

        _storyAnimator.Play("In");
        _storyCanvas.enabled = true;
        AudioManager.Instance.PlaySE(7);
    }

    public void StoryClose()
    {
        if (_isChangeScene) { return; }

        _storyAnimator.Play("Out");
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditOpen()
    {
        if (_isChangeScene) { return; }

        _creditAnimator.Play("In");
        _creditCanvas.enabled = true;
        AudioManager.Instance.PlaySE(7);
    }

    public void CreditClose()
    {
        if (_isChangeScene) { return; }

        _creditAnimator.Play("Out");
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>�V�[����؂�ւ���</summary>
    public void ChangeScene()
    {
        if (_isChangeScene) { return; }

        _isChangeScene = true;
        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //�t�F�[�h���J�n����
    }
}
