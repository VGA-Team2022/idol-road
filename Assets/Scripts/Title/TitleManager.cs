using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�^�C�g���V�[���̏������Ǘ�����N���X </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("�t�F�[�h���s���N���X")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("�J�ڐ�̃V�[����")]
    string _nextSceneName = "";

    /// <summary>�|�b�v�A�b�v���\������Ă��邩�@true=�\��</summary>
    bool _isPopUp = false;
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

    /// <summary>
    /// �X�g�[���[��N���W�b�g�̃|�b�v�A�b�v���J��
    /// �{�^���ŌĂяo��
    /// </summary>
    public void OpenPopUp(GameObject popup)
    {
        popup.SetActive(true);
        _isPopUp = true;
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>
    /// �X�g�[���[��N���W�b�g�̃|�b�v�A�b�v����� 
    /// �{�^���ŌĂяo��
    /// </summary>
    public void ClosePopUp(GameObject popup)
    {
        popup.SetActive(false);
        _isPopUp = false;
        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>�V�[����؂�ւ���</summary>
    public void ChangeScene()
    {
        if (_isPopUp) { return; }

        _isChangeScene = true;
        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //�t�F�[�h���J�n����
    }
}
