using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>�V�ѕ�UI���Ǘ�����N���X </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField, Tooltip("�L�����o�X�����ׂ̃{�^��")]
    Button _closeButton = default;

    Animator _anim => GetComponent<Animator>();

    /// <summary>
    /// �X�g�[���[UI�𑀍삷��
    /// �{�^������Ăяo��
    /// </summary>
    /// <param name="flag">true=�\�� false=��\��</param>
    public void TutorialOperator(bool flag)
    {
        if (flag)
        {
            _anim.Play("Open");
        }
        else
        {
            _anim.Play("Close");
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
}
