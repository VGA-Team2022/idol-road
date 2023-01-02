using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>�X�g�[���[��\������ׂ̃N���X </summary>
public class StoryPrinter : MonoBehaviour
{
    [SerializeField, Header("�e�L�X�g��\������܂łɂ�����b��")]
    float _showTextTime = 2f;
    [SerializeField, Header("���̃e�L�X�g��\������܂ł̑ҋ@����")]
    float _textWaitTime = 2f;
    [SerializeField, Header("�t�F�[�h���s���܂ł̑ҋ@����")]
    float _fadeWaitTime = 2f;

    [SerializeField, Tooltip("�t�F�[�h�s���I�u�W�F�N�g")]
    FadeController _fadeController = default;

    [SerializeField, Tooltip("�\������X�g�[���[�̃e�L�X�g�z��")]
    TMP_Text[] _storyTexts = default;
    /// <summary>�\�������e�L�X�g�̓Y���� </summary>
    int _currnetTextIndex = 0;

    /// <summary>�V�[���J�ڂ��s�������ǂ��� </summary>
    bool _isTransition = false;

    private void OnEnable()
    {
        StartCoroutine(ShowStory());
    }

    /// <summary>�X�g�[���[��\������ </summary>
    IEnumerator ShowStory()
    {
        for (var i = 0; i < _storyTexts.Length; i++)
        {
            yield return new WaitForSeconds(_textWaitTime);
            ShowText();
        }

        yield return new WaitForSeconds(_fadeWaitTime);

        if (!_isTransition)
        {
            _fadeController.FadeOut(() =>
            {
                gameObject.SetActive(false);
                _fadeController.FadeIn();
            });
            _isTransition = true;
        }
    }

    /// <summary>�e�L�X�g���t�F�[�h�����ĕ\������ </summary>
    public void ShowText()
    {
        _storyTexts[_currnetTextIndex].DOFade(1, _showTextTime);
        _currnetTextIndex++;
    }

    /// <summary>
    /// �X�L�b�v�@�\
    /// �{�^������Ăяo��
    /// </summary>
    public void Skip()
    {
        if (_isTransition) { return; }

        _fadeController.FadeOut(() =>
        {
            gameObject.SetActive(false);
            _fadeController.FadeIn();
        });

        _isTransition = true;
    }


}
