using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

/// <summary>���U���g�V�[����UI���Ǘ��E�X�V����N���X</summary>
public class ResultUIController : MonoBehaviour
{
    [SerializeField, Header("�w�i(�L�����N�^�[)")]
    Image _backGround = default;

    [ElementNames(new string[] { "�_", "��", "����", "��" })]
    [SerializeField, Header("�]���ʔw�i(�L�����N�^�[)"), Tooltip("0=�_ 1=�� 2=���� 3=��")]
    Sprite[] _backGroundSprites = default;

    [ElementNames(new string[] { "�]��", "Bad", "Good", "Perfect", "���v�X�R�A" })]
    [SerializeField, Header("���ʂ�\������e�L�X�g"), Tooltip("0=�]��, 1=Bad, 2=Good, 3=Perfect, 4=���v�X�R�A")]
    TextMeshProUGUI[] _resultValueText = default;

    [SerializeField, Header("���U���g�\���ؑ�"), ElementNames(new string[] { "�]��", "�݂�Ȃ̃R�����g" })]
    Transform[] _showResultParent = default;

    [SerializeField, Header("�t�F�[�h�C���֘A"), Tooltip("�{�^���C���[�W"), ElementNames(new string[] { "�]���؂�ւ�", "�X�e�[�W�Z���N�g", "���g���C" })]
    Image[] _fadeImageButton = default;

    [SerializeField, Tooltip("�e�L�X�g"), ElementNames(new string[] { "�]���؂�ւ�", "�X�e�[�W�Z���N�g", "���g���C" })]
    TextMeshProUGUI[] _fadeTextColor = default;

    [SerializeField, Tooltip("�l�̑�������")]
    float _increseTime = 1.0f;

    [SerializeField, Tooltip("UI��\��������܂ł̎���"), ElementNames(new string[] { "�]���\��", "�X�R�A�\��", "�{�^���̃t�F�[�h�C��", "�e�L�X�g�̃t�F�[�h�C��" })]
    float _showResultSpan = 1.0f, _scoreResultSpan = 2.0f, _buttonShowSpan = 1.0f, _textShowSpan = 2.0f;

    /// <summary>�]����ʂ��\������Ă��邩�ǂ���</summary>
    bool _isValue = true;

    /// <summary>���ʂɂ���Ĕw�i��ύX���� </summary>
    /// <param name="result">�v���C����</param>
    public void ChangeResultImage(Result result)
    {
        switch (result)
        {
            case Result.Bad:
                _backGround.sprite = _backGroundSprites[3];
                _resultValueText[0].text = "Result:Bad";
                break;
            case Result.Good:
                _backGround.sprite = _backGroundSprites[2];
                _resultValueText[0].text = "Result:Good";
                break;
            case Result.Perfect:
                _backGround.sprite = _backGroundSprites[1];
                _resultValueText[0].text = "Result:Perfect";
                break;
            case Result.SuperPerfect:
                _backGround.sprite = _backGroundSprites[0];
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
            .OnUpdate(() => _resultValueText[1].text = $"Bad:{result[0]}");
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[2].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[1] = r, result[1], _increseTime)
            .OnUpdate(() => _resultValueText[2].text = $"Good:{result[1]}");
        yield return new WaitForSeconds(_showResultSpan);

        _resultValueText[3].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[2] = r, result[2], _increseTime)
            .OnUpdate(() => _resultValueText[3].text = $"Perfect:{result[2]}");
        yield return new WaitForSeconds(_scoreResultSpan);

        _resultValueText[4].gameObject.SetActive(true);
        DOTween.To(() => firstValue, (r) => result[3] = r, result[3], _increseTime)
             .OnUpdate(() => _resultValueText[4].text = $"Score:{result[3]}");
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
            _isValue = false;
        }
        //����Ă��Ȃ�������
        else if (!_isValue)
        {
            //�]����ʂ��o��
            _showResultParent[0].gameObject.SetActive(true);
            //�݂�Ȃ̃R�����g���\����
            _showResultParent[1].gameObject.SetActive(false);
            _isValue = true;
        }
    }
    /// <summary>��Փx�Z���N�g�V�[���ɖ߂邩���g���C���邩</summary>
    /// <param name="index">�V�[���ԍ�</param>
    public void ReturnModeSelectAndRetry(int index)
    {
        SceneManager.LoadScene(index);
    }
}
