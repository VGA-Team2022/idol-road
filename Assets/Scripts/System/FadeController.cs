using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>�t�F�[�h�֘A�̏������s���N���X</summary>
public class FadeController : MonoBehaviour
{
    [SerializeField, Header("�t�F�[�h�C���ɂ����鎞��")]
    float _fadeInTime = 0f;
    [SerializeField, Header("�t�F�[�h�A�E�g�ɂ����鎞��")]
    float _fadeOutTime = 0f;
    [SerializeField, Tooltip("�t�F�[�h���s���p�l��")]
    Image _panel = default;

    /// <summary>�t�F�[�h�C�����s������</summary>
    /// <param name="action">�t�F�[�h�C���I��������</param>
    public bool FadeIn(Action action = null)
    {
        _panel.DOFade(1f, _fadeInTime)
            .OnComplete(() => action?.Invoke());

        return true;
    }

    /// <summary>�t�F�[�h�A�E�g���s������</summary>
    /// <param name="action">�t�F�[�h�A�E�g�I��������</param>
    public bool FadeOut(Action action = null)
    {
        _panel?.DOFade(0f, _fadeOutTime)
            .OnComplete(() => action?.Invoke());

        return true;
    }
}
