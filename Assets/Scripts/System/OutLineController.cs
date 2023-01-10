using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class OutLineController : MonoBehaviour
{
    [SerializeField, Header("�_�ł�����OutLine���������e�L�X�g")]
    TextMeshProUGUI _text;

    [SerializeField , Header("OutLine�̍ő勗��")]
    float _maxWidth = 0.55f;

    [SerializeField , Header("OutLine�̍ŏ�����")]
    float _minWidth = 0.20f;

    [SerializeField , Header("���b�����ĕΈڂ����邩")]
    float _changeInterval = 1f;

    Tweener _tween;

    private void Start()
    {
        Initialize();   
    }

    public void Initialize() 
    {
        _text.outlineWidth = _maxWidth;
        FlushingStrat();
    }

    public void FlushingStrat() 
    {
        if(_text.outlineWidth == _maxWidth) 
        {
            _tween = DOTween.To(() => _text.outlineWidth, // �A���I�ɕω�������Ώۂ̒l
                       x => _text.outlineWidth = x, // �ω��������l x ���ǂ��������邩������
                       _minWidth, // x ���ǂ̒l�܂ŕω������邩�w������
                       _changeInterval)
                       .OnComplete(()=>FlushingStrat());
        }
        else 
        {
            _tween = DOTween.To(() => _text.outlineWidth, // �A���I�ɕω�������Ώۂ̒l
                       x => _text.outlineWidth = x, // �ω��������l x ���ǂ��������邩������
                       _maxWidth, // x ���ǂ̒l�܂ŕω������邩�w������
                       _changeInterval)
                       .OnComplete(() => FlushingStrat());
        }
    }

    public void FlushingStop() 
    {
        _tween.Kill();
    }
}
