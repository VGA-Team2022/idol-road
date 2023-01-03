using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>�X�g�[���[��\������ׂ̃N���X </summary>
[RequireComponent(typeof(Animator))]
public class StoryPrinter : MonoBehaviour
{
    [SerializeField, Header("�e�L�X�g��\������܂łɂ�����b��")]
    float _showTextTime = 2f;
    [SerializeField, Header("���̃e�L�X�g��\������܂ł̑ҋ@����")]
    float _textWaitTime = 2f;
    [SerializeField, Header("�t�F�[�h���s���܂ł̑ҋ@����")]
    float _fadeWaitTime = 2f;
    [SerializeField, Tooltip("�\������X�g�[���[�̃e�L�X�g�z��")]
    TMP_Text[] _storyTexts = default;
    [SerializeField, Tooltip("�L�����o�X�����ׂ̃{�^��")]
    Button _closeButton = default;

    /// <summary>�\�������e�L�X�g�̓Y���� </summary>
    int _currnetTextIndex = 0;
    /// <summary>�X�g�[���[�\���̃R���[�`�����I��������ׂ̕ϐ� </summary>
    IEnumerator _showStroyEnumerator = default;

    Animator _anim => GetComponent<Animator>();
    TMP_Text _closeButtonText => _closeButton.transform.GetChild(0).GetComponent<TMP_Text>();

    /// <summary>
    /// �A�j���[�V�����������l�����Z�b�g����
    /// �ēx�A�j���[�V�����������
    /// </summary>
    void ResetValue()
    {
        Array.ForEach(_storyTexts, s => s.alpha = 0);
        _closeButton.gameObject.SetActive(false);
        _closeButtonText.alpha = 0;
        _closeButton.image.color = new Color(1, 1, 1, 0);
        _currnetTextIndex = 0;
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

        _closeButton.gameObject.SetActive(true);
        _closeButton.image.DOFade(1, 2);
        _closeButtonText.DOFade(1, 2);
    }

    /// <summary>�e�L�X�g���t�F�[�h�����ĕ\������ </summary>
    public void ShowText()
    {
        _storyTexts[_currnetTextIndex].DOFade(1, _showTextTime);
        _currnetTextIndex++;
    }

    /// <summary>
    /// �X�g�[���[UI�𑀍삷��
    /// �{�^������Ăяo��
    /// </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void StroyOperator(bool flag)
    {
        if (flag)
        {
            _anim.Play("Open");
            _showStroyEnumerator = ShowStory();
            StartCoroutine(_showStroyEnumerator);
        }
        else
        {
            _anim.Play("Close");
            ResetValue();
        }

        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>
    /// ���ׂẴe�L�X�g����C�ɕ\������
    /// �{�^������Ăяo��
    /// </summary>
    public void Skip()
    {
        StopCoroutine(_showStroyEnumerator);
        _closeButton.gameObject.SetActive(true);
        Array.ForEach(_storyTexts, s => s.alpha = 1);
        _closeButtonText.alpha = 1;
        _closeButton.image.color = new Color(1, 1, 1, 1);
    }

    /// <summary>����{�^�������������Ɏ��s������������ǉ�����֐� </summary>
    /// <param name="action"></param>
    public void CloseButtonAddListener(UnityAction action)
    {
        _closeButton.onClick.AddListener(action);
        _closeButton.onClick.AddListener(() => _closeButton.onClick.RemoveAllListeners());  //��x�������s������ׂɁA�{�^���������ꂽ��o�^����Ă��鏈����S�č폜����
    }
}
