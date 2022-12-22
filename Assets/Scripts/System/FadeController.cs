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

    /// <summary>�t�F�[�h�C�����s������ ���邭�Ȃ�</summary>
    /// <param name="action">�t�F�[�h�C���I��������</param>
    public void FadeIn(Action action = null)
    {
        gameObject.SetActive(true);
        _panel.color = new Color(0, 0, 0, MAX_��_VALUE);
        _panel.DOFade(MIN_��_VALUE, _fadeInTime)
            .OnComplete(() => { action?.Invoke(); });
    }

    /// <summary>�t�F�[�h�A�E�g���s�������@�Â��Ȃ�</summary>
    /// <param name="action">�t�F�[�h�A�E�g�I��������</param>
    public void FadeOut(Action action = null)
    {
        gameObject.SetActive(true);
        _panel.color = new Color(0, 0, 0, MIN_��_VALUE);
        _panel.DOFade(MAX_��_VALUE, _fadeOutTime)
            .OnComplete(() => { action?.Invoke(); });
    }

    /// <summary>�t�F�[�h�C������ </summary>
    /// <param name="time">�t�F�[�h�ɂ����鎞��</param>
    public void FadeIn(float time, Action action = null)
    {
        gameObject.SetActive(true);
        _panel.color = new Color(0, 0, 0, MAX_��_VALUE);
        _panel.DOFade(MIN_��_VALUE, time)
            .OnComplete(() => { action?.Invoke(); });
    }

    /// <summary>�t�F�[�h�A�E�g���� </summary>
    /// <param name="time">�t�F�[�h�ɂ����鎞��</param>
    public void FadeOut(float time, Action action = null)
    {
        gameObject.SetActive(true);
        _panel.color = new Color(0, 0, 0, MIN_��_VALUE);
        _panel.DOFade(MAX_��_VALUE, time)
            .OnComplete(() => { action?.Invoke(); });
    }
}
