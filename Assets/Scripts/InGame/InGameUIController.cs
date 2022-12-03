using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>�Q�[���V�[���Ŏg�p����UI�̊Ǘ�����N���X </summary>
[RequireComponent(typeof(GameManager))]
public class InGameUIController : MonoBehaviour
{
    [SerializeField, Header("�S�[���܂ł̋�����\������X���C�_�[")]
    Slider _goalSlider = default;
    [SerializeField, Header("�A�C�h���p���[��\������X���C�_�[")]
    Slider _idolPowerGauge = default;
    [SerializeField, Header("HPUI�̐e�I�u�W�F�N�g")]
    GridLayoutGroup _hpUIParent = default;
    [SerializeField, Header("HPUI�̃v���n�u")]
    Image _hpPrefab = default;
    [SerializeField, Header("�R���{����\������e�L�X�g")]
    TMP_Text _comboText = default;
    /// <summary>���݂̕\�����Ă���HpUI�̔z��</summary>
    Image[] _currentHpUIArray = default;
    GameManager _gameManager => GetComponent<GameManager>();

    private void Start()
    {
        _currentHpUIArray = new Image[_gameManager.MaxIdleHp];

        for (var i = 0; i < _gameManager.MaxIdleHp; i++)    //HPUI���ő�̗͕��\������
        {
            HpUIGenerator(i);
        }

        _goalSlider.maxValue = _gameManager.CountTime;          //�X���C�_�[�̍ő�l���Q�[���v���C���ԂƓ����ɂ���
        _idolPowerGauge.maxValue = _gameManager.MaxIdlePower;   //�A�C�h���p���[�Q�[�W�ő�l��ύX����

        //�֐���o�^
        _gameManager.OnReduceHpUI += ReduseHpUI;
        _gameManager.OnChangeIdolPowerGauge += ChangeIdolPowerGauge;
        _gameManager.OnUpdateComboText += UpdateComboText;
        _gameManager.OnUpdateGoalDistanceUI += UpdateGoalDistanceUI;
    }

    /// <summary>HPUI�𐶐����z��ɒǉ����� </summary>
    /// <param name="index">�z��̓Y����</param>
    void HpUIGenerator(int index)
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
    void ReduseHpUI(int currentHp)
    {
        _currentHpUIArray[currentHp].enabled = false;
    }

    /// <summary> �A�C�h���p���[�Q�[�W�𑝌�������</summary>
    /// <param name="value">�����l(����������ꍇ�͕���������)</param>
    void ChangeIdolPowerGauge(int value)
    {
        _idolPowerGauge.value = value;
    }

    /// <summary>�S�[���܂ł̋�����\������UI���X�V����</summary>
    void UpdateGoalDistanceUI(float elapsedTime)
    {
        _goalSlider.value = elapsedTime;   //�S�[���܂ł̋�����UI�ɕ\������
    }

    /// <summary>�R���{����\������e�L�X�g���X�V���� </summary>
    void UpdateComboText(int comboCount)
    {
        _comboText.text = comboCount.ToString();
    }
}
