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
    void Start()
    {
        //�`���[�g���A������������
        _stageSelectButton[0].onClick.AddListener(() => _selectColor.color = Color.red);
        //�ȒP����������
        _stageSelectButton[1].onClick.AddListener(() => _selectColor.color = Color.blue);
        //���ʂ���������
        _stageSelectButton[2].onClick.AddListener(() => _selectColor.color = Color.yellow);
        //�������������
        _stageSelectButton[3].onClick.AddListener(() => _selectColor.color = Color.green);
    }
}
