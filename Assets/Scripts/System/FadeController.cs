using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>�t�F�[�h�֘A�̏������s���N���X</summary>
public class FadeController : MonoBehaviour
{
    /// <summary>���l�̍ő�l </summary>
    const float MAX_��_VALUE = 1f;
    /// <summary>���l�̍ŏ��l </summary>
    const float MIN_��_VALUE = 0f;

    [SerializeField, Header("�t�F�[�h�C���ɂ����鎞��")]
    float _fadeInTime = 0f;
    [SerializeField, Header("�t�F�[�h�A�E�g�ɂ����鎞��")]
    float _fadeOutTime = 0f;
    [SerializeField, Tooltip("�t�F�[�h���s���p�l��")]
    Image _panel = default;

    /// <summary>�t�F�[�h�C�����s������</summary>
    /// <param name="action">�t�F�[�h�C���I��������</param>
    public void FadeIn(Action action = null)
    {
        _panel.DOFade(MAX_��_VALUE, _fadeInTime)
            .OnComplete(() => action?.Invoke());
    }

    /// <summary>�t�F�[�h�A�E�g���s������</summary>
    /// <param name="action">�t�F�[�h�A�E�g�I��������</param>
    public void FadeOut(Action action = null)
    {
        _panel?.DOFade(MIN_��_VALUE, _fadeOutTime)
            .OnComplete(() => action?.Invoke());
    }
}
