using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>�X�e�[�W�I���V�[���Ɋւ��鏈�����s���N���X </summary>
public class StageSelectManager : MonoBehaviour
{
    #region�@�ϐ�
    [SerializeField, Header("���̑J�ڐ�̃V�[����")]
    string _nextSceneName = "";
    [SerializeField, Header("�X�e�[�W�Z���N�g�ɂ���ĕω�����C���[�W")]
    Image _stageImage = default;
    [SerializeField, Header("�t�F�[�h�s���N���X")]
    FadeController _fadeController = default;
    [ElementNames(new string[] { "�`���[�g���A��", "�ȒP", "����", "���" })]
    [SerializeField, Header("�e�X�e�[�W�̃C���X�g"), Tooltip("0=�`���[�g���A��, 1=�ȒP, 2=����, 3=���")]
    Sprite[] _stageSprites = default;
    [ElementNames(new string[] {"�ȒP", "����", "���" })]
    [SerializeField , Header("�V�ѕ���\������L�����o�X")]
    Canvas _playUiCanvas = default;
    [SerializeField, Header("�X�^�[�g�{�^���̃A�j���[�^�[")]
    Animator _startAnimator = default;

    /// <summary>�J�ڂ����Ă��邩 true=�J�n���Ă���</summary>
    bool _isTransition = false;

    /// <summary>���ݑI������Ă���{�^�� </summary>
    Button _currentSelectedButton = default;

    /// <summary>�I������Ă���X�e�[�W�I���{�^���̃A�j���[�^�[ </summary>
    Animator _currentButtonAnimator = default;
    #endregion

    void Start()
    {
        _fadeController.FadeIn();
        _stageImage.sprite = _stageSprites[0];              //�����C���X�g��ݒ�
    }

    /// <summary>��������E�V�ѕ���\�����鏈��</summary>
    void PlayUiButton(Button selectButton)
    {
        if (_isTransition) { return; }

        if (selectButton == _currentSelectedButton)
        {
            AudioManager.Instance.PlaySE(7);
            if (!_playUiCanvas.enabled) 
            {
                _playUiCanvas.enabled = true;
            }
        }
        else
        {
            //�X�e�[�W��I������
            _currentSelectedButton = selectButton;
            _stageImage.sprite = _stageSprites[0];
            AudioManager.Instance.PlaySE(32);
        }
    }

    /// <summary>��Փx��I������ </summary>
    /// <param name="index"></param>
    public void LevelSelect(int index)
    {
        LevelManager.Instance.SelectLevel((GameLevel)index);    //���x����ύX
        AudioManager.Instance.PlaySE(32);
    }

    /// <summary>�^�C�g���V�[���ɖ߂�</summary>
    public void TitleChange() 
    {
        if (_isTransition) { return; }

        AudioManager.Instance.PlaySE(7);
        _fadeController.FadeOut(() => SceneManager.LoadScene(0));
        _isTransition = true;
    }

    /// <summary>
    /// �Q�[���V�[���ɑJ�ڂ��鎞�̏��� 
    /// �{�^������Ăяo��
    /// </summary>
    public void TransitionGameScene()
    {
        if (_isTransition) { return; }

        AudioManager.Instance.PlaySE(7);
        _isTransition = true;
        _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));
    }

    /// <summary>
    /// ���x���I���{�^���������ꂽ���̃A�j���[�V�������Đ�����
    /// �{�^������Ăяo��
    /// </summary>
    public void StartSelectLevelAnime(Animator selectAnimator)
    {
        if (_currentButtonAnimator == null)     //���߂ē�Փx���I�����ꂽ���̏���
        {
            _startAnimator.gameObject.SetActive(true);
            _startAnimator.Play("In");

            _currentButtonAnimator = selectAnimator;
            _currentButtonAnimator.Play("Select");
            return;
        }

        _currentButtonAnimator.Play("Deselect");
        _currentButtonAnimator = selectAnimator;
        _currentButtonAnimator.Play("Select");
    }

}
