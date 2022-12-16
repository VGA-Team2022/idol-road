using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�^�C�g���V�[���̏������Ǘ�����N���X </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("�t�F�[�h���s���N���X")]
    FadeController _fadeController = default;
    [SerializeField, Tooltip("Story��\������UI")]
    Canvas _storyCanvas = default;
    [SerializeField, Tooltip("�J�ڐ�̃V�[����")]
    string _nextSceneName = "";
    /// <summary>���͂��󂯎�邩�ǂ��� </summary>
    bool _isInput = false;
    /// <summary>�|�b�v�A�b�v���\������Ă��邩�@true=�\��</summary>
    bool _isPopUp = false;

    private void Start()
    {
        _fadeController.FadeIn(() => _isInput = true);
    }

    /// <summary>
    /// �X�g�[���[��N���W�b�g�̃|�b�v�A�b�v���J��
    /// �{�^���ŌĂяo��
    /// </summary>
    public void OpenPopUp(GameObject popup)
    {
        popup.SetActive(true);
        _isPopUp = true;
    }

    /// <summary>
    /// �X�g�[���[��N���W�b�g�̃|�b�v�A�b�v����� 
    /// �{�^���ŌĂяo��
    /// </summary>
    public void ClosePopUp(GameObject popup)
    {
        popup.SetActive(false);
        _isPopUp = false;
    }

    /// <summary>�V�[����؂�ւ���</summary>
    public void ChangeScene()
    {
        if (_isPopUp  || !_isInput) { return; }
        
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //�t�F�[�h���J�n����
    }
}
