using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeColor : MonoBehaviour
{
    [SerializeField,Header("�^�C�}�[�֘A�̕ϐ�")] 
    float _span = 5.0f;
    /// <summary>�����Ǘ��̃^�C�}�[</summary>
    float _time = 0;
    /// <summary>���F�ɂȂ��Ă����t���O</summary>
    bool _isFade;
    /// <summary>�����ɂȂ�b���̃v���p�e�B</summary>
    public float Span { get => _span; set => _span = value; }
    private void Start()
    {
        _isFade = false;
    }
    private void Update()
      {
        _time += Time.deltaTime;
        if (_isFade&&_time<_span)
        {          
            float alpha = 1.0f - _time / _span;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = alpha;
            GetComponent<SpriteRenderer>().color = color;
        }
      }
      /// <summary>�A�j���[�V�����I�����ɌĂяo�����֐�</summary>
      public void FadeCharacter()
      {
          _isFade = true;
      }
}
