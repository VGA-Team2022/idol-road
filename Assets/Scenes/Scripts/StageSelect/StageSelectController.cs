using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageSelectController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�Z���N�g�̃{�^��"), ElementNames(new string[] { "�`���[�g���A��", "�ȒP", "����", "���" })]
    Button[] _stageSelectButton = default;
    [SerializeField,Header("�X�e�[�W�Z���N�g�ɂ���ĕω�����C���[�W")]
    Image _selectColor = default;
    [SerializeField]
    FadeController _fadeController = default;
    void Start()
    {
        GameStateEndMethod();
        //�`���[�g���A������������
        _stageSelectButton[0].onClick.AddListener(() => _selectColor.color = Color.red);
        //�ȒP����������
        _stageSelectButton[1].onClick.AddListener(() => _selectColor.color = Color.blue);
        //���ʂ���������
        _stageSelectButton[2].onClick.AddListener(() => _selectColor.color = Color.yellow);
        //�������������
        _stageSelectButton[3].onClick.AddListener(() => _selectColor.color = Color.green);
    }
    /// <summary>�^�C�g������̃V�[���J�ڂ��I������Ƃ��ɌĂяo�����֐�</summary>
    public void GameStateEndMethod()
    {
        _fadeController.FadeOut(() => _fadeController.gameObject.SetActive(true));
    }
}
