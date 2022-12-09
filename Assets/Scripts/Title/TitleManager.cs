using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>�^�C�g���V�[���̏������Ǘ�����N���X </summary>
public class TitleManager : MonoBehaviour
{
    [SerializeField, Tooltip("�t�F�[�h���s���N���X")] 
    FadeController _fadeController = default;
    [SerializeField, Tooltip("�J�ڐ�̃V�[����")]
    string _nextSceneName = "";

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))    //�^�b�v
        {
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));   //�t�F�[�h���J�n����
        }
    }
}
