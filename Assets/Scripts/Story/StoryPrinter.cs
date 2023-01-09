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
    [SerializeField, Header("�\�����[�h")]
    ShowMode _currentMode = ShowMode.Title;
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
    [SerializeField, Tooltip("�X�L�b�v����{�^��")]
    Button _skipButton = default;
    [SerializeField, Tooltip("�����\�����Ɏg�p����Next�e�L�X�g")]
    TMP_Text _nextText = default;

    /// <summary>�\�������e�L�X�g�̓Y���� </summary>
    int _currnetTextIndex = 0;
    /// <summary>���o���I���������ǂ��� </summary>
    bool _endAnimation = false;
    /// <summary>�X�g�[���[�\���̃R���[�`�����I��������ׂ̕ϐ� </summary>
    IEnumerator _showStroyEnumerator = default;

    Animator _anim => GetComponent<Animator>();

    /// <summary>
    /// �A�j���[�V�����������l�����Z�b�g����
    /// �ēx�A�j���[�V�����������
    /// </summary>
    void ResetValue()
    {
        Array.ForEach(_storyTexts, s => s.alpha = 0);
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

        _endAnimation = true;
    }

    /// <summary>�e�L�X�g���t�F�[�h�����ĕ\������ </summary>
    public void ShowText()
    {
        _storyTexts[_currnetTextIndex].DOFade(1, _showTextTime)
            .OnComplete(() => _currnetTextIndex++);
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
            Initialize();
            _anim.Play("Open");
            _showStroyEnumerator = ShowStory();
            StartCoroutine(_showStroyEnumerator);
        }
        else
        {
            _anim.Play("Close");
            StopCoroutine(_showStroyEnumerator);

            if (!_endAnimation)
            {
                _storyTexts[_currnetTextIndex].DOComplete();
            }

            Array.ForEach(_storyTexts, s => s.alpha = 0);
            _currnetTextIndex = 0;
            _endAnimation = false;
        }

        AudioManager.Instance.PlaySE(7);
    }

    /// <summary>����{�^�������������Ɏ��s������������ǉ�����֐� </summary>
    /// <param name="action"></param>
    public void CloseButtonAddListener(UnityAction action)
    {
        _closeButton.onClick.AddListener(action);
        _closeButton.onClick.AddListener(() => _closeButton.onClick.RemoveAllListeners());  //��x�������s������ׂɁA�{�^���������ꂽ��o�^����Ă��鏈����S�č폜����
    }

    /// <summary>�e���[�h�̏������������s�� </summary>
    public void Initialize()
    {
        if (_currentMode == ShowMode.Title)
        {
            _skipButton.gameObject.SetActive(false);
            _nextText.gameObject.SetActive(false);
        }
        else
        {
            _closeButton.gameObject.SetActive(false);
            _nextText.color = new Color(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// �X�g�[���[�̉��o�𖳂����A�����Ƀe�L�X�g��\��������
    /// �{�^������Ăяo��
    /// </summary>
    public void EndAnimation()
    {
        if (_endAnimation) { return; }  //�A�j���[�V�������I�����Ă����牽�����Ȃ�

        StopCoroutine(_showStroyEnumerator);
        _storyTexts[_currnetTextIndex].DOComplete();
        Array.ForEach(_storyTexts, s => s.alpha = 1);
    }

    /// <summary>�\��������ꏊ�ɂ���ĕ\���̎d����ς��� </summary>
    enum ShowMode
    {
        /// <summary>�^�C�g���ŕ\�� </summary>
        Title,
        /// <summary>�Q�[����(�����\��) </summary>
        InGame,
    }
}
