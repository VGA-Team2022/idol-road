using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�Q�[���V�[���Ŏg�p����UI�̊Ǘ�����N���X </summary>
public class InGameUIController : MonoBehaviour
{
    [SerializeField, Header("�R���{���ɕ\������C���X�g�̏���"), ElementNames(new string[] {"1", "2", "3", "4"})]
    Sprite[] _comboSprites = default;
    [SerializeField, Tooltip("�S�[���܂ł̋�����\������X���C�_�[")]
    Slider _goalSlider = default;
    [SerializeField, Tooltip("�A�C�h���p���[��\������Image")]
    Image _idolPowerGauge = default;
    [SerializeField, Tooltip("HPUI�̐e�I�u�W�F�N�g")]
    GridLayoutGroup _hpUIParent = default;
    [SerializeField, Tooltip("HPUI�̃v���n�u")]
    Image _hpPrefab = default;
    [SerializeField, Tooltip("�R���{����\������e�L�X�g")]
    TMP_Text _comboText = default;
    [SerializeField, Tooltip("�R���{�C���X�g��\������Image")]
    Image _comboImage = default;
    [SerializeField, Tooltip("�R���{�Ŏg�p����A�j���[�^�["), ElementNames(new string[] {"�w�i", "�C���X�g"})]
    Animator[] _comboAnimators = default;

    /// <summary>���݂̕\�����Ă���HpUI�̔z��</summary>
    Image[] _currentHpUIArray = default;
    /// <summary>�\������C���X�g�̓Y����</summary>
    int _currentComboIndex = 0;

    /// <summary>InGameUI�̏���������</summary>
    /// <param name="maxHp">�ő�HP</param>
    /// <param name="gameTime">�Q�[���v���C����</param>
    /// <param name="maxIdlePower">�ő�A�C�h���p���[</param>
    public void InitializeInGameUI(int maxHp, float gameTime, float maxIdlePower)
    {
        _currentHpUIArray = new Image[maxHp];

        for (var i = 0; i < maxHp; i++)    //HPUI���ő�̗͕��\������
        {
            HpUIGenerator(i);
        }

        _goalSlider.maxValue = gameTime;          //�X���C�_�[�̍ő�l���Q�[���v���C���ԂƓ����ɂ���
        //_idolPowerGauge.maxValue = maxIdlePower;   //�A�C�h���p���[�Q�[�W�ő�l��ύX����(Slider���̏���)
    }

    /// <summary>HPUI�𐶐����z��ɒǉ����� </summary>
    /// <param name="index">�z��̓Y����</param>
    public void HpUIGenerator(int index)
    {
        var hpUI = Instantiate(_hpPrefab);
        var liveHaart = hpUI.transform.GetChild(0).GetComponent<Image>();   //�q�I�u�W�F�N�g�ɂ���c���C�t�̃C���X�g���擾����
        hpUI.transform.SetParent(_hpUIParent.transform);

        hpUI.transform.localScale = new Vector3(1, 1, 1);   //�����̑傫�������߂�
        liveHaart.rectTransform.sizeDelta = new Vector2(_hpUIParent.cellSize.x, _hpUIParent.cellSize.y);    //�c���C�t�̑傫���𒲐�����

        _currentHpUIArray[index] = liveHaart;
    }

    /// <summary>���݂̗̑͂�HPUI�����킹��</summary>
    /// <param name="currentHp">���݂̗̑�</param>
    public void UpdateHpUI(int currentHp)
    {
        _currentHpUIArray[currentHp].enabled = false;
    }

    /// <summary> �A�C�h���p���[�Q�[�W�𑝌�������</summary>
    /// <param name="value">�����l(����������ꍇ�͕���������)</param>
    public void UpdateIdolPowerGauge(float value)
    {
        _idolPowerGauge.fillAmount = value;
    }

    /// <summary>�S�[���܂ł̋�����\������UI���X�V����</summary>
    public void UpdateGoalDistanceUI(float elapsedTime)
    {
        _goalSlider.value = elapsedTime;   //�S�[���܂ł̋�����UI�ɕ\������
    }

    /// <summary>�R���{����\������e�L�X�g���X�V���� </summary>
    public void UpdateComboText(int comboCount)
    {
        _comboText.text = comboCount.ToString();
    }

    /// <summary>�R���{�p�J�b�g�C���̃A�j���[�V�������Đ�����</summary>
    public void PlayComboAnimation(Sprite sprite)
    {
        //_comboImage.sprite = _comboSprites[_currentComboIndex];     //�C���X�g��ύX
        //_currentComboIndex = (_currentComboIndex + 1) % _comboSprites.Length;   //�C���X�g���z�����邽��

        _comboImage.sprite = sprite;

        foreach (var anim in _comboAnimators)
        {
            anim.SetTrigger("Play");
        }
    }
}
