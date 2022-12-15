using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�^�C�g���V�[���̏������Ǘ�����N���X </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("�t�F�[�h���s���N���X")] 
    FadeController _fadeController = default;
    [SerializeField, Tooltip("StoryCanvas�������ꏊ")]
    GameObject _canvas = default;
    [SerializeField, Tooltip("�J�ڐ�̃V�[����")]
    string _nextSceneName = "";
    /// <summary>���͂��󂯎�邩�ǂ��� </summary>
    bool _isInput = false;

    bool _isPopUp;

    private void Start()
    {
        _fadeController.FadeIn(() => _isInput = true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _isInput && _isPopUp == true)    //�^�b�v
        {
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //�t�F�[�h���J�n����
        }

        if (Input.GetMouseButtonDown(0) && _isPopUp == false)
        {
            _canvas.SetActive(false);
            _isPopUp = true;
        }
    }

    public void OnButtomDown()
    {
        _canvas.SetActive(true);
        _isPopUp = false;
    }
}
