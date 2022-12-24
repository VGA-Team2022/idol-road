using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

/// <summary>���U���g�V�[����UI���Ǘ��E�X�V����N���X</summary>
public class ResultUIController : MonoBehaviour
{
    #region
    [SerializeField, Header("ResultManager")]
    ResultManager _resultManager = default;
   
    [SerializeField, Header("�e�X�R�A�̃e�L�X�g��\��������܂Ŏ���")]
    float _showResultSpan = 1.0f;
    [SerializeField, Header("�X�R�A��\������܂ł̎���")]
    float _scoreResultSpan = 2.0f;
    [SerializeField, Header("�{�^����\������܂ł̎���")]
    float _buttonShowSpan = 1.0f;
    [SerializeField, Header("�e�L�X�g�̃t�F�[�h�C���ɂ����鎞��")]
    float _textShowSpan = 2.0f;
    [SerializeField, Header("�e�L�X�g�̃A�j���[�V�����ɂ����鎞��")]
    float _increseTime = 1.0f;


    [SerializeField, Header("�w�i(�L�����N�^�[)")]
    Image _backGround = default;

    [SerializeField, Header("�{�^����Image")]
    Image _buttonImage = default;

    [SerializeField, Header("�y�[�W��Text")]
    TextMeshProUGUI _pageText = default;


    [ElementNames(new string[] { "�_", "��", "����", "��" })]
    [SerializeField, Header("�]���ʔw�i(�L�����N�^�[)"), Tooltip("0=�_ 1=�� 2=���� 3=��")]
    Sprite[] _backGroundSprites = default;

    [ElementNames(new string[] { "�]��", "Bad", "Good", "Perfect", "���v�X�R�A" })]
    [SerializeField, Header("���ʂ�\������e�L�X�g"), Tooltip("0=�]��, 1=Bad, 2=Good, 3=Perfect, 4=���v�X�R�A")]
    TextMeshProUGUI[] _resultValueText = default;

    [SerializeField, Header("���U���g�\���ؑ�"), ElementNames(new string[] { "�]��", "�݂�Ȃ̃R�����g" })]
    Transform[] _showResultParent = default;

    [SerializeField, Header("�{�^���\���ؑ�"), ElementNames(new string[] { "�]��", "�݂�Ȃ̃R�����g" })]
    Sprite[] _buttonSprites = default;

    [SerializeField, Header("���ږ��ؑ�"), ElementNames(new string[] { "�]��", "�݂�Ȃ̃R�����g" })]
    string[] _pageName = default;


    [SerializeField, Header("�t�F�[�h�C���֘A"), Tooltip("�{�^���C���[�W"), ElementNames(new string[] { "�]���؂�ւ�", "�X�e�[�W�Z���N�g", "���g���C" })]
    Image[] _fadeImageButton = default;

    [SerializeField, Tooltip("�e�L�X�g"), ElementNames(new string[] { "�]���؂�ւ�", "�X�e�[�W�Z���N�g", "���g���C" })]
    TextMeshProUGUI[] _fadeTextColor = default;

    [SerializeField, Tooltip("�t�@���̃R�����g��Text"), ElementNames(new string[] { "�t�@��1", "�t�@��2", "�t�@��3", "�t�@��4", "�t�@��5" })]
    TextMeshProUGUI[] _fanCommentTexts = default;

    /// <summary>�]����ʂ��\������Ă��邩�ǂ���</summary>
    bool _isValue = true;

    /// <summary>�J�ڂ����Ă��邩 true=�J�n���Ă���</summary>
    bool _isTransition = false;
    #endregion
    public void Start()
    {
        ReflectFansComment();
        SetCommonUI(0);
    }

    /// <summary>���ʂɂ���Ĕw�i��ύX���� </summary>
    /// <param name="result">�v���C����</param>
    public void ChangeResultImage(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _backGround.sprite = _backGroundSprites[3];
                _resultValueText[0].text = "Bad";
                break;
            case Result.Good:
                _backGround.sprite = _backGroundSprites[2];
                _resultValueText[0].text = "Good";
                break;
            case Result.Perfect:
                _backGround.sprite = _backGroundSprites[1];
                _resultValueText[0].text = "Excellent!!";
                break;
            case Result.SuperPerfect:
                _backGround.sprite = _backGroundSprites[0];
                _resultValueText[0].text = "GOD!!";
                break;
        }
    }
    /// <summary>���ʂ̃e�L�X�g��\������֐�</summary>
    /// <param name="result">0=bad 1=good 2=perfect 4=score</param>
    public IEnumerator ShowResult(int[] result)
    {
        int firstValue = 0;

        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[0].gameObject.SetActive(true);

        yield return new WaitForSeconds(_showResultSpan);
        _resultValueText[1].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[0] = r, result[0], _increseTime)
            .OnUpdate(() => _resultValueText[1].text = result[0].ToString());
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[2].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[1] = r, result[1], _increseTime)
            .OnUpdate(() => _resultValueText[2].text = result[1].ToString());
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[3].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[2] = r, result[2], _increseTime)
            .OnUpdate(() => _resultValueText[3].text = result[2].ToString());
        yield return new WaitForSeconds(_scoreResultSpan);

        _resultValueText[4].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[3] = r, result[3], _increseTime)
             .OnUpdate(() => _resultValueText[4].text = result[3].ToString());
        yield return new WaitForSeconds(_scoreResultSpan);

        for (int i = 0; i < _fadeImageButton.Length; i++)
        {
            _fadeImageButton[i].gameObject.SetActive(true);
            _fadeImageButton[i].DOFade(1.0f, _buttonShowSpan);
            _fadeTextColor[i].DOFade(1.0f, _textShowSpan);
        }

        yield return null;
    }
    /// <summary>���U���gUI��؂�ւ���</summary>
    public void SwitchResultUi()
    {
        //�{�^�����������Ƃ���Value���\������Ă�����
        if (_isValue)
        {
            //�]����ʉB��
            _showResultParent[0].gameObject.SetActive(false);
            //�݂�Ȃ̃R�����g��\��
            _showResultParent[1].gameObject.SetActive(true);
            //����UI�̐؂�ւ�
            SetCommonUI(1);
            _isValue = false;

        }
        //����Ă��Ȃ�������
        else if (!_isValue)
        {
            //�]����ʂ��o��
            _showResultParent[0].gameObject.SetActive(true);
            //�݂�Ȃ̃R�����g���\����
            _showResultParent[1].gameObject.SetActive(false);
            //����UI�̐؂�ւ�
            SetCommonUI(0);

            _isValue = true;
        }

        AudioManager.Instance.PlaySE(7);
    }
    /// <summary>��Փx�Z���N�g�V�[���ɖ߂邩���g���C���邩</summary>
    /// <param name="index">�V�[���ԍ�</param>
    public void ReturnModeSelectAndRetry(int index)
    {
        if (_isTransition) { return; }

        _isTransition = true;
        _resultManager.FadeController.FadeOut(() => { SceneManager.LoadScene(index); });

        if (_resultManager.CurrentResult == Result.Bad)
        {
            AudioManager.Instance.PlayVoice(16);
        }

        AudioManager.Instance.PlaySE(7);
    }
    /// <summary>�R�����g�𔽉f������</summary>
    public void ReflectFansComment()
    {
        for (int i = 0; i < LevelManager.Instance.CurrentLevel.Result._fanScripts.Length; i++)
        {
            _fanCommentTexts[i].text = LevelManager.Instance.CurrentLevel.Result._fanScripts[i];
        }
    }
    public void SetCommonUI(int num)
    {
        _buttonImage.sprite = _buttonSprites[num];
        _pageText.text = _pageName[num];
    }
}
