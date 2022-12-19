using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>�X�e�[�W�I���V�[���Ɋւ��鏈�����s���N���X </summary>
public class StageSelectController : MonoBehaviour
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
    [SerializeField, Header("�X�e�[�W�Z���N�g�̃{�^��"), Tooltip("1=�ȒP, 2=����, 3=���")]
    Button[] _stageSelectButtons = default;
    [SerializeField, Header("�V�ѕ���\������{�^��")]
    Button _playingUIButton = default;
    [SerializeField , Header("�V�ѕ���\������L�����o�X")]
    Canvas _playUiCanvas = default;

    /// <summary>���ݑI������Ă���{�^�� </summary>
    Button _currentSelectedButton = default;
    #endregion

    void Start()
    {
        _fadeController.FadeIn();

        for (var i = 0; i < _stageSelectButtons.Length; i++)
        {
            var button = _stageSelectButtons[i];    //��x���[�J���ύX�ɑ�����Ȃ��ƃG���[���o��
            var index = i;
            _stageSelectButtons[i].onClick.AddListener(() => TransitionGameScene(button, index));
        }

        _currentSelectedButton = _playingUIButton;    //�����{�^����ݒ�
        _playingUIButton.onClick.AddListener(() => PlayUiButton(_playingUIButton));
        _stageImage.sprite = _stageSprites[0];              //�����C���X�g��ݒ�
    }

    /// <summary>�Q�[���V�[���ɑJ�ڂ��鎞�̏��� </summary>
    void TransitionGameScene(Button selectButton, int index)
    {
        if (selectButton == _currentSelectedButton)     //�I���Q�[���V�[���ɑJ�ڂ���
        {
            AudioManager.Instance.PlaySE(7);
            _fadeController.FadeOut(() => SceneManager.LoadScene(_nextSceneName));
        }
        else
        {
            if (_playUiCanvas.enabled)
            {
                _playUiCanvas.enabled = false;
            }

            //�X�e�[�W��I������
            _currentSelectedButton = selectButton;
            _stageImage.sprite = _stageSprites[index];
            LevelManager.Instance.SelectLevel((GameLevel)index);    //���x����ύX
            AudioManager.Instance.PlaySE(32);
        }
    }

    void PlayUiButton(Button selectButton)
    {
        if (selectButton == _currentSelectedButton)     //�I���Q�[���V�[���ɑJ�ڂ���
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
}
